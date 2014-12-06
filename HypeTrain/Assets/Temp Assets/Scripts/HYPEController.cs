// AUTHOR
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class HYPEController : MonoBehaviour {

	public GameObject player;
	public GameObject revolver;
	public Component gunScript;
	public ScoreKeeper HYPECounter;

	//Timer variables
	private float HYPETimer;
	private bool hTimerOn = false;
	public float HYPEDuration = 7f; 

	//Default HYPE value
	public static string HYPEMode = "red";

	public static bool lazers = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("character");
		revolver = GameObject.Find ("character/gun");
		gunScript = revolver.GetComponent<gun> ();
		HYPECounter = GameObject.Find("character").GetComponent<ScoreKeeper>();
		HYPETimer = HYPEDuration;
	}
	
	// Update is called once per frame
	void Update () {
		//When HYPE is full, pressing the scroll wheel activates HYPE MODE, faster fire and no reloading, HYPE reset
		if (Input.GetButtonDown ("Fire3") && ScoreKeeper.HYPE == "HYPE") {
			Debug.Log ("HYPE MODE");

			if (HYPEMode == "red"){ //Enable rapid fire
				SpriteRenderer[] renderers = revolver.GetComponentsInChildren<SpriteRenderer>();
				renderers[1].color = Color.red;
				revolver.GetComponent<gun> ().magSize = 100;
				revolver.GetComponent<gun> ().inMag = revolver.GetComponent<gun> ().magSize;
				revolver.GetComponent<gun> ().adjustCounter(revolver.GetComponent<gun> ().inMag);
				revolver.GetComponent<gun> ().interShotDelay = .1f;
				revolver.GetComponent<gun> ().rTimerOn = false;
				revolver.GetComponent<gun> ().kickForce = 300f;
			}

			if (HYPEMode == "purple"){ //Enable lazers, disable bullets
				SpriteRenderer[] renderers = revolver.GetComponentsInChildren<SpriteRenderer>();
				renderers[1].color = new Vector4(114, 0, 255, 255);
				lazers = true;
				//revolver.GetComponent<gun> (). DISABLE BULLETS SOMEHOW
				//ENABLE LASERS SOMEHOW
			}
			hTimerOn = true;
			ScoreKeeper.HYPED = true;
		}

		//Timer for how long HYPE lasts, resets gun modifications once time runs out
		if (hTimerOn) {
			HYPETimer -= Time.deltaTime;
			if (HYPETimer <= 0) {
				Debug.Log ("hype over...");

				if(HYPEMode == "red"){	//Reset gun values
					revolver.GetComponent<gun> ().magSize = 4;
					revolver.GetComponent<gun> ().inMag = revolver.GetComponent<gun> ().magSize;
					revolver.GetComponent<gun> ().adjustCounter(revolver.GetComponent<gun> ().inMag);
					revolver.GetComponent<gun> ().interShotDelay = .5f;
					revolver.GetComponent<gun> ().kickForce = 1000f;
				}

				if (HYPEMode == "purple"){ //Disable lazers and reenable bullets
					lazers = false;
					//revolver.GetComponent<gun> (). REENABLE BULLETS SOMEHOW
				}

				//Reset HYPE gauge, Timer, and gun color
				HYPECounter.incrementHype(false); //Reset HYPE, since it was activated.
				ScoreKeeper.HYPED = false;
				hTimerOn = false;
				HYPETimer = HYPEDuration;
				SpriteRenderer[] renderers = revolver.GetComponentsInChildren<SpriteRenderer>();
				renderers[1].color = Color.white;
			}
		}
	}
}
