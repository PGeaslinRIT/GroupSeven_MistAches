using System.Collections;
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
				LoadScene ("sandbox", 10);
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
		} else if (gameObject.scene.name == "sandbox") {
			InitLevel (10);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("sandbox", 10);
			});

			btnNext.onClick.AddListener (delegate {
				LoadScene ("testlevel", 11);
			});
		} else if (gameObject.scene.name == "testlevel") {
			InitLevel (11);

			btnRestart.onClick.AddListener (delegate {
				LoadScene ("testlevel", 11);
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
				prevState = state;
				state = 10;
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
			break;
		//level 2
		case 8:
			break;
		//level 3
		case 9:
			break;
		case 10:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("sandbox", 10);
			}
			break;
		case 11:
			UpdateLevel ();
			if (Input.GetKeyDown (KeyCode.R)) {
				LoadScene ("testlevel", 11);
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
		if (playerObj.GetComponent<Rigidbody2D> ().IsTouching(goalObj.GetComponent<BoxCollider2D> ())) {
			prevState = state;
			state = 5;
			nextPanel.SetActive (true);
			TogglePauseplayerObj (true);
			ToggleBlockPause (true);
		}
	}
}
