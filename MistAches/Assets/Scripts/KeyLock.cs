using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class KeyLock : MonoBehaviour {

	private ReferenceManager refManager;

	public List<GameObject> keys;
	public List<GameObject> locks;

	private PlatformerCharacter2D player;

	// Use this for initialization
	void Start () {
		refManager = GetComponent<ReferenceManager> ();

		player = refManager.playerObj;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < keys.Count; i++) {
			if (player.GetComponent<BoxCollider2D> ().IsTouching (keys [i].GetComponent<BoxCollider2D> ())) {
				Destroy (keys [i]);
				keys.RemoveAt (i);
			}
		}

		for (int i = 0; i < keys.Count; i++) {
			for (int j = 0; j < locks.Count; j++) {
				if (keys [i].GetComponent<BoxCollider2D> ().IsTouching (locks [i].GetComponent<BoxCollider2D> ())) {
					Destroy (keys [i]);
					keys.RemoveAt (i);
					Destroy (locks [j]);
					locks.RemoveAt (j);
				}
			}
		}
	}
}
