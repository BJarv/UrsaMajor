using UnityEngine;
using System.Collections;

public class Cannonball : LogController {
	
	public float bulletDeath = .1f;
	[HideInInspector]
	public int layerOfTrigs = 8; //8 is the triggers layer
	[HideInInspector]
	public int layerOfLoot = 14; //14 is the Loot layer
	[HideInInspector]
	public int layerOfProj = 13; //13 is the Projectiles layer

	public float explosionRadius = 3f; //explosion size

	public float damage = 20f; //damage delt to enemies

	public GameObject explosion;

	public LayerMask hittableObjs;



	
	[HideInInspector]
	public GameObject meatObj;
	
	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletDeath);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void explode() {
		Collider2D[] objs = Physics2D.OverlapCircleAll (transform.position, explosionRadius, hittableObjs); //get all objects in explosion radius

		GameObject tempExplo = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);//show explosion
		Camera.main.transform.parent.transform.GetComponent<CameraShake> ().bumpIt (); //shake camera a bit
        Destroy(tempExplo, tempExplo.GetComponent<ParticleSystem>().main.startLifetime.constant);

        foreach (Collider2D colObj in objs) { //apply bullet effects but to each object in explosion radius
			if (colObj.tag == "enemy") {
				colObj.gameObject.GetComponent<Enemy> ().Hurt (damage);
				if (transform.position.x - colObj.transform.position.x > 0) {
					colObj.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-200, 375));
				} else if (transform.position.x - colObj.transform.position.x < 0) {
					colObj.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (200, 375));
				}
				colObj.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
				//Invoke("returnColor", .3f); HELP: need a way/place to return color after a short delay, even though bullet is gone. Maybe call in enemyScripts?
				Destroy (gameObject);
			}
			if (colObj.tag == "dino") {
				colObj.gameObject.GetComponent<BigDino> ().Hurt (damage);
				Destroy (gameObject);
			}
			//If it hits a breakable object
			if (colObj.GetComponent<Collider2D> ().tag == "breakable" || colObj.GetComponent<Collider2D> ().tag == "meat") {
				colObj.gameObject.GetComponent<Breakable> ().Damage (gameObject);
				Destroy (gameObject);
			}
		
			if (colObj.GetComponent<Collider2D> ().tag == "meat") {
				meatObj = colObj.gameObject;
				Destroy (meatObj.GetComponent<HingeJoint2D> ());
				Destroy (gameObject);
			} else if (colObj.tag != "Player") {
				Destroy (gameObject);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D colObj) {
		
		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == layerOfTrigs || colObj.gameObject.layer == layerOfLoot || colObj.gameObject.layer == layerOfProj) {
			return;
		} else {
			Debug.Log (colObj.name);
			explode();
		}

		
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}