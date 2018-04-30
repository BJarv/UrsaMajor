using UnityEngine;
using System.Collections;

public class HypeBlue : Hype {

	public GameObject airBlast;
	public GameObject airShotParticles;

	// Use this for initialization
	protected override void Start () {
		color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override float Shoot(GameObject shootFrom) {
		//Get reticle position
		var pos = Camera.main.ScreenToWorldPoint(Reticle.recPos);
		//Get direction between the gun and the reticle
		Vector2 direction = (pos - shootFrom.transform.position);
		//Appropriate rotation for trigger
		var q = Quaternion.FromToRotation(Vector3.up, direction);

		GameObject toShoot = airBlast;
		//Airblast is shot, direction is passed
		GameObject go = Instantiate(toShoot, shootFrom.transform.position, q) as GameObject;
		go.GetComponent<AirBlast>().direction = direction;

		//Create particles for shot
		GameObject particles = (GameObject)Instantiate(airShotParticles, shootFrom.transform.position, shootFrom.transform.rotation);
		particles.GetComponent<ParticleSystem>().Play ();
		Destroy (particles, particles.GetComponent<ParticleSystem>().main.startLifetime.constant);

		//	Would like this to return intershotDelay
		return intershotDelay;

		// Kick if airborne 100f
	}
}
