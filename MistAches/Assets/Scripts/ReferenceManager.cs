using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class ReferenceManager : MonoBehaviour {

	public PlatformerCharacter2D playerObj;
	public Camera cameraObj;

	public List<GameObject> interactableObjects = new List<GameObject> (); //set manually for now
	public List<GameObject> allPlatformColliders = new List<GameObject> (); //set manually for now
	public List<GameObject> plantObjList = new List<GameObject> (); //set manually for now

	public GameObject hotTintObj;
	public GameObject coldTintObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
