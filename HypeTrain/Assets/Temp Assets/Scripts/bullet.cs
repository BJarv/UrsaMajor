using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	
	public float bulletDeath = .1f;
	[HideInInspector]
	public int layerOfTrigs = 8; //8 is the triggers layer
	[HideInInspector]
	public int layerOfLoot = 14; //14 is the Loot layer
	[HideInInspector]
	public int layerOfProj = 13; //13 is the Projectiles layer

	[HideInInspector]
	public GameObject meatObj;
	
	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletDeath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj) {

		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == layerOfTrigs || colObj.gameObject.layer == layerOfLoot || colObj.gameObject.layer == layerOfProj) {
			return;
		}
		if(colObj.tag == "enemy") {
            //colObj.gameObject.GetComponent<baseCharacter>().Hurt(10, gameObject);
            colObj.gameObject.GetComponent<Enemy>().Hurt(10f);

            if (transform.position.x - colObj.transform.position.x > 0)
			{
				colObj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 375));
			}
			else if(transform.position.x - colObj.transform.position.x < 0)
			{
				colObj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 375));
			}
			colObj.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			//Invoke("returnColor", .3f); HELP: need a way/place to return color after a short delay, even though bullet is gone. Maybe call in enemyScripts?
			Destroy (gameObject);
		}
		if(colObj.tag == "dino") {
			colObj.gameObject.GetComponent<BigDino>().Hurt (10f);
			Destroy (gameObject);
		}
		//If it hits a breakable object
		if (colObj.GetComponent<Collider2D>().tag == "breakable" || colObj.GetComponent<Collider2D>().tag == "meat") {
			colObj.gameObject.GetComponent<breakable>().Damage(gameObject);
			Destroy (gameObject);
		}

		if (colObj.GetComponent<Collider2D>().tag == "meat") {
			meatObj = colObj.gameObject;
			Destroy (meatObj.GetComponent<HingeJoint2D>());
			Destroy (gameObject);
		}

		else if(colObj.tag != "Player") {
			Destroy (gameObject);
		}

	}
}
