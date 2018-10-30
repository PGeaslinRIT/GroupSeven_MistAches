using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

enum Bones{
	skull,
	neck,
	back,
	ribs,
	arms,
	legs
};

public class BoneMenu : MonoBehaviour {
	//important references
	private ReferenceManager refManager;
	WeatherController myWeatherController;
	private Camera cameraObj;
	private PlatformerCharacter2D playerObj;

	// the initial max speed of the character, which then can get reduced as legs break
	// it's just equal to the maxSpeed value of the character
	float initSpeed;

	// ticking timer, for use in BoneEffects. It goes to 1000 and then resets, incrementing every Update().
	int tick;

	// a boolean of whether our dude is allowed to move. Starts false, which allows you to move
	bool stopMoving;

	// these are a collection of floats to make the camera zooming/rotating play nice=
	public float targetOrtho; // the base desired camera zoom level
	float smoothSpeed; // for smoothing out zooming
	float minOrtho; // smallest possible camera
	float maxOrtho; // largest possible camera
	public float targetRot; // target rotation for the camera

	//broken bone trackers
	public int[] brokenBones;

	public int maxBrokenBones = 3;
	public int armLegCount = 2;

	public Button btnRibs;
	public Button btnRibsUp;
	public Button btnRibsDown;
	public Button btnRibsLeft;
	public Button btnRibsRight;

	public Button btnArms;
	public Button btnArmsIn;
	public Button btnArmsDe;

	public Button btnLegs;
	public Button btnLegsIn;
	public Button btnLegsDe;

	public Button btnSkull;

