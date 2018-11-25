// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class EnemyMissile : LogController {

	//Player Reference
	[HideInInspector] public Transform player;

	//Collision/lifetime variables
	float timer = 0f;
	public float homingTime = 1f;
	public float lifetime = 4f;
	public float missileSpeed = 10f;
	[HideInInspector] public int triggerLayer = 8; //8 is the triggers layer
	[HideInInspector] public int lootLayer = 14; //14 is the Loot layer
	[HideInInspector] public int projectileLayer = 13; //13 is the Projectiles layer

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
		//Flip the sprite in the y-direction if shooting to the left
		if (player.position.x < transform.position.x)
			transform.localScale = new Vector3 (1, -1, 1);
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//Home for 1 second, then continue flying in the last calculated direction
		if (timer < 1f) {
			//Determine the angle between player and missile, rotate missile to that angle
			Vector3 dir = player.position - transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			//Propel missile toward player
			transform.position = Vector3.MoveTowards (transform.position, player.position, Time.deltaTime * missileSpeed); 
		} else {
			//Propel missile in last calculated direction at a faster speed
			transform.Translate(Vector3.right * Time.deltaTime * (missileSpeed + 5));
		}
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		//If missile is hit by bullet, destroy it
		if(colObj.gameObject.layer == projectileLayer){
			Destroy(colObj.gameObject);
			Destroy(gameObject);
		}
		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.tag == "path node" || colObj.gameObject.layer == triggerLayer || colObj.gameObject.layer == lootLayer) {
			return;
		}
		//If missile hits player, hurt them
		if(colObj.tag == "Player") {
            colObj.gameObject.GetComponent<PlayerCharacter>().Hurt(10, gameObject);
            Destroy (gameObject);
		}
		//If it hits a breakable object, damage it
		if (colObj.GetComponent<Collider2D>().tag == "breakable") {
			colObj.gameObject.GetComponent<Breakable>().Damage(gameObject);
			Destroy (gameObject);
		} else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
	}
}
