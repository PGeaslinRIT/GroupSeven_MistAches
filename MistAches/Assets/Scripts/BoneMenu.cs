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
	}

	//Method to break a specific bone
	bool BreakBone (Bones bone, Direction dir = Direction.none, bool increase = true) {
		int breakLimit = maxBrokenBones;

		//arms and legs have a higher limit than other bones
		if (bone == Bones.arms || bone == Bones.legs) {
			breakLimit *= armLegCount;
		}

		//check if bone has reached break limit
		if (brokenBones[(int)bone] >= breakLimit) {
			return false;
		} else {
			brokenBones[(int)bone]++;
		}

		switch (bone) {
		case Bones.arms:
			myWeatherController.ChangePrecipitation (increase);			
			break;
		case Bones.legs:
			myWeatherController.ChangeTemperature (increase);
			break;
		case Bones.skull:

			break;
		case Bones.neck:

			break;
		case Bones.back:

			break;
		case Bones.ribs:
			myWeatherController.ChangeWind (dir);
			break;
		}

		return true;
	}
}
