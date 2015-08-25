using UnityEngine;
using System.Collections;

public class heist : MonoBehaviour {

	public GameObject valuables;
	public GameObject explosionParticles;

	// Use this for initialization
	void Start () {
		valuables = GameObject.Find ("moneyBag");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerStay2D(Collider2D col){
		GameObject player = col.gameObject;

		//If the player hits a laser, hurt them and destroy the loot
		if (player.tag == "Player") {
			//Hurt the player
			player.GetComponent<PlayerHealth>().Hurt(10);
			player.GetComponent<CharControl>().hitAnim();
			if(transform.position.x - player.transform.position.x > 0)
			{
				player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 375));
			}
			else if(transform.position.x - player.transform.position.x < 0)
			{
				player.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 375));
			}

			//Destroy the loot
			GameObject particles = (GameObject)Instantiate(explosionParticles, valuables.transform.position, valuables.transform.rotation);
			particles.GetComponent<ParticleSystem>().Play ();
			Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);
			Destroy(valuables);

			//Deactivate all lasers
			gameObject.SetActive(false);
			Debug.Log ("BUSTED");
		}
	}
}
