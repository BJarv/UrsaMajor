﻿using UnityEngine;
using System.Collections;

public class AirBlast : LogController {

	public Vector2 direction;

	public GameObject player;

	//private float timer = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		//If this airblast has existed for 1/10 s and hasn't hit anything, kill itself.
		Destroy (gameObject, .1f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.name != "coinMagnet") airBlastForce (other);
	}

	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.name != "coinMagnet") airBlastForce (other);
	}

	void airBlastForce(Collider2D enemy) {
		enemy.gameObject.GetComponent<Enemy>().blastedByAir();
		enemy.gameObject.GetComponent<Rigidbody2D> ().AddForce (direction * 300);

		Destroy (gameObject);
	}
}
