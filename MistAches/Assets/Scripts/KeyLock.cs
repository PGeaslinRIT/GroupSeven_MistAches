using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class KeyLock : MonoBehaviour {

	private ReferenceManager refManager;

	public List<GameObject> keys;
	public List<GameObject> locks;

	private GameObject player;

	// Use this for initialization
	void Start () {
		refManager = GetComponent<ReferenceManager> ();

		player = refManager.playerObj.gameObject;
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < keys.Count; i++) {
			if (player.GetComponent<Rigidbody2D>().IsTouching(keys[i].GetComponent<BoxCollider2D>()) && gameObject.GetComponent<KeyOnPlayer>().hasKey == false) {
				Debug.Log ("touch key");
				Destroy (keys [i]);
				keys.RemoveAt (i);
				gameObject.GetComponent<KeyOnPlayer> ().hasKey = true;
			}
		}

		for (int i = 0; i < locks.Count; i++) {
			if (player.GetComponent<Rigidbody2D> ().IsTouching (locks [i].GetComponent<BoxCollider2D> ()) && gameObject.GetComponent<KeyOnPlayer> ().hasKey == true) {
				Destroy (locks [i]);
				locks.RemoveAt (i);
				gameObject.GetComponent<KeyOnPlayer> ().hasKey = false;
			}
		}
	}
}
