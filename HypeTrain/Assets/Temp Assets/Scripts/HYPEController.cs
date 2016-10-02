// AUTHOR
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class HYPEController : MonoBehaviour
{

	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject gunArm;
	[HideInInspector] public GameObject gun;
	[HideInInspector] public GameObject gunGlow;
	[HideInInspector] public Component gunScript;
	[HideInInspector] public ScoreKeeper HYPECounter;
	public GameObject HYPEPlane;

	private Transform trail;
	private string trailName;

	//Timer variables
	private float HYPETimer;
	private bool hTimerOn = false;
	public float HYPEDuration = 7f;

	public AudioClip HYPEsound;

	//Default HYPE value
	public static string HYPEMode = "orange";

	public static bool lazers = false;
	public static bool airblasts = false;
	public static bool cannon = false;
	private bool planeSpawn = true;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		trail = player.transform.Find ("HYPEtrail");
		gunArm = player.transform.Find ("GunArm").gameObject;
		gun = gunArm.transform.Find ("Gun").gameObject;
		gunGlow = gun.transform.Find ("Glow").gameObject;
		gunGlow.SetActive (false);
		gunScript = gunArm.GetComponent<gun> ();
		HYPECounter = player.GetComponent<ScoreKeeper> ();
		HYPETimer = HYPEDuration;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If HYPE is full and player is on top of a train, spawn a HYPE Plane
		if (ScoreKeeper.HYPE == 6 && player.transform.position.y > 18.5 && planeSpawn) {
			Instantiate (HYPEPlane, new Vector3 (player.transform.position.x - 50, 28, 0), Quaternion.identity);
			planeSpawn = false;
		}


		//When HYPE is full, pressing the scroll wheel activates HYPE MODE, faster fire and no reloading, HYPE reset
		if (((Input.GetButtonDown ("Interact") && Input.GetButton ("Reload")) || Input.GetButtonDown ("Fire3")) && ScoreKeeper.HYPE == 6) {
			Debug.Log ("HYPE MODE");
			gunArm.GetComponent<gun>().setHypeActive(true);
			trailName = HYPEController.HYPEMode + "Trail";
			trail.Find (trailName).GetComponent<trailToggle> ().On ();
			hTimerOn = true;
			ScoreKeeper.HYPED = true;
			//AudioSource.PlayClipAtPoint(HYPEsound, transform.position);
		}

		//Timer for how long HYPE lasts, resets gun modifications once time runs out
		if (hTimerOn) {
			HYPETimer -= Time.deltaTime;
			if (HYPETimer <= 0) {
				//Debug.Log ("hype over...");
				gunArm.GetComponent<gun>().setHypeActive(false);

				trailName = HYPEController.HYPEMode + "Trail";
				trail.Find (trailName).GetComponent<trailToggle> ().Off ();

				//Reset HYPE gauge, Timer, and gun color
				HYPECounter.incrementHype (false); //Reset HYPE, since it was activated.
				ScoreKeeper.HYPED = false;
				hTimerOn = false;
				HYPETimer = HYPEDuration;
				SpriteRenderer[] renderers = gun.GetComponentsInChildren<SpriteRenderer> ();
				renderers [1].color = Color.white;
				gunGlow.SetActive (false);
			}
		}
	}
}
