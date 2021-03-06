﻿using UnityEngine;
using System.Collections;

public class EnemyLobbedProjectile : LogController {

	public float lifetime = 3f;
	public float spinningSpeed = 5f;

	[HideInInspector] public int layerOfTrigs = 8; //8 is the triggers layer
	[HideInInspector] public int layerOfLoot = 14; //14 is the Loot layer
	[HideInInspector] public int layerOfProj = 13; //13 is the Projectiles layer

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,0,spinningSpeed);
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		//Ignore certain objects
		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.tag == "path node" || colObj.gameObject.layer == layerOfTrigs || colObj.gameObject.layer == layerOfLoot || colObj.gameObject.layer == layerOfProj) {
			return;
		}
		//If projectile hits player, hurt them
		if(colObj.tag == "Player") {
            colObj.gameObject.GetComponent<PlayerCharacter>().Hurt(10, gameObject);
            Destroy (gameObject);
		}
		//If it hits a breakable object, damage it
		if (colObj.GetComponent<Collider2D>().tag == "breakable") {
			colObj.gameObject.GetComponent<Breakable>().Damage(gameObject);
			Destroy (gameObject);
		} else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
	}
}
