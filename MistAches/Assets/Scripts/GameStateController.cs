using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class GameStateController : MonoBehaviour {

	WeatherController weatherController;

	public GameObject player;

	public GameObject panel;
	public GameObject pausePanel;

	public List<GameObject> blocks;

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
			weatherController = gameObject.GetComponent<WeatherController> ();
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
				ToggleBlockPause (pause);
				prevState = state;
				state = 10;
			}
			break;
		//bone menus
		case 4:
			break;
		case 10:
			if (Input.GetKeyDown (KeyCode.Escape)) {
				pause = !pause;
				pausePanel.SetActive (pause);
				TogglePausePlayer (pause);
				ToggleBlockPause (pause);
				prevState = state;
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
				ToggleBlockPause (pause);
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
		weatherController.enabled = !paused;
	}

	void ToggleBlockPause(bool paused){
		for (int i = 0; i < blocks.Count; i++) {
			blocks [i].GetComponent<Rigidbody2D> ().isKinematic = !paused;
		}
	}

	void Resume(){
		pause = !pause;
		pausePanel.SetActive (pause);
		TogglePausePlayer (pause);
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
		TogglePausePlayer (boneMenu);
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
}
