using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class GameStateController : MonoBehaviour {

	public GameObject player;
	public GameObject panel;
	public Button btnPlay;
	public Button btnQuit;
	public Button btnRestart;
	public int state;
	private bool on;

	// Use this for initialization
	void Start () {
		if (gameObject.scene.name == "mainmenu") {
			state = 1;
			btnPlay.onClick.AddListener (Play);
			btnQuit.onClick.AddListener (Quit);
		}
		else if (gameObject.scene.name == "gameover") {
			state = 2;
			btnRestart.onClick.AddListener (Restart);
			btnQuit.onClick.AddListener (Quit);
		}
		else if (gameObject.scene.name == "sandbox") {
			state = 10;
			on = false;
			player.transform.position = new Vector3 (-4, -1, 0);
			player.transform.localScale = new Vector3 (1.771533f, 1.77153f, 1);
		}
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
				SceneManager.LoadScene ("gameover");
			}
			if (Input.GetKeyDown (KeyCode.Tab)) {
				on = !on;
				panel.SetActive (on);
				player.GetComponent<Platformer2DUserControl> ().enabled = !on;
				player.GetComponent<Animator> ().enabled = !on;
				player.GetComponent<Rigidbody2D> ().simulated = !on;
			}
			break;
		}
	}

	void Play () {
		state = 10;
		SceneManager.LoadScene ("sandbox");
	}

	void Quit () {
		Application.Quit ();
	}

	void Restart () {
		state = 1;
		SceneManager.LoadScene ("mainmenu");
	}
}
