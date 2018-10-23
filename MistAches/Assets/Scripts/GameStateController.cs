using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour {

	public GameObject player;
	public GameObject button;
	public int state;

	// Use this for initialization
	void Start () {
		if (gameObject.scene.name == "mainmenu")
			state = 1;
		else if (gameObject.scene.name == "gameover")
			state = 2;
		else if (gameObject.scene.name == "sandbox")
			state = 10;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		//main menu
		case 1:
			break;
		//game over
		case 2:
			break;
		//pause
		case 3:
			break;
		//level 1
		case 4:
			break;
		case 10:
			if (Input.GetKeyDown (KeyCode.Escape)) {
				state = 3;
			}
			if (player.transform.position.y <= -10) {
				state = 2;
			}
			break;
		}
	}
}
