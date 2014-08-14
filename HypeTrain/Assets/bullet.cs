﻿using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public int bulletDeath = 3;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletDeath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj) {

		if (colObj.tag == "bonus") {
			return;
		}
		if(colObj.tag == "enemy") {
			colObj.gameObject.GetComponent<Enemy>().Hurt(10f);
			Destroy (gameObject);
		}
		else if(colObj.tag != "Player") {
			Destroy (gameObject, .2f);
		}

	}


}
