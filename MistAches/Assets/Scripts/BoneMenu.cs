using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
using UnityStandardAssets._2D;
<<<<<<< HEAD
=======
>>>>>>> parent of b9276b9... A - Added bone menu buttons
=======
>>>>>>> parent of 712f60c... S - added broken bone effects
=======
>>>>>>> parent of 09fbd8c... Revert "A - Added bone menu buttons"

enum Bones{
	skull,
	neck,
	back,
	ribs,
	arms,
	legs
};

public class BoneMenu : MonoBehaviour {
	//weather controller reference
	WeatherController myWeatherController;

	//broken bone trackers
	public int[] brokenBones;

	public int maxBrokenBones = 3;
	public int armLegCount = 2;

	public Button btnRibs;
	public Button btnArms;
	public Button btnLegs;
	public Button btnSkull;

	// Use this for initialization
	void Start () {
		myWeatherController = gameObject.GetComponent<WeatherController> ();

		brokenBones = new int[6];

		for (int i = 0; i < 6; i++) {
			brokenBones [i] = 0;
		}

		btnRibs.onClick.AddListener (delegate {
			BreakBone (Bones.ribs, Direction.up);
		});

		btnArms.onClick.AddListener (delegate {
			BreakBone (Bones.arms, true);
		});

		btnLegs.onClick.AddListener (delegate {
			BreakBone (Bones.legs, true);
		});

		btnSkull.onClick.AddListener (delegate {
			BreakBone (Bones.skull);
		});
<<<<<<< HEAD

        // stuff for manipulating the player
        initSpeed = pC2D.m_MaxSpeed;
        tick = 0;
        stopMoving = false;

        // setting default camera values
        targetOrtho = myCam.orthographicSize;
        smoothSpeed = 2;
        targetRot = 0;
    }
<<<<<<< HEAD
=======
	}
>>>>>>> parent of b9276b9... A - Added bone menu buttons
=======
	}
>>>>>>> parent of 712f60c... S - added broken bone effects
=======
>>>>>>> parent of 09fbd8c... Revert "A - Added bone menu buttons"
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Alpha1)) {
			BreakBone (Bones.ribs, Direction.up);
		}
		if (Input.GetKeyUp(KeyCode.Alpha2)) {
			BreakBone (Bones.ribs, Direction.right);
		}
		if (Input.GetKeyUp(KeyCode.Alpha3)) {
			BreakBone (Bones.ribs, Direction.down);
		}
		if (Input.GetKeyUp(KeyCode.Alpha4)) {
			BreakBone (Bones.ribs, Direction.left);
		}


		if (Input.GetKeyUp(KeyCode.Alpha5)) {
			BreakBone (Bones.legs, true);
		}
		if (Input.GetKeyUp(KeyCode.Alpha6)) {
			BreakBone (Bones.legs, false);
		}


		if (Input.GetKeyUp(KeyCode.Alpha7)) {
			BreakBone (Bones.arms, true);
		}
		if (Input.GetKeyUp(KeyCode.Alpha8)) {
			BreakBone (Bones.arms, false);
		}

		if (Input.GetKeyUp(KeyCode.Alpha9)) {
			BreakBone (Bones.skull);
		}
	}

	//Methods to break a specific bone
	//break bones that require a direction. default bone breaking method for bones without direction or increase/decrease
	void BreakBone (Bones bone, Direction dir = Direction.none) {
		if (!EnoughBones (bone)) {
			return;
		}

		bool validBreak = false;

		//change weather accordingly and check for break validity
		switch (bone) {
		case Bones.ribs:
			validBreak = myWeatherController.ChangeWind (dir);
			break;
		case Bones.skull:
			validBreak = myWeatherController.CreateLightning ();
			break;
		}

		//only break the bone if the break was valid
		if (validBreak) {
			brokenBones [(int)bone]++;
		}

		btnRibs.GetComponentInChildren<Text> ().text = "Ribs:\t" + brokenBones [(int)Bones.ribs];
		btnSkull.GetComponentInChildren<Text> ().text = "Skull:\t" + brokenBones [(int)Bones.skull];
	}
	//break bones that increase/decrease
	void BreakBone(Bones bone, bool increase){

		if (!EnoughBones (bone)) {
			return;
		}

		bool validBreak = false;

		//change weather accordingly and check for break validity
		switch (bone) {
		case Bones.arms:
			validBreak = myWeatherController.ChangePrecipitation (increase);			
			break;
		case Bones.legs:
			validBreak = myWeatherController.ChangeTemperature (increase);
			break;
		}

		//only break the bone if the break was valid
		if (validBreak) {
			brokenBones [(int)bone]++;
		}
			
		btnArms.GetComponentInChildren<Text> ().text = "Arms:\t" + brokenBones [(int)Bones.arms];
		btnLegs.GetComponentInChildren<Text> ().text = "Legs:\t" + brokenBones [(int)Bones.legs];
	}

	//check that there are enough bones to still break
	bool EnoughBones (Bones bone){
		int breakLimit = maxBrokenBones;

		//arms and legs have a higher limit than other bones
		if (bone == Bones.arms || bone == Bones.legs) {
			breakLimit *= armLegCount;
		}

		//check if bone has reached break limit
		return (brokenBones[(int)bone] < breakLimit);
	}
}
