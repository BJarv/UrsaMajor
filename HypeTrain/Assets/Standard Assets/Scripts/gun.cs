﻿using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {

	public float bulletSpeed = 500f;
	public float kickForce = 10000f;
	public int magSize = 3;
	public float reloadTime = 2f;
	private int inMag;
	private float reloadTimer;
	public Rigidbody2D bullet;
	private bool rTimerOn = false;
	public float interShotDelay = .5f;
	private bool sTimerOn = false;
	private float shotTimer;
	private GameObject player = null;
	public AudioClip gunshot;
	public AudioClip reload;
	//private CharControl charControl;

	// Use this for initialization
	void Start () {
		inMag = magSize;
		reloadTimer = reloadTime;
		shotTimer = interShotDelay;
		player = GameObject.Find("character");
		//charControl = player.GetComponent<CharControl>() as CharControl;
	}
	
	// Update is called once per frame
	void Update () {

		//rotation
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	
		if (Input.GetButtonDown ("Fire1") && Firable ()) {
			AudioSource.PlayClipAtPoint(gunshot, transform.position);

			sTimerOn = true;
			inMag -= 1;
			var pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);

			var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
			Rigidbody2D go = Instantiate(bullet, transform.position, q) as Rigidbody2D;
			go.rigidbody2D.AddForce(go.transform.up * bulletSpeed);


			if(inMag <= 0){
				AudioSource.PlayClipAtPoint(reload, transform.position);
				rTimerOn = true;
			}

			//if(player.GetComponent<)
			if(!player.GetComponent<CharControl>().isGrounded()){
				Debug.Log(new Vector2(go.transform.up.x * -kickForce, go.transform.up.y * -kickForce));
				player.rigidbody2D.AddForce (new Vector2(go.transform.up.x * -kickForce, go.transform.up.y * -kickForce ));
			}
		}

		if (Input.GetButtonDown ("Reload") && inMag != magSize) {
			AudioSource.PlayClipAtPoint(reload, transform.position);
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