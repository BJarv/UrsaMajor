using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float maxHealth = 30f;
	public float playerHealth;
	private float resetTimer = 0.0f;
	private bool dying = false;
	public float deathDelay = 1.0f;
	public GameObject heart1;
	public GameObject heart2;
	public GameObject heart3;

	// Use this for initialization
	void Start () {
		playerHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerHealth <= 0f || transform.position.y < -5f) {
			if(!dying){
				resetTimer = 0.0f;
				dying = true;
			} else { 
				bool timer = (Time.time > resetTimer + deathDelay);
				if(timer){
					Application.LoadLevel (Application.loadedLevelName);
				}
			}
		}
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
		playerHealth -= damage;
		adjustCounter (playerHealth);
	}
}
