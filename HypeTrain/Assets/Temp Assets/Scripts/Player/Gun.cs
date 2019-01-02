using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : LogController {

    public bool oldGunLogic = false;

	//Bullet variables
	public float bulletSpeed = 500f;
	public float kickForce = 1000f;
	public int magSize = 3;
	public int inMag;
    public GameObject[] UIBullets;
	public Rigidbody2D bullet;
	public Rigidbody2D key;
	//Timing Variables
	private bool canShoot = true;
	public float reloadTime = 2f;
	public float interShotDelay = .1f;

	public GameObject gunGlow;
	public GameObject gunUnderlay;

	private GameObject player = null;
	private GameObject shootFrom = null;
    private PlayerCharacter playerScript;
	public AudioClip gunshot;
	public AudioClip reload;

	private Transform trail;
	private string trailName;

	//For changing gun sprite back after firing key
	public static bool keyLoaded = false;
	public SpriteRenderer gunSprite;
	public Sprite gunRegular;

	public GameObject shotParticles;

	public GameObject equippedHypePrefab;
	private GameObject equippedHype;

	public GameObject HYPEPlane;

	public bool hypeActive = false;

	private bool planeSpawn = true;

	/*WHAT IS THE GUN POINTING AT SO TUCKER CAN GO GET EM
	private Vector3 pointingDirection; 
	private RaycastHit pointingAt = new RaycastHit();
	private GameObject tucker;
	Vector3 mouseWorldPosition;*/

	[HideInInspector] public ScoreKeeper HYPECounter;

	// Use this for initialization
	void Start () {
        if (oldGunLogic) {
            interShotDelay = 0.26f;
            bulletSpeed = 1200f;
            magSize = 4;
            UIBullets[4].SetActive(false);
            UIBullets[5].SetActive(false);
        }

        keyLoaded = false;
        inMag = magSize;
        player = transform.parent.gameObject;
		trail = player.transform.Find ("HYPEtrail");
        shootFrom = transform.Find("Gun/BarrelTip").gameObject;
		HYPECounter = player.GetComponent<ScoreKeeper>();
		gunSprite = shootFrom.transform.parent.GetComponent<SpriteRenderer>();
        playerScript = transform.parent.GetComponent<PlayerCharacter>();
		gunGlow = transform.Find ("Gun").Find("Glow").gameObject;
		gunUnderlay = transform.Find ("Gun").Find("GunUnderlay").gameObject;
		gunGlow.SetActive (false);
		if (equippedHypePrefab != null) {
			Debug.Log ("Hype is being spawned.");
			equippedHype = (GameObject) Instantiate (equippedHypePrefab, transform.parent) as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
        
        //If HYPE is full and player is on top of a train, spawn a HYPE Plane
        if (ScoreKeeper.HYPE == 6 && player.transform.position.y > 18.5 && planeSpawn) {
			Instantiate (HYPEPlane, new Vector3 (player.transform.position.x - 50, 28, 0), Quaternion.identity);
			planeSpawn = false;
		}

		//When HYPE is full, pressing the scroll wheel activates HYPE MODE
		if (Input.GetButtonDown ("Fire3") && ScoreKeeper.HYPE == 6) {
			BeginHype ();
		}

        //GUN / ARM ROTATION
        if (oldGunLogic) {
            Vector3 mousePos = Reticle.recPos;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


            //Gun image will flip depending on where the mouse is relative to the player
            if (mousePos.x - 15 > player.transform.position.x) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(1, -1, 1);
        }

        //GUN / ARM DIRECTION
        //Orient the arm in the appropriate cardinal direction based on vertical input.
        if (!oldGunLogic) {
            int verticatInput = (int)Input.GetAxisRaw("Vertical");
            switch (verticatInput) {
                case -1:
                    transform.localEulerAngles = new Vector3(0, 0, -90);
                    break;
                case 0:
                    transform.localEulerAngles = new Vector3(0, transform.rotation.y, 0);
                    break;
                case 1:
                    transform.localEulerAngles = new Vector3(0, 0, 90);
                    break;
            }
        }
        
        
		if (ShootButton() && Firable ()) {
			
			if (equippedHype != null && hypeActive) {
				float shotTime = equippedHype.GetComponent<Hype> ().Shoot (shootFrom);
				StartCoroutine(WaitAndEnableShoot (shotTime));
				kickIfAirbourne (equippedHype.GetComponent<Hype>().GetKickForce());
				return;
			}
			else
				Shoot ();

		}

        //MANUAL RELOAD
        //If reload is pressed and clip is not full, reload
		if (Input.GetButtonDown ("Reload") && inMag != magSize) {
			StartCoroutine (WaitAndReload ());
		}

	}

	public void Shoot() {
		//shoot bullet
		AudioSource.PlayClipAtPoint(gunshot, Camera.main.transform.position);

		StartCoroutine(WaitAndEnableShoot (interShotDelay));
		inMag--;
		adjustCounter(inMag);
		var pos = Reticle.recPos;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint(pos);

		var q = Quaternion.FromToRotation(Vector3.up, pos - shootFrom.transform.position);

		Rigidbody2D toShoot = bullet;
		//Fire the key instead of a bullet if it's loaded
		if(keyLoaded){
			toShoot = key;
			gunSprite.sprite = gunRegular;
			keyLoaded = false;
		}
        if (oldGunLogic)
        {
            Rigidbody2D go = Instantiate(toShoot, shootFrom.transform.position, q) as Rigidbody2D;
            go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * bulletSpeed);
        }
        else {
            Rigidbody2D go = Instantiate(toShoot, shootFrom.transform.position, shootFrom.transform.rotation) as Rigidbody2D;
            go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * bulletSpeed);
        }
		
		GameObject particles = (GameObject)Instantiate(shotParticles, shootFrom.transform.position, shootFrom.transform.rotation);
		particles.GetComponent<ParticleSystem>().Play ();
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.startLifetime.constant);

        //Firing knockback
		if(!playerScript.IsGrounded()){
			//Debug.Log(new Vector2(go.transform.up.x * -kickForce, go.transform.up.y * -kickForce));
			//player.GetComponent<Rigidbody2D>().AddForce (new Vector2(go.transform.up.x * -kickForce, go.transform.up.y * -kickForce ));
		}

        //If clip is empty, auto-reload
        if (inMag <= 0)
        {
            StartCoroutine(WaitAndReload());
        }
    }

	/*void FixedUpdate() {

		mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//Cast a ray from the player thru the gun
		//pointingDirection = (Reticle.recPos - transform.position);
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

	public void kickIfAirbourne(float kickForce){
		//Get reticle position
		var pos = Camera.main.ScreenToWorldPoint(Reticle.recPos);
		//Get direction between reticle position and the gunpoint
		Vector2 direction = (pos - shootFrom.transform.position);

		//If the player is airbourne, send add force in the opposite direction of the shot
		if(!player.GetComponent<PlayerCharacter>().IsGrounded()){
			player.GetComponent<Rigidbody2D>().AddForce (-direction * kickForce);
		}
	}

	public void adjustCounter(int currBulls){
        //If value is equal to the magSize, re-enable all UI bullets
        if (currBulls == magSize) {
            for (int i = 0; i < currBulls; i++)
            {
                UIBullets[i].SetActive(true);
            }
        }
        //Otherwise, deactivate the bullet that was just expended
        else
        {
            UIBullets[currBulls].SetActive(false);
        }
	}

	public void HypeCounter() {
		//Bullet counter modifications for during and after HYPEmode
		if (ScoreKeeper.HYPED) {
            for (int i = 0; i < magSize; i++)
            {
                UIBullets[i].SetActive(true);
                UIBullets[i].GetComponent<RawImage>().color = Color.red;
            }
		}
		if (!ScoreKeeper.HYPED) {
            for (int i = 0; i < magSize; i++)
            {
                UIBullets[i].GetComponent<RawImage>().color = Color.white;
            }
        }
	}

	bool ShootButton() {
		return (Input.GetButton ("Fire1") || Input.GetAxis ("RTrig") > 0.1);
	}

	bool Firable() {
		return (inMag > 0 && canShoot && !PlayerHealth.alreadyDying && !Popup.paused);
	}

	void BeginHype() {
        EventManager.TriggerEvent("StartHYPE");
		gunGlow.SetActive (true);
		gunGlow.GetComponent<SpriteRenderer> ().color = equippedHype.GetComponent<Hype> ().GetColor ();
		gunUnderlay.GetComponent<SpriteRenderer> ().color = equippedHype.GetComponent<Hype> ().GetColor ();
		hypeActive = true;
		trail.Find ("redTrail").GetComponent<TrailToggle> ().On ();
		StartCoroutine (WaitAndEndHype());
		ScoreKeeper.HYPED = true;
		HypeCounter ();
		//AudioSource.PlayClipAtPoint(HYPEsound, transform.position);
	}

	IEnumerator WaitAndEnableShoot(float howLong) {
		canShoot = false;
		yield return new WaitForSeconds (howLong);
		canShoot = true;
	}

	IEnumerator WaitAndEndHype() {
		yield return new WaitForSeconds (7f);
		gunGlow.SetActive (false);
		gunUnderlay.GetComponent<SpriteRenderer> ().color = Color.white;
		hypeActive = false;
		trail.Find ("redTrail").GetComponent<TrailToggle> ().Off ();
		ScoreKeeper.HYPED = false;
		HypeCounter ();
		HYPECounter.incrementHype (false); //Reset HYPE, since it was activated.
	}

	IEnumerator WaitAndReload() {
		AudioSource.PlayClipAtPoint(reload, Camera.main.transform.position);
		yield return new WaitForSeconds (reloadTime);
		inMag = magSize;
		adjustCounter(inMag);
	}
}
