using UnityEngine;
using System.Collections;

public class HypeRed : Hype {

	public AudioClip gunShot;
	public Rigidbody2D bullet; // SET IN INSPECTOR
	public float bulletSpeed = 500f;
	public GameObject shotParticles;

	// Use this for initialization
	void Start () {
		base.Start ();
		color = Color.red;
		intershotDelay = .1f;
		kickForce = 35f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override float Shoot(GameObject shootFrom) {
		AudioSource.PlayClipAtPoint(gunShot, Camera.main.transform.position);

		var pos = retical.recPos;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint(pos);

		var q = Quaternion.FromToRotation(Vector3.up, pos - shootFrom.transform.position);

		Rigidbody2D toShoot = bullet;

		Rigidbody2D go = Instantiate(toShoot, shootFrom.transform.position, q) as Rigidbody2D;
		go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * bulletSpeed);

		GameObject particles = (GameObject)Instantiate(shotParticles, shootFrom.transform.position, shootFrom.transform.rotation);
		particles.GetComponent<ParticleSystem>().Play ();
		Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);

		//	Would like this to return intershotDelay
		return intershotDelay;
	}
}
