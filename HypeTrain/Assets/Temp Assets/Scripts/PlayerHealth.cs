using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float maxHealth = 30f;
	[HideInInspector] public float playerHealth;
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
	[HideInInspector] public bool alreadyDying = false;
	private bool healed = false;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("character");
		camObj = GameObject.Find ("Main Camera");
		playerHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if ((playerHealth <= 0f || transform.position.y < -5f) && !alreadyDying) {
			//Causes game end in Popup script
			alreadyDying = true;
			Invoke ("safetyKill", 2*deathDelay);
			player.GetComponent<CharControl>().StartDeath (); 
			Invoke("deathCheck", deathDelay);
			/*if(!dying){
				resetTimer = 0.0f;
				dying = true;
			} else { 
				bool timer = (Time.time > resetTimer + deathDelay);
				if(timer){
					Game.addLoot(ScoreKeeper.Score);
					endOfLife = true;
				}
			}*/
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
		Game.addCarsCleared (ScoreKeeper.carsCompleted);
		//endOfLife = true;
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

	void invincOff(){
		invincibility = false;
	}
}
