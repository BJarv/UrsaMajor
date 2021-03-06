﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : LogController {
	public float maxHealth = 30f;
	[HideInInspector] public static float playerHealth;
	public float deathDelay = 1.5f;
	public GameObject heart1;
	public GameObject heart2;
	public GameObject heart3;
	[HideInInspector] public bool invincibility; 
	public float invincCD = .5f;
	//public static bool endOfLife = false;
	[HideInInspector] public bool deathCheckCheck = false; //checks to see if you can deathcheck lol
	[HideInInspector] public GameObject camObj;
	[HideInInspector] public GameObject player;
	public static bool alreadyDying = false;
	private bool healed = false;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		camObj = GameObject.Find ("Main Camera");
		playerHealth = maxHealth;
		alreadyDying = false;
	}
	
	// Update is called once per frame
	void Update () {

		if ((playerHealth <= 0f || transform.position.y < -5f) && !alreadyDying) {
			//Causes game end in Popup script
			alreadyDying = true;
			Invoke ("safetyKill", deathDelay);
			player.GetComponent<CharControl>().StartDeath (); 
			Invoke("deathCheck", deathDelay);
		}
		//Restore health upon entering HYPEMode
		if (ScoreKeeper.HYPED && !healed) {
			playerHealth = 30f;
			heart1.SetActive (true);
			heart2.SetActive (true);
			heart3.SetActive (true);
			healed = true;
		}
		if (!ScoreKeeper.HYPED) healed = false;
	}
	public void safetyKill() {
		camObj.GetComponent<Popup> ().dead = true;
	}
	public void deathCheck() {
		Game.addLoot(ScoreKeeper.Score);
		Game.addCarsCleared (ScoreKeeper.CarsCompleted);
	}

	private void adjustCounter(float currHealth)
	{
		if (currHealth == 30f) {
			heart1.SetActive (true);
			heart2.SetActive (true);
			heart3.SetActive (true);
		}
		if (currHealth == 20f) {
			heart1.SetActive (true);
			heart2.SetActive (true);
			heart3.SetActive (false);
		}
		if (currHealth == 10f) {
			heart1.SetActive (true);
			heart2.SetActive (false);
			heart3.SetActive (false);
		}
		if (currHealth == 0f) {
			heart1.SetActive (false);
			heart2.SetActive (false);
			heart3.SetActive (false);
		}
	}

	public void Hurt(float damage)
	{
		if (!invincibility) {
			invincibility = true;
			Invoke("invincOff", invincCD);
			playerHealth -= damage;
			adjustCounter (playerHealth);
		}
	}

	//Along with applying damage to player, also plays hitanim and applies appropriate knockback
	public void HurtPlus(float damage, GameObject dmgObj){
		//If player's not invincible, apply damage
		if (!invincibility) {
			invincibility = true;
			Invoke("invincOff", invincCD);
			playerHealth -= damage;
			adjustCounter (playerHealth);
		}
		//Play hit animation
		player.GetComponent<CharControl>().hitAnim();

		//Knock back player in a direction depending on their position relative to the damaging object
		if(dmgObj.transform.position.x - player.transform.position.x > 0){
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 375));
		}
		else if(dmgObj.transform.position.x - player.transform.position.x < 0){
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 375));
		}
	}

	void invincOff(){
		invincibility = false;
	}
}
