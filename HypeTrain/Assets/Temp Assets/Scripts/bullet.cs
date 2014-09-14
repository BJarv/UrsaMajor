using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public int bulletDeath = 3;
	public int layerOfTrigs = 8; //8 is the triggers layer
	
	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletDeath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj) {

		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == layerOfTrigs) {
			return;
		}
		if(colObj.tag == "enemy") {
			colObj.gameObject.GetComponent<Enemy>().Hurt(10f);
			if(transform.position.x - colObj.transform.position.x > 0)
			{
				colObj.gameObject.rigidbody2D.AddForce(new Vector2(-200, 375));
			}
			else if(transform.position.x - colObj.transform.position.x < 0)
			{
				colObj.gameObject.rigidbody2D.AddForce(new Vector2(200, 375));
			}
			colObj.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			//Invoke("returnColor", .3f); HELP: need a way/place to return color after a short delay, even though bullet is gone. Maybe call in enemyScripts?
			Destroy (gameObject);
		}
		else if(colObj.tag != "Player") {
			Destroy (gameObject);
		}

	}


}
