﻿using UnityEngine;
using System.Collections;

public class Magnetic : LogController {

	public GameObject player;
	public bool magnetized = false;
	public bool target;
	public float speed;
	// Use this for initialization
	void Start () {
		target = player;
		speed = 20f;
        Log("WE DID IT");
	}
	
	// Update is called once per frame
	void Update () {
		if(magnetized){
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
		}

	}
}
