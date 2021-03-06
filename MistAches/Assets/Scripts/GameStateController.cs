﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class GameStateController : MonoBehaviour {

	private ReferenceManager refManager;

	WeatherController weatherController;

	private PlatformerCharacter2D playerObj;
	private GameObject goalObj;

	public GameObject panel;
	public GameObject pausePanel;
	public GameObject nextPanel;

	private List<GameObject> interactableObjects = new List<GameObject> (); //set manually for now

	public Button btnPlay;
	public Button btnQuit;
	public Button btnRestart;
	public Button btnControls;

	public GameObject ribPanel;
	public Button btnRibs;
	public Button btnUp;
	public Button btnDown;
	public Button btnLeft;
	public Button btnRight;

	public GameObject armPanel;
	public Button btnArms;
	public Button btnArmsIn;
	public Button btnArmsDe;

	public GameObject legPanel;
	public Button btnLegs;
	public Button btnLegsIn;
	public Button btnLegsDe;

	public Button btnSkull;

	public Button btnBack1;
	public Button btnBack2;
	public Button btnBack3;

	public Button btnNext;
	public Button btnNextMenu;
	public Button btnNextQuit;

	public int state;
	private int prevState;
	private bool boneMenu;
	private bool pause;
	private bool controls;

	// Use this for initialization
	void Start () {
		refManager = GetComponent<ReferenceManager> ();

		if (gameObject.scene.name == "mainmenu") {
			state = 1;
			controls = false;
			btnPlay.onClick.AddListener (delegate {
				LoadScene ("level1", 7);
			});
			btnControls.onClick.AddListener (ToggleControls);
			btnRestart.onClick.AddListener (ToggleControls);
			btnQuit.onClick.AddListener (Quit);
		} else if (gameObject.scene.name == "gameover") {
			state = 2;
			btnRestart.onClick.AddListener (delegate {
				LoadScene ("mainmenu", 1);
			});
			btnQuit.onClick.AddListener (Quit);
		} else if (gameObject.scene.name == "win") {
			state = 6;
			btnRestart.onClick.AddListener (delegate {
				LoadScene ("mainmenu", 1);
			});
			btnQuit.onClick.AddListener (Quit);
		} else if (gameObject.scene.name == "level1") {
			InitLevel (7);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("level1", 7);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("level2", 8);
			});
		} else if (gameObject.scene.name == "level2") {
			InitLevel (8);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("level2", 8);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("level3", 9);
			});
		} else if (gameObject.scene.name == "level3") {
			InitLevel (9);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("level3", 9);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("level4", 10);
			});
		} else if (gameObject.scene.name == "level4") {
			InitLevel (10);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("level4", 10);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("win", 6);
			});
		} 

		// sandbox and test level
		else if (gameObject.scene.name == "sandbox") {
			InitLevel (20);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("sandbox", 20);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("testlevel", 21);
			});
		} else if (gameObject.scene.name == "testlevel") {
			InitLevel (21);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("testlevel", 21);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("mainmenu", 1);
			});
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		//main menu
		default:
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
				TogglePauseplayerObj (pause);
				ToggleBlockPause (pause);
				state = prevState;
				prevState = 3;
			}
			break;
		//bone menus
		case 4:
			break;
		//next level
		case 5:
			break;
		//win
		case 6:
			break;
		//level 1
		case 7:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("level1", 7);
			}
			if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
				prevState = state;
				state = 5;
				nextPanel.SetActive (true);
				TogglePauseplayerObj (true);
				ToggleBlockPause (true);
			}
			break;
		//level 2
		case 8:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("level2", 8);
			}
			if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
				prevState = state;
				state = 5;
				nextPanel.SetActive (true);
				TogglePauseplayerObj (true);
				ToggleBlockPause (true);
			}
			break;
		//level 3
		case 9:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("level3", 9);
			}if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
				prevState = state;
				state = 5;
				nextPanel.SetActive (true);
				TogglePauseplayerObj (true);
				ToggleBlockPause (true);
			}
			break;
		//level 4
		case 10:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("level4", 10);
			}
			if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
				prevState = state;
				LoadScene ("win", 6);
			}
			break;
		
		//sandbox and test level
		case 20:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("sandbox", 20);
			}
			if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
				prevState = state;
				state = 5;
				nextPanel.SetActive (true);
				TogglePauseplayerObj (true);
				ToggleBlockPause (true);
			}
			break;
		case 21:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("testlevel", 21);
			}
			if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
				prevState = state;
				state = 5;
				nextPanel.SetActive (true);
				TogglePauseplayerObj (true);
				ToggleBlockPause (true);
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

	void TogglePauseplayerObj(bool paused){
		playerObj.GetComponent<Platformer2DUserControl> ().enabled = !paused;
		playerObj.GetComponent<Animator> ().enabled = !paused;
		playerObj.GetComponent<Rigidbody2D> ().simulated = !paused;
		weatherController.enabled = !paused;
	}

	void ToggleBlockPause(bool paused){
		for (int i = 0; i < interactableObjects.Count; i++) {
			interactableObjects [i].GetComponent<Rigidbody2D> ().simulated = !paused;
		}
	}

	void Resume(){
		pause = !pause;
		pausePanel.SetActive (pause);
		TogglePauseplayerObj (pause);
		state = prevState;
		prevState = 3;
	}

	void ToggleControls(){
		controls = !controls;
		panel.SetActive (controls);
	}

	void ToggleBoneMenu(){
		boneMenu = !boneMenu;
		panel.SetActive (boneMenu);
		TogglePauseplayerObj (boneMenu);
		ToggleBlockPause (pause);
	}

	void ToggleRibMenu(bool on){
		if (on) {
			prevState = state;
			state = 4;
			ribPanel.SetActive (true);
			panel.SetActive (false);
		} else {
			state = prevState;
			prevState = 4;
			ribPanel.SetActive (false);
			ToggleBoneMenu ();
		}
	}

	void ToggleArmMenu(bool on){
		if (on) {
			prevState = state;
			state = 4;
			armPanel.SetActive (true);
			panel.SetActive (false);
		} else {
			state = prevState;
			prevState = 4;
			armPanel.SetActive (false);
			ToggleBoneMenu ();
		}
	}

	void ToggleLegMenu(bool on){
		if (on) {
			prevState = state;
			state = 4;
			legPanel.SetActive (true);
			panel.SetActive (false);
		} else {
			state = prevState;
			prevState = 4;
			legPanel.SetActive (false);
			ToggleBoneMenu ();
		}
	}

	void ToggleRibMenuBack(){
		ToggleRibMenu (false);
		ToggleBoneMenu ();
	}

	void ToggleArmMenuBack(){
		ToggleArmMenu (false);
		ToggleBoneMenu ();
	}

	void ToggleLegMenuBack(){
		ToggleLegMenu (false);
		ToggleBoneMenu ();
	}

	//HELPER FUNCTIONS
	void InitLevel(int s){
		playerObj = refManager.playerObj;
		goalObj = refManager.goalObj;
		interactableObjects = refManager.interactableObjects;

		weatherController = gameObject.GetComponent<WeatherController> ();
		state = s;
		boneMenu = false;
		pause = false;

		btnPlay.onClick.AddListener (Resume);
		btnQuit.onClick.AddListener (delegate {
			LoadScene ("mainmenu", 1);
		});

		btnRibs.onClick.AddListener (delegate {
			ToggleRibMenu (true);
		});

		btnUp.onClick.AddListener (delegate {
			ToggleRibMenu (false);
		});
		btnDown.onClick.AddListener (delegate {
			ToggleRibMenu (false);
		});
		btnLeft.onClick.AddListener (delegate {
			ToggleRibMenu (false);
		});
		btnRight.onClick.AddListener (delegate {
			ToggleRibMenu (false);
		});

		btnArms.onClick.AddListener (delegate {
			ToggleArmMenu (true);
		});
		btnArmsIn.onClick.AddListener (delegate {
			ToggleArmMenu (false);
		});
		btnArmsDe.onClick.AddListener (delegate {
			ToggleArmMenu (false);
		});

		btnLegs.onClick.AddListener (delegate {
			ToggleLegMenu (true);
		});
		btnLegsIn.onClick.AddListener (delegate {
			ToggleLegMenu (false);
		});
		btnLegsDe.onClick.AddListener (delegate {
			ToggleLegMenu (false);
		});

		btnSkull.onClick.AddListener (ToggleBoneMenu);

		btnBack1.onClick.AddListener (ToggleRibMenuBack);
		btnBack2.onClick.AddListener (ToggleArmMenuBack);
		btnBack3.onClick.AddListener (ToggleLegMenuBack);

		btnNextMenu.onClick.AddListener (delegate {
			LoadScene ("mainmenu", 1);
		});
		btnNextQuit.onClick.AddListener (Quit);
	}

	void UpdateLevel (){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pause = !pause;
			pausePanel.SetActive (pause);
			TogglePauseplayerObj (pause);
			ToggleBlockPause (pause);
			prevState = state;
			state = 3;
		}
		if (playerObj.transform.position.y <= -10) {
			state = 2;
			LoadScene ("gameover", 2);
		}
		if (Input.GetKeyDown (KeyCode.Tab)) {
			boneMenu = !boneMenu;
			panel.SetActive (boneMenu);
			TogglePauseplayerObj (boneMenu);
			ToggleBlockPause (boneMenu);
		}
	}
}
