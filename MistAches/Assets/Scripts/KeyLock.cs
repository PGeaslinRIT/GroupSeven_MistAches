using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour {

	public List<GameObject> keys;
	public List<GameObject> locks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
