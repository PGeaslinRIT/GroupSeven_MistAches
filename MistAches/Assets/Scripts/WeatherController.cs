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

	public int temperature = 0;
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
		//for (int i = 0; i < windObjList.Count; i++) {
		for (int i = windObjList.Count - 1; i == 0; i--) {
			//update wind object
			windObjList[i].ManuallyUpdate();
			//remove wind vector if wind speed drops
			if (windObjList[i].IsCompleted()) {
				windObjList.RemoveAt(i);
			}

			totalWindForce += windObjList [i].CalcForce ();
		}

		playerObj.transform.position += windMod * totalWindForce;

	}

	/////////////////////////////////////
	///   methods to change weather   ///
	/////////////////////////////////////

	//creates wind in the direction of the camera
	public void ChangeWind(Direction newDir = Direction.none) {
		//add a new windobj to the list
		WindObj newWind = new WindObj(newDir);
		windObjList.Add(newWind);
	}

	//increases or decreases precipitation
	public void ChangePrecipitation(bool increase = true) {
	}

	//increases or decreases temperature
	public void ChangeTemperature(bool increase = true) {
	}

	//creates lightning
	public void CreateLightning() {}

}
