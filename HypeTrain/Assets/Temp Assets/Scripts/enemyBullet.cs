using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {

	public int bulletDeath = 3;
	public int layerOfTrigs = 8; //8 is the triggers layer
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

		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == layerOfTrigs) {
			return;
		}
		if(colObj.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			if(transform.position.x - colObj.transform.position.x > 0)
			{
				player.rigidbody2D.AddForce(new Vector2(-200, 375));
			}
			else if(transform.position.x - colObj.transform.position.x < 0)
			{
				player.rigidbody2D.AddForce(new Vector2(200, 375));
			}
			player.GetComponent<CharControl>().hitAnim();
			Destroy (gameObject);
		}
		//If it hits a breakable object
		if (colObj.collider2D.tag == "breakable") {
			colObj.gameObject.GetComponent<breakable>().Damage();
			Destroy (gameObject);
		}
		
		else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
		
	}
	
	
}
