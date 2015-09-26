// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class EnemyLauncher : MonoBehaviour {
	
	private bool shooting = false;
	private float timer;
	public float shootCD = 3f;

	public GameObject missile;
	public GameObject shotParticles;
	private Transform shootFrom;
	
	// Use this for initialization
	void Start () {
		timer = 1f; //Fires 1 second after aggro initially
		shootFrom = GetComponentInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (shooting) { //if true, enemy has aggro'd and shooting at player
			//Every (shootCD) seconds, fire a birdMissile
			timer -= Time.deltaTime;
			if(timer <= 0){
				Instantiate (missile, shootFrom.position, Quaternion.identity);
				timer = shootCD;
			}
		}
	}
	
	/*
		GameObject particles = (GameObject)Instantiate(shotParticles, transform.position, rotToPlayer); //apply particles
		particles.GetComponent<ParticleSystem>().Play ();
		Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);
	*/
	
	public void isShooting(bool x){
		shooting = x;
	}
}

