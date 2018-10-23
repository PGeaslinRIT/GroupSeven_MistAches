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
	public int windSpeed = 0;
	private bool windIncrease = true;
	public Direction windDir = Direction.none;
	public Vector3 windForce = Vector2.zero;
	public int windMax = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//decrease how long wind lasts
		if (windMax > 0) {
			windMax--;

			if (windMax == 0 && windSpeed > 0) {
				windSpeed--;
			}
		}

		//remove wind vector if wind speed drops
		if (windSpeed <= 0) {
			windForce = Vector3.zero;
		}

		playerObj.transform.position += windForce;

	}

	/////////////////////////////////////
	///   methods to change weather   ///
	/////////////////////////////////////

	//creates wind in the direction of the camera
	public void ChangeWind(bool increase = true, Direction windDir = Direction.none) {
		//set up direction vector
		windForce.z = 1.0f;
		switch (windDir) {
		case Direction.up:
			windForce.x = 0.0f;
			windForce.y = 1.0f;
			break;
		case Direction.right:
			windForce.x = 1.0f;
			windForce.y = 0.0f;
			break;
		case Direction.down:
			windForce.x = 0.0f;
			windForce.y = -1.0f;
			break;
		case Direction.left:
			windForce.x = -1.0f;
			windForce.y = 0.0f;
			break;
		default:
		case Direction.none:
			windForce = Vector3.zero;
			break;
		}

		if (windIncrease) {
			windSpeed++;
			windMax += 250;
		} else if (windSpeed > 0) {
			windSpeed--;
		}
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
