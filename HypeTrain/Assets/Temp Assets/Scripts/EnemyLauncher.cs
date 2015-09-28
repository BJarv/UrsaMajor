// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class EnemyLauncher : MonoBehaviour {
	
	private bool shooting = false;
	private float shotTimer;
	public float shootCD = 3f;

	//Animation variables.
	private float animTimer;
	public float shootAnimCD = 2.75f;
	public bool justDidAnim = false;
	public Animator anim;

	public GameObject missile;
	public GameObject shotParticles;
	private Transform shootFrom;


	// Use this for initialization
	void Start () {
		shotTimer = 1f; //Fires 1 second after aggro initially
		shootFrom = GetComponentInChildren<Transform>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (shooting) { //if true, enemy has aggro'd and shooting at player
			//Every (shootCD) seconds, fire a birdMissile
			shotTimer -= Time.deltaTime;
			animTimer -= Time.deltaTime;

			//The time between when the animation plays and when the bullet fires is constant.
			//So, when a bullet is fired, both timers are reset.
			if(shotTimer <= 0){
				Instantiate (missile, shootFrom.position, Quaternion.identity);
				Debug.Log ("BIRD TIMER" + shotTimer);
				shotTimer = shootCD;
				animTimer = shootAnimCD;
				justDidAnim = false;
			}
			if(animTimer <= 0) {
				if (anim != null && justDidAnim == false){
					anim.SetTrigger ("throwing");
					justDidAnim = true;
				}
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

	public void setAnimator(Animator a) {
		anim = a;

	}
}

