using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour {
	private List<GameObject> plantObjList;
	private BoxCollider2D playerCollider;
	private Vector3 position;

	public float climbSpeed = 0.05f;



	// Use this for initialization
	void Start () {
		plantObjList = GameObject.Find ("LevelManager").GetComponent<ReferenceManager>().plantObjList;

		playerCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		position = gameObject.transform.position;

		if (Input.GetKeyUp(KeyCode.W)) {
			Debug.Log ("pressing w");

			foreach (GameObject obj in plantObjList) {
				if (playerCollider.IsTouching(obj.GetComponent<BoxCollider2D> ())) {
					Debug.Log ("climbing tree");
					position.y += climbSpeed;
				}
			}

			gameObject.transform.position = position;
		}
	}
}
