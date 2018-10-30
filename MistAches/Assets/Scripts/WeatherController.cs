using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	up,
	right,
	down,
	left,
	none
};

public class WeatherController : MonoBehaviour {
	//platform references
	public List<GameObject> allPlatformColliders = new List<GameObject> (); //set manually for now

	//physics materials
//	public float iceFriction = 0.0f;
//	public float defaultFriction = 0.4f;
	public PhysicsMaterial2D iceMat;
	public PhysicsMaterial2D defaultMat;

	//player weather controller
	public GameObject playerObj;
	public List<GameObject> interactableObjects = new List<GameObject> (); //set manually for now

	//temperature variables
	public int temperature = 0;
	public GameObject hotTintObj;
	public GameObject coldTintObj;

	//precipitation variables
	public int precipitation = 0;
	public GameObject precipParticleObj;
	ParticleSystem myParticleSystem;
	ParticleSystem.EmissionModule particleEmissions;
	ParticleSystem.MainModule particleMain;

	//wind variables
	public float windMod = 0.1f;
	public int windMaxDuration = 250;
	public List<WindObj> windObjList = new List<WindObj> ();
	public Vector3 totalWindForce = Vector3.zero;

	//lightning variables
	public int lightningDuration = 0;
	public int maxLightningDuration = 5;
	public GameObject lightningObj; //set manually for the time being

	//methods to get the weather states
	public bool IsRaining () { return (precipitation > 0 && temperature > -1); }
	public bool IsSnowing () { return (precipitation > 0 && temperature == -1); }
	public bool IsPrecipitating () { return (precipitation > 0); }
	public bool IsStorming () { return (precipitation == 2); }
	public bool IsHot () { return (temperature == 1); }
	public bool IsCold () { return (temperature == -1); }
	public bool IsWindy () { return (windObjList.Count > 0); }

	// Use this for initialization
	void Start () {
		//set up particle system
		myParticleSystem = precipParticleObj.GetComponent<ParticleSystem> ();
		particleEmissions = myParticleSystem.emission;
		particleMain = myParticleSystem.main;

		//grab reference to lightning object
//		SpriteRenderer[] playerChildren = playerObj.GetComponentsInChildren<SpriteRenderer>();
//		foreach (SpriteRenderer child in playerChildren) {
//			Debug.Log ("Child name = " + child.name + " Child.gameObjet name = " + child.gameObject.name);
//			if (child.name == "Lightning") {
//				lightningObj = child.gameObject;
//				break;
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {
		//update wind
		UpdateWind();

		//update lightning
		UpdateLightning();

		//make snow and rain appear
		UpdatePrecipitation ();
	}

	/////////////////////////////////////
	///   methods to change weather   ///
	/////////////////////////////////////

	//creates wind in the direction of the camera
	public bool ChangeWind(Direction newDir = Direction.none) {
		//add a new windobj to the list
		WindObj newWind = new WindObj(newDir);
		windObjList.Add(newWind);

		return true;
	}

	//increases or decreases precipitation
	public bool ChangePrecipitation(bool increase = true) {
		if (increase && precipitation < 2) {
			precipitation++;
		} else if (!increase && precipitation > 0) {
			precipitation--;
		} else {
			return false; //invalid break if beyond precipitation range
		}

		switch (precipitation) {
		default:
		case 0:
			particleEmissions.rateOverTime = 0.0f;
			break;
		case 1:
			particleEmissions.rateOverTime = 10.0f;
			break;
		case 2:
			particleEmissions.rateOverTime = 70.0f;
			break;
		}

		return true;
	}

	//increases or decreases temperature
	public bool ChangeTemperature(bool increase = true) {
		if (increase && temperature < 1) {
			temperature++;
		} else if (!increase && temperature > -1) {
			temperature--;
		} else {
			return false; //invalid break if beyond temperature range
		}

		//set appropriate tint
		if (IsCold()) {
			coldTintObj.SetActive (true);
		} else {
			coldTintObj.SetActive (false);
		}

		if (IsHot()) {
			hotTintObj.SetActive (true);
		} else {
			hotTintObj.SetActive (false);
		}

		return true;
	}

	//creates lightning
	public bool CreateLightning() {
		if (!IsRaining() || lightningDuration > 0) {
			return false; //invalid break if not raining or lightning is already active
		}

		lightningObj.SetActive (true);
		lightningDuration = maxLightningDuration;		

		return true;
	}

	/////////////////////////////////////
	///  methods to maintain weather  ///
	/////////////////////////////////////

	//changes between rain and snow
	void UpdatePrecipitation() {
		if (IsPrecipitating ()) {
			//update particles to match weather
			if (IsSnowing()) {
				particleMain.startColor = new Color(255, 255, 255);
				particleMain.simulationSpeed = 0.25f;
				particleMain.startSize = 0.15f;
			} else if (IsRaining()){ //rain
				particleMain.startColor = new Color(0, 0, 255);
				particleMain.simulationSpeed = 1.0f;
				particleMain.startSize = 0.1f;
			}

			//check for iciness
			foreach (GameObject colliderObj in allPlatformColliders) {
				if (IsSnowing ()) {
					colliderObj.GetComponent<BoxCollider2D>().sharedMaterial = iceMat;
				} else {
					colliderObj.GetComponent<BoxCollider2D>().sharedMaterial = defaultMat;
				}

				colliderObj.SetActive (false);
				colliderObj.SetActive (true);
			}
		}

	}

	//update the wind objects
	void UpdateWind() {
		//reset wind force
		totalWindForce = Vector3.zero;

		//maintain wind durations for each wind force
		for (int i = 0; i < windObjList.Count; i++) {
			//update wind object
			windObjList [i].ManuallyUpdate ();
			//remove wind vector if wind speed drops
			if (windObjList [i].IsCompleted ()) {
				Destroy (windObjList [i]);
				windObjList.RemoveAt (i);
			}

			totalWindForce += windObjList [i].CalcForce ();
		}

		//apply wind to all of the objects that it can affect
		playerObj.transform.position += windMod * totalWindForce;

		for (int i = 0; i < interactableObjects.Count; i++) {
			interactableObjects [i].transform.position += windMod * totalWindForce;
		}
	}

	//update lightning
	void UpdateLightning() {
		if (lightningDuration > 0) {
			//decrement lightning count
			lightningDuration--;

			BoxCollider2D lightningCollider = lightningObj.GetComponent<BoxCollider2D>();

			//check for collisisons with interactable objects
			for (int i = 0; i < interactableObjects.Count; i++) {
				BoxCollider2D thisObjCollider = interactableObjects [i].GetComponent<BoxCollider2D> ();

				if (lightningCollider.IsTouching(thisObjCollider)) {
					Destroy (interactableObjects[i]);
					interactableObjects.RemoveAt (i);
				}
			}
		} else {
			//turn off lightning if no longer active
			lightningObj.SetActive (false);
		}
	}
}
