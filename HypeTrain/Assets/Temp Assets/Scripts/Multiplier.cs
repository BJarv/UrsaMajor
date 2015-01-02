using UnityEngine;
using System.Collections;

public class Multiplier : MonoBehaviour {

	public static float enemyHealth = 1f;
	public static float enemySpeed = 1f;
	public static float enemyShootCD = 1f;
	public static int shotgunBulletsPlus = 0; 

	public static float moneyDrop = 1f;
	public static float safeDrop = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(ScoreKeeper.carsCompleted == 1){
			enemyHealth = 1.1f;
			enemySpeed = 1.1f;
			enemyShootCD = 0.9f;
			
			moneyDrop = 1.1f;
			safeDrop = 1.1f;

			trainSpawner.exPoint = 0; //Includes DinoCar in PossTrains
		} 
		else if(ScoreKeeper.carsCompleted == 20){
			enemyHealth = 1.25f;
			enemySpeed = 1.25f;
			enemyShootCD = 0.9f;
			shotgunBulletsPlus = 1;
			
			moneyDrop = 1.25f;
			safeDrop = 1.25f;
		}
		else if(ScoreKeeper.carsCompleted == 30){
			enemyHealth = 1.4f;
			enemySpeed = 1.4f;
			enemyShootCD = 0.9f;
			
			moneyDrop = 1.4f;
			safeDrop = 1.4f;
		}
		else if(ScoreKeeper.carsCompleted == 40){
			enemyHealth = 1.6f;
			enemySpeed = 1.6f;
			enemyShootCD = 0.9f;
			shotgunBulletsPlus = 2;
			
			moneyDrop = 1.6f;
			safeDrop = 1.6f;
		}
	}
}
