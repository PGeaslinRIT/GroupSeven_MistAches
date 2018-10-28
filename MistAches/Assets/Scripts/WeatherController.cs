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
	//player weather controller
	public GameObject playerObj;
	public List<GameObject> interactableObjects = new List<GameObject> ();

	//temperature variables
	public int temperature = 0;
	public GameObject hotTintObj;
	public GameObject coldTintObj;

	//precipitation variables
	public int precipitation = 0;

	//wind variables
	public float windMod = 0.1f;
	public int windMaxDuration = 250;
	public List<WindObj> windObjList;
	public Vector3 totalWindForce = Vector3.zero;

	// Use this for initialization
	void Start () {
		windObjList = new List<WindObj> ();
	}
	
	// Update is called once per frame
	void Update () {
		//reset wind force
		totalWindForce = Vector3.zero;

		//maintain wind durations for each wind force
		//		for (int i = windObjList.Count - 1; i == 0; i--) {
		for (int i = 0; i < windObjList.Count; i++) {
			//update wind object
			windObjList[i].ManuallyUpdate();
			//remove wind vector if wind speed drops
			if (windObjList[i].IsCompleted()) {
				windObjList.RemoveAt(i);
			}

			totalWindForce += windObjList [i].CalcForce ();
		}

		playerObj.transform.position += windMod * totalWindForce;

		for (int i = 0; i < interactableObjects.Count; i++) {
			interactableObjects [i].transform.position += windMod * totalWindForce;
		}
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
		return increase;
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
		switch (temperature) {
		case -1:
			coldTintObj.SetActive (true);
			break;
		default:
		case 0:
			coldTintObj.SetActive (false);
			hotTintObj.SetActive (false);
			break;
		case 1:
			hotTintObj.SetActive (true);
			break;
		}

		return true;
	}

	//creates lightning
	public void CreateLightning() {}

}
