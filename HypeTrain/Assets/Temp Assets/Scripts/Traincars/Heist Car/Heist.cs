using UnityEngine;
using System.Collections;

public class Heist : LogController {

	public GameObject valuables;
	public GameObject explosionParticles;

	// Use this for initialization
	void Start () {
		valuables = GameObject.Find ("moneyBag");
	}
	
	// Update is called once per frame
	void Update () {
		if(valuables == null){
			SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
			BoxCollider2D[] colls = gameObject.GetComponentsInChildren<BoxCollider2D>();
			foreach(SpriteRenderer r in renderers){
				r.color = Color.green;
			}
			foreach(BoxCollider2D bc in colls){
				bc.enabled = false;
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D col){
		GameObject player = col.gameObject;

		//If the player hits a laser, hurt them and destroy the loot
		if (player.tag == "Player") {
			//Hurt the player
			player.GetComponent<PlayerHealth>().HurtPlus(10, gameObject);

			//Destroy the loot
			GameObject particles = (GameObject)Instantiate(explosionParticles, valuables.transform.position, valuables.transform.rotation);
			particles.GetComponent<ParticleSystem>().Play ();
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.startLifetime.constant);
            Destroy(valuables);

			//Deactivate all lasers
			gameObject.SetActive(false);
			//Debug.Log ("BUSTED");
		}
	}
}
