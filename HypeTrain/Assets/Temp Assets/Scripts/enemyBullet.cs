using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {

	private Animator animator; //LOOK HERE HAYDEN Store a ref to the animator so we can use it later
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

	void returnColor() {
		player.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		
		if(colObj.tag == "Player") {
			animator.SetBool ("Hit",true); //LOOK HERE HAYDEN Switch character to hit animation
			colObj.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			if(transform.position.x - colObj.transform.position.x > 0)
			{
				player.rigidbody2D.AddForce(new Vector2(-200, 375));
			}
			else if(transform.position.x - colObj.transform.position.x < 0)
			{
				player.rigidbody2D.AddForce(new Vector2(200, 375));
			}
			colObj.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			//Invoke("returnColor", .3f); HELP: need a way/place to return color after a short delay, even though bullet is gone. Maybe call in CharControl?
			Destroy (gameObject);
		}
		
		else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
		
	}
	
	
}