	// Use this for initialization
	void Start () {
		//get reference manager
		refManager = GetComponent<ReferenceManager>();

		//get weather controller
		myWeatherController = GetComponent<WeatherController> ();

		//get references from reference manager
		playerObj = refManager.playerObj;
		cameraObj = refManager.cameraObj;

		brokenBones = new int[6];

		for (int i = 0; i < 6; i++) {
			brokenBones [i] = 0;
		}

		btnRibsUp.onClick.AddListener (delegate {
			BreakBone (Bones.ribs, Direction.up);
		});
		btnRibsDown.onClick.AddListener (delegate {
			BreakBone (Bones.ribs, Direction.down);
		});
		btnRibsLeft.onClick.AddListener (delegate {
			BreakBone (Bones.ribs, Direction.left);
		});
		btnRibsRight.onClick.AddListener (delegate {
			BreakBone (Bones.ribs, Direction.right);
		});

		btnArmsIn.onClick.AddListener (delegate {
			BreakBone (Bones.arms, true);
		});
		btnArmsDe.onClick.AddListener (delegate {
			BreakBone (Bones.arms, false);
		});

		btnLegsIn.onClick.AddListener (delegate {
			BreakBone (Bones.legs, true);
		});
		btnLegsDe.onClick.AddListener (delegate {
			BreakBone (Bones.legs, false);
		});

		btnSkull.onClick.AddListener (delegate {
			BreakBone (Bones.skull);
		});

        // stuff for manipulating the player
        initSpeed = playerObj.m_MaxSpeed;
        tick = 0;
        stopMoving = false;

        // setting default camera values
        targetOrtho = cameraObj.orthographicSize;
        smoothSpeed = 2;
        targetRot = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Alpha1)) {
			BreakBone (Bones.ribs, Direction.up);
		}
		if (Input.GetKeyUp(KeyCode.Alpha2)) {
			BreakBone (Bones.ribs, Direction.right);
		}
		if (Input.GetKeyUp(KeyCode.Alpha3)) {
			BreakBone (Bones.ribs, Direction.down);
		}
		if (Input.GetKeyUp(KeyCode.Alpha4)) {
			BreakBone (Bones.ribs, Direction.left);
		}


		if (Input.GetKeyUp(KeyCode.Alpha5)) {
			BreakBone (Bones.legs, true);
		}
		if (Input.GetKeyUp(KeyCode.Alpha6)) {
			BreakBone (Bones.legs, false);
		}


		if (Input.GetKeyUp(KeyCode.Alpha7)) {
			BreakBone (Bones.arms, true);
		}
		if (Input.GetKeyUp(KeyCode.Alpha8)) {
			BreakBone (Bones.arms, false);
		}

		if (Input.GetKeyUp(KeyCode.Alpha9)) {
			BreakBone (Bones.skull);
		}

		// BoneEffects updates every frame
		BoneEffects();

		// adjust the camera as needed
		cameraObj.orthographicSize = Mathf.MoveTowards(cameraObj.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
		cameraObj.transform.rotation = Quaternion.Euler(0, 0, targetRot); // this snaps, but it does work
		// float test = Mathf.MoveTowards(cameraObj.transform.rotation.z, targetRot, smoothSpeed * Time.deltaTime);
		// cameraObj.transform.rotation = Quaternion.Euler(0, 0, test); // this snaps instead of it being smooth, but that's a TBD issue

	}

	//Methods to break a specific bone
	//break bones that require a direction. default bone breaking method for bones without direction or increase/decrease
	void BreakBone (Bones bone, Direction dir = Direction.none) {
		if (!EnoughBones (bone)) {
			return;
		}

		bool validBreak = false;

		//change weather accordingly and check for break validity
		switch (bone) {
		case Bones.ribs:
			validBreak = myWeatherController.ChangeWind (dir);
			break;
		case Bones.skull:
			validBreak = myWeatherController.CreateLightning ();
			break;
		case Bones.neck:
			validBreak = true;
			break;
		}

		//only break the bone if the break was valid
		if (validBreak) {
			brokenBones [(int)bone]++;
		}

		btnRibs.GetComponentInChildren<Text> ().text = "Ribs:\t" + brokenBones [(int)Bones.ribs];
		btnSkull.GetComponentInChildren<Text> ().text = "Skull:\t" + brokenBones [(int)Bones.skull];
	}
	//break bones that increase/decrease
	void BreakBone(Bones bone, bool increase){

		if (!EnoughBones (bone)) {
			return;
		}

		bool validBreak = false;

		//change weather accordingly and check for break validity
		switch (bone) {
		case Bones.arms:
			validBreak = myWeatherController.ChangePrecipitation (increase);			
			break;
		case Bones.legs:
			validBreak = myWeatherController.ChangeTemperature (increase);
			break;
		}

		//only break the bone if the break was valid
		if (validBreak) {
			brokenBones [(int)bone]++;
		}
			
		btnArms.GetComponentInChildren<Text> ().text = "Arms:\t" + brokenBones [(int)Bones.arms];
		btnLegs.GetComponentInChildren<Text> ().text = "Legs:\t" + brokenBones [(int)Bones.legs];
	}

	//check that there are enough bones to still break
	bool EnoughBones (Bones bone){
		int breakLimit = maxBrokenBones;

		//arms and legs have a higher limit than other bones
		if (bone == Bones.arms || bone == Bones.legs) {
			breakLimit *= armLegCount;
		}

		//check if bone has reached break limit
		return (brokenBones[(int)bone] < breakLimit);
	}

	// method to determine the exact negative effects of bones breaking. This should be called every update, as it has some random effects that happen based on ticks
	// the effects are:
	// ribs - character randomly stops sometimes, wheezing plays (assuming I can get audio working)
	// arms - currently nothing, TBD discussion
	// legs - character's speed is reduced by X% for Y bones broken
	// skull - camera zooms in and out, white light, high-pitched ringing
	// neck - camera wobbles
	// back - TBD, since we're in 2D now
	public void BoneEffects()
	{
		// increment the timer
		tick++;

		// if stopMoving is true, stop the player
		// after at most 4 seconds, let the player move again
		if (stopMoving == true)
		{
			playerObj.m_MaxSpeed = 0;
			if (tick % 120 == 0)
			{
				stopMoving = false;
			}
		}

		// LEGS first - reduce player speed by 1 (out of 7ish) for each broken leg, assuming the player is actually allowed to move
		if (stopMoving == false)
		{
			playerObj.m_MaxSpeed = (initSpeed - (brokenBones[5] * 1));
		}

		// ARMS are currently nothign, as breaking your arms doesn't do anything
		// TODO: make arms do something, so breaking them means something

		// RIBS - every ~15 seconds, ish, play a wheezing sound and set the max speed to 0
		// to clarify exactly what this if statement wants:
		//  - you need at least one broken rib
		//  - stopMoving has to be false, you need to be able to move
		//  - then it takes 15 seconds, subtracts or adds up to 3 seconds, subtracts another second for each broken bone: every time that passes, it is true
		// basically, every 15ish seconds you stop moving, and that number gets smaller the more ribs you break
		if (brokenBones[3] != 0 && stopMoving == false && tick % ((450 - brokenBones[3] * 30) + Random.Range(-90, 90)) == 0)
		{
			stopMoving = true; // the player stops moving
			// TODO: add wheezing sounds effects
			Debug.Log("ribs broken");
		}

		// SKULL - every ~10 seconds, if your skull is broken, the camera zooms in or out a bit. The more broken your skull, the more frequently it changes
		// the timer mather is very similar to how the ribs work
		if (brokenBones[0] != 0 && tick % (300 - brokenBones[0] * 60) + Random.Range(-90, 90) == 0)
		{
			targetOrtho += Random.Range(-2f, 2f); // the camera zoom changes a bit, depending
			if (targetOrtho <= 2) // camera view can't go negative
			{
				targetOrtho = 2f;
			}
			if (targetOrtho >= 10.0) // and can't zoom insanely far out, either
			{
				targetOrtho = 10.0f;
			}
		}

		// NECK - once broken, every ~10 seconds, the camera just tilts back and forth, like your neck is broken
		// the more your neck is broken, the more frequeny the tilting, and the deeper the tilts
		// the timer math is the same as the skull, which is very similar to the ribs
		if (brokenBones[1] != 0 && tick % (300 - brokenBones[1] * 60) + Random.Range(-90, 90) == 0)
		{
			targetRot += Random.Range(-10f, 10f);
			if (targetRot <= (-10 * brokenBones[1])) // camera can't swing more than 15 degrees per bone broken
			{
				targetRot = (-10 * brokenBones[1]);
			}
			if (targetRot >= (10 * brokenBones[1])) // same goes for here, but in the opposite direction
			{
				targetRot = (10 * brokenBones[1]);
			}
		}
	}
}
