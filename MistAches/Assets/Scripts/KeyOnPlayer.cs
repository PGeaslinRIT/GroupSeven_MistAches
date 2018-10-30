using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOnPlayer : MonoBehaviour {

	public bool hasKey;

	private ReferenceManager refManager;
	public GameObject player;

	// Use this for initialization
	void Start () {
		hasKey = false;

		refManager = GetComponent<ReferenceManager> ();
		player = refManager.playerObj.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasKey) {
			player.transform.FindChild ("Key").gameObject.SetActive (true);
		} else {
			player.transform.FindChild ("Key").gameObject.SetActive (false);
		}
	}
}
