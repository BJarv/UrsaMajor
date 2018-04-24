// AUTHOR
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class LaserFire : LogController {
		
	//What the lazer collides with
	public LayerMask lazerStoppers;
	LineRenderer lazer;
	public float lazerLength;

	//Referebces
	private GameObject revolver;
	private GameObject shootFrom;
	public GameObject laserShotParticles;
	public GameObject laserChargeParticles;

	private float lazerTimer;
	private bool lTimerOn = false;
	public float lazerTime = 0.1f;

	private float reloadTimer;
	private bool rTimerOn = false;
	public float reloadTime = 2f;

	//Particle effects
	GameObject shotParticles = null;
	GameObject rechargeParticles = null;

	//Used to play recharge particles only once per CD
	bool once = false;
		
	void Start () {
		//Grab the linerenderer from the object and default it to disabled
		lazer = gameObject.GetComponent<LineRenderer>();
		lazer.enabled = false;
		lazer.sortingLayerName = "Player";
		lazer.sortingOrder = 1;
		lazerTimer = lazerTime;
		reloadTimer = reloadTime;
		shootFrom = GameObject.Find("barrelTip");
	}

	void Update () {

		//Fire the lazer when HYPE mode is on, and cooldown is off
		if(Input.GetButtonDown("Fire1") && !lTimerOn && !rTimerOn && HYPEController.lazers){
			StopCoroutine("Firelazer");
			StartCoroutine("Firelazer");
			lTimerOn = true;

			//Access gun script for kickback
			//player.GetComponentInChildren<gun>().kickIfAirbourne(200f);

			/*Create particles for shot
			shotParticles = (GameObject)Instantiate(laserShotParticles, shootFrom.transform.position, laserShotParticles.transform.rotation * shootFrom.transform.rotation);
			shotParticles.GetComponentInChildren<ParticleSystem>().Play ();
			Destroy (shotParticles, shotParticles.GetComponentInChildren<ParticleSystem>().startLifetime);
			shotParticles = null;
			*/
		}

		//Keep the active particles lined up with the barrel tip
		if (shotParticles != null){
			shotParticles.transform.position = shootFrom.transform.position;
			shotParticles.transform.rotation = shootFrom.transform.rotation;
		}
		if (rechargeParticles != null){
			rechargeParticles.transform.position = shootFrom.transform.position;
			rechargeParticles.transform.rotation = shootFrom.transform.rotation;
		}

		//Turns off the lazer quickly after it's fired, starts reload timer
		if (lTimerOn) {
			lazerTimer -= Time.deltaTime;
			if(lazerTimer <= 0) {
				lTimerOn = false;
				lazerTimer = lazerTime;
				lazer.enabled = false;
				rTimerOn = true;
			}
		}

		//Once completed, player can fire lazer again
		if (rTimerOn) {
			reloadTimer -= Time.deltaTime;
			//If HYPE isn't over, show recharge particles
			if(reloadTimer <= .7f && HYPEController.lazers && !once){
				once = true;
				/*
				rechargeParticles = (GameObject)Instantiate(laserChargeParticles, shootFrom.transform.position, laserShotParticles.transform.rotation * shootFrom.transform.rotation);
				rechargeParticles.GetComponentInChildren<ParticleSystem>().Play ();
				Destroy (rechargeParticles, rechargeParticles.GetComponentInChildren<ParticleSystem>().startLifetime);
				rechargeParticles = null;
				*/
			}
			if(reloadTimer <= 0) {
				rTimerOn = false;
				once = false;
				reloadTimer = reloadTime;
			}
		}
	}

	//Coroutine to raycast then display lazer
	IEnumerator Firelazer(){
		lazer.enabled = true; //Enable LineRenderer
			
		while(Input.GetButton("Fire1")){
			//Raycast for determining length of lazer
			Ray2D ray = new Ray2D(transform.position, transform.right);
			lazer.SetPosition(0, ray.origin);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, lazerLength, lazerStoppers);

			//If the ray collides with something in the lazerStoppers, set the lazer's endpoint to the point of the collision
			if(hit){
				lazer.SetPosition(1, hit.point);
				//If the ray hits an enemy, deal damage
				if(hit.collider.gameObject.tag == "enemy"){
					hit.collider.gameObject.GetComponent<Enemy>().Hurt(50f);
				}
			}
			else //If no collision, set the lazer's endpoint to the lazerlength
				lazer.SetPosition(1, ray.GetPoint(lazerLength));
			yield return null;
		}
		lazer.enabled = false;
	}
}
