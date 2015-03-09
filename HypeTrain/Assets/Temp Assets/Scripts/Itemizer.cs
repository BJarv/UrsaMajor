using UnityEngine;
using System.Collections; 

public class Itemizer : MonoBehaviour {
	
	public GameObject coin;     
	public GameObject safeKey;   
	public GameObject gold;		 
	public GameObject gem;
	GameObject player;

	GameObject nuObj;
	public float xMin = -400;
	public float xMax = 400;
	public float yMin = 1300;
	public float yMax = 1600;
	
	void Awake() {
		player = GameObject.Find("character");
	}

	public void At(Vector3 here, int amount)
	{
		int coins, golds, gems;
		gems = amount / 25;
		golds = amount % 25;
		coins = golds % 5;
		golds = golds / 5;
		StartCoroutine(makeGems(here, gems));
		StartCoroutine(makeGolds(here, golds));
		StartCoroutine(makeCoins(here, coins));
	}

	IEnumerator makeCoins(Vector3 here, int coins) {
		for(;coins > 0; coins--) {
			spawn(here, coin);
			yield return new WaitForSeconds(.2f);
		}
	}
	IEnumerator makeGolds(Vector3 here, int golds) {
		for(;golds > 0; golds--) {
			spawn(here, gold);
			yield return new WaitForSeconds(.2f);
		}
	}
	IEnumerator makeGems(Vector3 here, int gems) {
		for(;gems > 0; gems--) {
			spawn(here, gem);
			yield return new WaitForSeconds(.2f);
		}
	}

	public void keyAt(Vector3 here)
	{
		spawn (here, safeKey);
	}

	void spawn(Vector3 at, GameObject what) {
		nuObj = (GameObject)Instantiate (what, at, Quaternion.identity); //spawn item
		if(nuObj.GetComponent<magnetic>()) {
			nuObj.GetComponent<magnetic>().player = player;
		}
		nuObj.GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax)));//pop it up
	}
}
