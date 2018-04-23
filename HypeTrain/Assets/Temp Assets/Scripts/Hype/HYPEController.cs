// AUTHOR
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class HYPEController : LogController
{

	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject gunArm;
	[HideInInspector] public GameObject gun;
	[HideInInspector] public GameObject gunGlow;
	[HideInInspector] public Component gunScript;
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
		gunScript = gunArm.GetComponent<Gun> ();
		HYPETimer = HYPEDuration;
	}
	
	// Update is called once per frame
	void Update ()
	{
		

		//Timer for how long HYPE lasts, resets gun modifications once time runs out
		if (hTimerOn) {
			HYPETimer -= Time.deltaTime;
			if (HYPETimer <= 0) {
				
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
