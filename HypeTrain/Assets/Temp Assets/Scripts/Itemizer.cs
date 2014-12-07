using UnityEngine;
using System.Collections;

public class Itemizer : MonoBehaviour {
	
	public GameObject coin;     
	public GameObject safeKey;   
	public GameObject gold;		 
	public GameObject gem;

	GameObject nuObj;
	public float xMin = -400;
	public float xMax = 400;
	public float yMin = 1300;
	public float yMax = 1600;
	
	public void At(Vector3 here, int amount)
	{
		int coins, golds, gems;
		gems = amount / 25;
		golds = amount % 25;
		coins = golds % 5;
		golds = golds / 5;
		while(gems > 0) {
			spawn(here, gem);
			gems--;
		}
		while(golds > 0) {
			spawn(here, gold);
			golds--;
		}
		while(coins > 0) {
			spawn(here, coin);
			coins--;
		}
			
	}

	public void keyAt(Vector3 here)
	{
		spawn (here, safeKey);
	}

	void spawn(Vector3 at, GameObject what) {
		nuObj = (GameObject)Instantiate (what, at, Quaternion.identity); //spawn item
		nuObj.rigidbody2D.AddForce (new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax)));//pop it up
	}
}
