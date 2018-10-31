using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindObj : MonoBehaviour {
	private Direction myDir = Direction.none;
	private int myDuration = 0;
	private int myMaxDuration = 250;
	private Vector3 myForce = Vector3.zero;
	private bool completed = false;

	//getters and setters
	public void SetMaxDuration (int newMax) { myMaxDuration = newMax; } 
	public bool IsCompleted () { return completed; }
	public Direction GetDir () { return myDir; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	//update when called
	public void ManuallyUpdate () {
		//decrease duration
		if (myDuration >= 0) {
			myDuration--;
		}

		//kill this off if it's hit its limit
		if (myDuration == 0) {
			completed = true;
			myForce = Vector3.zero;
		}
	}

	//constructor
	public WindObj (Direction newDir) {
		myDir = newDir;
		myDuration = myMaxDuration;

		myForce = CalcForce ();
	}

	//calculate the windforce of this object
	public Vector3 CalcForce () {
		switch (myDir) {
		case Direction.up:
			myForce.y = .5f;
			break;
		case Direction.down:
			myForce.y = -.5f;
			break;
		case Direction.left:
			myForce.x = .5f;
			break;
		case Direction.right:
			myForce.x = .5f;
			break;
		default:
		case Direction.none:
			myForce = Vector3.zero;
			break;
		}

		myForce.z = 0.0f;

		return myForce;
	}
}
