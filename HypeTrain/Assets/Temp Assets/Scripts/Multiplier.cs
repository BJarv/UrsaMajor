using UnityEngine;
using System.Collections;

public class Multiplier : MonoBehaviour {

	public static float enemyHealth = 1f;
	public static float enemySpeed = 1f;
	public static float enemyShootCD = 1f;
	public static int shotgunBullets = 0;

	public static float moneyDrop = 1f;
	public static float safeDrop = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ScoreKeeper.carsCompleted == 10){
			//DO SHIT
		} 
		else if(ScoreKeeper.carsCompleted == 20){
			//DO SHIT
		}
		else if(ScoreKeeper.carsCompleted == 30){
			//DO SHIT
		}
		else if(ScoreKeeper.carsCompleted == 40){
			//DO SHIT
		}
	}
}
