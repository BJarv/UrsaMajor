using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {

	public int bulletDeath = 3;
	[HideInInspector] public int layerOfTrigs = 8; //8 is the triggers layer
	[HideInInspector] public int layerOfLoot = 14; //14 is the Loot layer
	[HideInInspector] public int layerOfProj = 13; //13 is the Projectiles layer
	private GameObject player = null;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("character");
		Destroy (gameObject, bulletDeath);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void returnColor() {
		player.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	void OnTriggerEnter2D(Collider2D colObj) {

		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == layerOfTrigs || colObj.gameObject.layer == layerOfLoot || colObj.gameObject.layer == layerOfProj) {
			return;
		}
		if(colObj.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().HurtPlus(10, gameObject);
			Destroy (gameObject);
		}
		//If it hits a breakable object
		if (colObj.GetComponent<Collider2D>().tag == "breakable") {
			colObj.gameObject.GetComponent<breakable>().Damage();
			Destroy (gameObject);
		}
		
		else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
		
	}
	
	
}
