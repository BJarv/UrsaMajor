using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gun : MonoBehaviour {
	//Bullet variables
	public float bulletSpeed = 500f;
	public float kickForce = 1000f;
	public int magSize = 3;
	public int inMag;
	public GameObject bull1;
	public GameObject bull2;
	public GameObject bull3;
	public GameObject bull4;
	public Rigidbody2D bullet;
	//Timing Variables
	private float reloadTimer;
	private float shotTimer;
	[HideInInspector] public bool rTimerOn = false;
	private bool sTimerOn = false;
	public float reloadTime = 2f;
	public float interShotDelay = .5f;

	private GameObject player = null;
	private GameObject shootFrom = null;
	public AudioClip gunshot;
	public AudioClip reload;

	public GameObject shotParticles;
	public GameObject airShotParticles;
	public LayerMask airBlastMask;

	/*WHAT IS THE GUN POINTING AT SO TUCKER CAN GO GET EM
	private Vector3 pointingDirection; 
	private RaycastHit pointingAt = new RaycastHit();
	private GameObject tucker;
	Vector3 mouseWorldPosition;*/

	[HideInInspector] public ScoreKeeper HYPECounter;

	// Use this for initialization
	void Start () {
		inMag = magSize;
		reloadTimer = reloadTime;
		shotTimer = interShotDelay;
		player = GameObject.Find("character");
		shootFrom = GameObject.Find("barrelTip");
		HYPECounter = GameObject.Find("character").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {

		//rotation
		Vector3 mousePos = retical.recPos;
		mousePos.z = 5.23f;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	
		//Gun image will flip depending on where the mouse is relative to the player
		if (mousePos.x - 15 > player.transform.position.x)
						transform.localScale = new Vector3(1,1,1);
				else
						transform.localScale = new Vector3(1,-1,1);
		if ((Input.GetButton ("Fire1") || Input.GetAxis ("RTrig") > 0.1) && Firable () && !HYPEController.lazers && !HYPEController.airblasts && !PlayerHealth.alreadyDying) {
			//shoot bullet
			AudioSource.PlayClipAtPoint(gunshot, transform.position);

			sTimerOn = true;
			inMag -= 1;
			adjustCounter(inMag);
			var pos = retical.recPos;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);

			var q = Quaternion.FromToRotation(Vector3.up, pos - shootFrom.transform.position);
			Rigidbody2D go = Instantiate(bullet, shootFrom.transform.position, q) as Rigidbody2D;
			go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * bulletSpeed);

			GameObject particles = (GameObject)Instantiate(shotParticles, shootFrom.transform.position, shootFrom.transform.rotation);
			particles.GetComponent<ParticleSystem>().Play ();
			Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);

			if(inMag <= 0){
				AudioSource.PlayClipAtPoint(reload, transform.position);
				rTimerOn = true;
			}

			//if(player.GetComponent<)
			if(!player.GetComponent<CharControl>().isGrounded()){
				//Debug.Log(new Vector2(go.transform.up.x * -kickForce, go.transform.up.y * -kickForce));
				player.GetComponent<Rigidbody2D>().AddForce (new Vector2(go.transform.up.x * -kickForce, go.transform.up.y * -kickForce ));
			}
		}

		//If the airblasts power is on
		else if ((Input.GetButton ("Fire1") || Input.GetAxis ("RTrig") > 0.1) && Firable () && !HYPEController.lazers && HYPEController.airblasts && !PlayerHealth.alreadyDying) {
			sTimerOn = true;
			//Get reticle position
			var pos = retical.recPos;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);

			//Get direction between the gun and the reticle
			Vector2 direction = (pos - shootFrom.transform.position);

			//Declare RayCast and store info, draw RayCast
			RaycastHit2D hit = Physics2D.Raycast(shootFrom.transform.position, direction, 5f, airBlastMask);
			Debug.DrawRay (shootFrom.transform.position, direction);

			//Create particles for shot
			GameObject particles = (GameObject)Instantiate(airShotParticles, shootFrom.transform.position, shootFrom.transform.rotation);
			particles.GetComponent<ParticleSystem>().Play ();
			Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);

			//Cast a ray from the shootFrom in the direction of the reticle
			if(hit == true)
			{
				Debug.Log ("HIT");
				hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 2500);
			}

		}

		if (Input.GetButtonDown ("Reload") && inMag != magSize && !rTimerOn) {
			AudioSource.PlayClipAtPoint(reload, transform.position);
			//play reload anim
			rTimerOn = true;
		}

		//Timer for how long reload takes
		if (rTimerOn) {
			reloadTimer -= Time.deltaTime;
			if(reloadTimer <= 0) {
				rTimerOn = false;
				inMag = magSize;
				adjustCounter(inMag);
				reloadTimer = reloadTime;
			}
		}

		//Timer for how long before you can shoot again
		if (sTimerOn) {
			shotTimer -= Time.deltaTime;
			if(shotTimer <= 0) {
				sTimerOn = false;
				shotTimer = interShotDelay;
			}
		}

	}

	/*void FixedUpdate() {

		mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Cast a ray from the player thru the gun
		//pointingDirection = (retical.recPos - transform.position);
		pointingDirection = (mouseWorldPosition - transform.position);
		//If this returns true it hit something
		Debug.DrawRay (transform.position, pointingDirection, Color.red);
		Physics.Raycast (transform.position, pointingDirection, out pointingAt, 20f);
		//Debug.Log (pointingAt);
		if (Physics.Raycast (transform.position, pointingDirection, out pointingAt)) {
		//if (Physics.Raycast()) {
			Debug.Log ("GUN: I'm pointing at something.");
			if (pointingAt.collider.tag.Equals ("enemy")) {
				Debug.Log ("GUN: It's an enemy.");
				if (tucker = GameObject.Find ("Tucker")) {
					tucker.GetComponent<TuckerController> ().changeTarget (pointingAt.collider.gameObject);
				}
			}
		} else {
			Debug.Log ("false");
		}
	}*/

	public void adjustCounter(int currBulls)
	{
		if (currBulls == 4) {
			bull1.SetActive (true);
			bull2.SetActive (true);
			bull3.SetActive (true);
			bull4.SetActive (true);
		}
		if (currBulls == 3) {
			bull1.SetActive (true);
			bull2.SetActive (true);
			bull3.SetActive (true);
			bull4.SetActive (false);
		}
		if (currBulls == 2) {
			bull1.SetActive (true);
			bull2.SetActive (true);
			bull3.SetActive (false);
			bull4.SetActive (false);
		}
		if (currBulls == 1) {
			bull1.SetActive (true);
			bull2.SetActive (false);
			bull3.SetActive (false);
			bull4.SetActive (false);
		}
		if (currBulls == 0) {
			bull1.SetActive (false);
			bull2.SetActive (false);
			bull3.SetActive (false);
			bull4.SetActive (false);
		}
		//Bullet counter modifications for during and after HYPEmode
		if (ScoreKeeper.HYPED) {
			bull1.SetActive (true);
			bull2.SetActive (true);
			bull3.SetActive (true);
			bull4.SetActive (true);
			bull1.GetComponent<RawImage>().color = Color.red;
			bull2.GetComponent<RawImage>().color = Color.red;
			bull3.GetComponent<RawImage>().color = Color.red;
			bull4.GetComponent<RawImage>().color = Color.red;
		}
		if (!ScoreKeeper.HYPED) {
			bull1.GetComponent<RawImage>().color = Color.white;
			bull2.GetComponent<RawImage>().color = Color.white;
			bull3.GetComponent<RawImage>().color = Color.white;
			bull4.GetComponent<RawImage>().color = Color.white;
		}
	}

	bool Firable() {
		return (inMag != 0 && !rTimerOn && !sTimerOn);
	}
}
