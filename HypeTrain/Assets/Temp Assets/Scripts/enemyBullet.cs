using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {
	
	public int bulletDeath = 3;
	private GameObject player = null;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("character");
		Destroy (gameObject, bulletDeath);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D colObj) {
		
		if(colObj.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			player.rigidbody2D.AddForce(new Vector2(-200, 375)); //HELP: Need to make x value rely on where player is hit from
			Destroy (gameObject);
		}
		
		else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
		
	}
	
	
}
