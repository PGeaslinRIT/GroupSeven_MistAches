using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour {
	private WeatherController weatherControllerObj;

	public Sprite fullGrownSprite;
	public bool isGrown = false;

	// Use this for initialization
	void Start () {
		weatherControllerObj = GameObject.Find ("LevelManager").GetComponent<WeatherController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGrown && weatherControllerObj.IsHot () && weatherControllerObj.IsRaining ()) {
			GetComponent<SpriteRenderer> ().sprite = fullGrownSprite;
			Destroy(GetComponent<BoxCollider2D>());
			gameObject.AddComponent<BoxCollider2D> ();

			isGrown = true;
		}
	}
}
