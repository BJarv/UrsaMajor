using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

	public float bulletSpeed = 500f;
	public float kickForce = 100f;
	public int magSize = 3;
	public float reloadTime = 2f;
	private int inMag;
	private float reloadTimer;
	public Rigidbody2D bullet;
	private bool rTimerOn = false;
	public float interShotDelay = .5f;
	private bool sTimerOn = false;
	private float shotTimer;
	public GameObject player;
	public CharControl charControl;

	// Use this for initialization
	void Start () {
		inMag = magSize;
		reloadTimer = reloadTime;
		shotTimer = interShotDelay;
		//charControl = player.GetComponent<CharControl>() as CharControl;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown ("Fire1") && Firable ()) {
			sTimerOn = true;
			inMag -= 1;
			var pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);

			var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
			Rigidbody2D go = Instantiate(bullet, transform.position, q) as Rigidbody2D;
			go.rigidbody2D.AddForce(go.transform.up * bulletSpeed);

			//if(player.GetComponent<)
			if(charControl.isGrounded()){
				player.rigidbody2D.AddForce (go.transform.up * -kickForce);
			}
		}

		if (Input.GetButtonDown ("Reload") && inMag != magSize) {
			//play reload anim
			rTimerOn = true;
		}

		if (rTimerOn) {
			reloadTimer -= Time.deltaTime;
			if(reloadTimer <= 0) {
				rTimerOn = false;
				inMag = magSize;
				reloadTimer = reloadTime;
			}
		}
		if (sTimerOn) {
			shotTimer -= Time.deltaTime;
			if(shotTimer <= 0) {
				sTimerOn = false;
				shotTimer = interShotDelay;
			}
		}

	}

	bool Firable() {
		return (inMag != 0 && !rTimerOn && !sTimerOn);
	}
}
