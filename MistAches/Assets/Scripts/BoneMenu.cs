using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMenu : MonoBehaviour {
	//weather controller reference
	WeatherController myWeatherController;

	//broken bone trackers
	public Dictionary<string, int> brokenBones = new Dictionary<string, int>();

	public int maxBrokenBones = 3;
	public int armLegCount = 2;

	public GameObject panel;
	private bool on;

	// Use this for initialization
	void Start () {
<<<<<<< HEAD
		on = false;
=======
		myWeatherController = gameObject.GetComponent<WeatherController> ();

		brokenBones.Add ("skull", 0);
		brokenBones.Add ("neck", 0);
		brokenBones.Add ("back", 0);
		brokenBones.Add ("ribs", 0);
		brokenBones.Add ("arms", 0);
		brokenBones.Add ("legs", 0);
		
>>>>>>> 40628403e383255fce6e1489e763d38ec3a34ebc
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if (Input.GetKeyDown (KeyCode.Tab)) {
			on = !on;
			panel.SetActive (on);
		}
=======
		if (Input.GetKeyUp(KeyCode.Alpha1)) {
			BreakBone ("ribs", true, Direction.up);
			Debug.Log ("Wind Up");
		} else if (Input.GetKeyUp(KeyCode.Alpha2)) {
			BreakBone ("ribs", true, Direction.right);
			Debug.Log ("Wind Right");
		} else if (Input.GetKeyUp(KeyCode.Alpha3)) {
			BreakBone ("ribs", true, Direction.down);
			Debug.Log ("Wind Down");
		} else if (Input.GetKeyUp(KeyCode.Alpha4)) {
			BreakBone ("ribs", true, Direction.left);
			Debug.Log ("Wind Left");
		}
		
>>>>>>> 40628403e383255fce6e1489e763d38ec3a34ebc
	}

	//Method to break a specific bone
	bool BreakBone (string bone, bool increase = true, Direction dir = Direction.none) {
		int breakLimit = maxBrokenBones;

		//arms and legs have a higher limit than other bones
		if (bone == "arms" || bone == "legs") {
			breakLimit *= armLegCount;
		}

		//check if bone has reached break limit
		if (brokenBones[bone] >= breakLimit) {
			return false;
		} else {
			brokenBones[bone]++;
		}

		switch (bone) {
		case "arms":
			myWeatherController.ChangePrecipitation (increase);			
			break;
		case "legs":
			myWeatherController.ChangeTemperature (increase);
			break;
		case "skull":

			break;
		case "neck":

			break;
		case "back":

			break;
		case "ribs":
			myWeatherController.ChangeWind (increase, dir);
			break;
		}

		return true;
	}
}
