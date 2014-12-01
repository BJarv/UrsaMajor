using UnityEngine;
using System.Collections;

public class HYPEController : MonoBehaviour {

	public GameObject player;
	public GameObject revolver;
	public ScoreKeeper HYPECounter;

	//Timer variables
	private float HYPETimer;
	private bool hTimerOn = false;
	public float HYPEDuration = 7f; 

	//Default HYPE value
	public static string HYPEMode = "red";

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("character");
		//revolver = player.GetComponent
		HYPECounter = GameObject.Find("character").GetComponent<ScoreKeeper>();
		HYPETimer = HYPEDuration;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire3") && ScoreKeeper.HYPE == "HYPE") {
			Debug.Log ("HYPE MODE");
			//SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
			//renderers[1].color = Color.red;
			//magSize = 100;
			//inMag = magSize;
			//adjustCounter(inMag);
			//interShotDelay = .3f;
			//hTimerOn = true;
			ScoreKeeper.HYPED = true;
		}

		//Timer for how long HYPE lasts, resets gun modifications once time runs out
		if (hTimerOn) {
			HYPETimer -= Time.deltaTime;
			if (HYPETimer <= 0) {
				Debug.Log ("hype over...");
				//Reset gun values
				//magSize = 4;
				//inMag = magSize;
				//adjustCounter(inMag);
				//interShotDelay = .5f;
				//SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
				//renderers[1].color = Color.white;

				//Reset HYPE gauge and Timer
				HYPECounter.incrementHype(false); //Reset HYPE, since it was activated.
				ScoreKeeper.HYPED = false;
				//hTimerOn = false;
				HYPETimer = HYPEDuration;
			}
		}
	}
}
