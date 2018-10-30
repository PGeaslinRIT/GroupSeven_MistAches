using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOnPlayer : MonoBehaviour {

	public bool hasKey;

	// Use this for initialization
	void Start () {
		hasKey = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasKey) {
			gameObject.transform.FindChild ("Key").gameObject.SetActive (true);
		} else {
			gameObject.transform.FindChild ("Key").gameObject.SetActive (false);
		}
	}
}
