using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float playerHealth = 100f;
	private float resetTimer = 0.0f;
	private bool dying = false;
	public float deathDelay = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth <= 0f) {
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

	public void Hurt(float damage){
		playerHealth -= damage;
	}
}
