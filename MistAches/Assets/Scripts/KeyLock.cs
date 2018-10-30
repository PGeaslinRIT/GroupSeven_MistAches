using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour {

	private ReferenceManager refManager;

	public List<GameObject> keys;
	public List<GameObject> locks;

	private GameObject player;

	// Use this for initialization
	void Start () {
		refManager = GetComponent<ReferenceManager> ();

		player = refManager.playerObj;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < keys.Count; i++) {
			if (player.GetComponent<BoxCollider2D> ().IsTouching (keys [i].GetComponent<BoxCollider2D> ()) && player.GetComponent<KeyOnPlayer>().hasKey == false) {
				Destroy (keys [i]);
				keys.RemoveAt (i);
				player.GetComponent<KeyOnPlayer> ().hasKey = true;
			}
		}

		for (int i = 0; i < locks.Count; i++) {
			if (player.GetComponent<BoxCollider2D> ().IsTouching (locks [i].GetComponent<BoxCollider2D> ()) && player.GetComponent<KeyOnPlayer> ().hasKey == true) {
				Destroy (locks [j]);
				locks.RemoveAt (j);
				player.GetComponent<KeyOnPlayer> ().hasKey = false;
			}
		}
	}
}
