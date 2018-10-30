using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour {
	private List<GameObject> plantObjList;
	private BoxCollider2D playerCollider;
	private Vector3 playerPos;

	public float climbRange = 1.0f;

	public float climbSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		plantObjList = GameObject.Find ("LevelManager").GetComponent<ReferenceManager>().plantObjList;

		playerCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = gameObject.transform.position;

		if (Input.GetKeyDown(KeyCode.W)) {
			Debug.Log ("pressing w");

			foreach (GameObject obj in plantObjList) {
				if (obj.GetComponent<PlantGrowth>().isGrown && WithinRange(climbRange, playerCollider, obj.GetComponent<BoxCollider2D> ())) {
					Debug.Log ("climbing tree");
					playerPos.y += climbSpeed;
				}
			}

			gameObject.transform.position = playerPos;
		}
	}

	//detemine whether two objects are within a range of each other
	bool WithinRange (float range, BoxCollider2D collider1, BoxCollider2D collider2) {
		Vector3 c1Pos = collider1.transform.position;
		Vector3 c2Pos = collider2.transform.position;

		float distanceSqr = (c1Pos - c2Pos).sqrMagnitude;

		return (distanceSqr < range * range);
	}
}
