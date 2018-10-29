using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class GameStateController : MonoBehaviour {

	public GameObject player;
	public GameObject panel;
	public GameObject pausePanel;
	public Button btnPlay;
	public Button btnQuit;
	public Button btnRestart;
	public Button btnControls;
	public int state;
	private int prevState;
	private bool boneMenu;
	private bool pause;
	private bool controls;

	// Use this for initialization
	void Start () {
		if (gameObject.scene.name == "mainmenu") {
			state = 1;
			controls = false;
			btnPlay.onClick.AddListener (delegate {
				LoadScene ("sandbox", 10);
			});
			btnControls.onClick.AddListener (ToggleControls);
			btnRestart.onClick.AddListener (ToggleControls);
			btnQuit.onClick.AddListener (Quit);
		}
		else if (gameObject.scene.name == "gameover") {
			state = 2;
			btnRestart.onClick.AddListener (delegate {
				LoadScene ("mainmenu", 1);
			});
			btnQuit.onClick.AddListener (Quit);
		}
		else if (gameObject.scene.name == "sandbox") {
			state = 10;
			boneMenu = false;
			pause = false;

			btnPlay.onClick.AddListener (Resume);
			btnRestart.onClick.AddListener (delegate {
				LoadScene ("sandbox", 10);
			});
			btnQuit.onClick.AddListener (delegate {
				LoadScene ("mainmenu", 1);
			});

//			player.transform.position = new Vector3 (-4, -1, 0);
//			player.transform.localScale = new Vector3 (1.771533f, 1.77153f, 1);
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
			if (Input.GetKeyDown (KeyCode.Escape)) {
				pause = !pause;
				pausePanel.SetActive (pause);
				TogglePausePlayer (pause);
				prevState = 3;
				state = 10;
			}
			break;
		//level 1
		case 4:
			break;
		case 10:
			if (Input.GetKeyDown (KeyCode.Escape)) {
				pause = !pause;
				pausePanel.SetActive (pause);
				TogglePausePlayer (pause);
				prevState = 10;
				state = 3;
			}
			if (player.transform.position.y <= -10) {
				state = 2;
				LoadScene ("gameover", 2);
			}
			if (Input.GetKeyDown (KeyCode.Tab)) {
				boneMenu = !boneMenu;
				panel.SetActive (boneMenu);
				TogglePausePlayer (boneMenu);
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("sandbox", 10);
			}
			break;
		}
	}

	void Quit () {
		Application.Quit ();
	}

	void LoadScene(string level, int s){
		SceneManager.LoadScene (level);
		state = s;
	}

	void TogglePausePlayer(bool paused){
		player.GetComponent<Platformer2DUserControl> ().enabled = !paused;
		player.GetComponent<Animator> ().enabled = !paused;
		player.GetComponent<Rigidbody2D> ().simulated = !paused;
	}

	void Resume(){
		pause = !pause;
		pausePanel.SetActive (pause);
		TogglePausePlayer (pause);
		prevState = 3;
		state = 10;
	}

	void ToggleControls(){
		controls = !controls;
		panel.SetActive (controls);
	}
}
