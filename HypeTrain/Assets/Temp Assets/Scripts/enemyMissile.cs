// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class enemyMissile : MonoBehaviour {

	//Player Reference
	[HideInInspector] public Transform player;

	//Collision/lifetime variables
	public float lifetime = 3f;
	public float missileSpeed = 10f;
	[HideInInspector] public int triggerLayer = 8; //8 is the triggers layer
	[HideInInspector] public int lootLayer = 14; //14 is the Loot layer
	[HideInInspector] public int projectileLayer = 13; //13 is the Projectiles layer

	// Use this for initialization
	void Start () {
		player = GameObject.Find("character").transform;
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		//Determine the angle between player and missile, rotate missile to that angle
		Vector3 dir = player.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		//Propel missile forward
		transform.position = Vector3.MoveTowards (transform.position, player.position, Time.deltaTime * missileSpeed); 
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		//If missile is hit by bullet, destroy it
		if(colObj.gameObject.layer == projectileLayer){
			Destroy(colObj.gameObject);
			Destroy(gameObject);
		}
		if (colObj.tag == "bonus" || colObj.tag == "UI" || colObj.gameObject.layer == triggerLayer || colObj.gameObject.layer == lootLayer) {
			return;
		}
		//If missile hits player, hurt them
		if(colObj.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().HurtPlus(10, gameObject);
			Destroy (gameObject);
		}
		//If missile hits a breakable object
		if (colObj.GetComponent<Collider2D>().tag == "breakable") {
			colObj.gameObject.GetComponent<breakable>().Damage(gameObject);
			Destroy (gameObject);
		} else if(colObj.tag != "enemy") {
			Destroy (gameObject);
		}
	}
}
