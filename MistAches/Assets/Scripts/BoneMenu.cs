using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		myWeatherController = gameObject.GetComponent<WeatherController> ();

		brokenBones = new int[6];

		for (int i = 0; i < 6; i++) {
			brokenBones [i] = 0;
		}
	}
	
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
