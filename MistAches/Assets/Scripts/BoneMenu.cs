using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMenu : MonoBehaviour {

	public GameObject panel;
	private bool on;

	// Use this for initialization
	void Start () {
		on = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			on = !on;
			panel.SetActive (on);
		}
	}
}
