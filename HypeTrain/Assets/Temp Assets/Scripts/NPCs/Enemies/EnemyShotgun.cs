using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShotgun : LogController {
	
	private bool shooting = false;
	public float bulletSpeed = 5000f;
	public GameObject bullet;
	Transform player;
	bool shootable = true; //currently able to shoot
	public float shootCD = 3f;
	public List<Quaternion> bulletRotsToPlayer;
	
	public float spread = 0f;
	public int bullets = 3; //must be more than 2

	public GameObject shotParticles;

	// Use this for initialization
	void Start () {

		if (bullets < 2){
			bullets = 2;
		}
		bullets += Multiplier.shotgunBulletsPlus;
		bulletRotsToPlayer = new List<Quaternion> ();
		player = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (shooting) { //if true, enemy has aggro'd and shooting at player
			if (shootable) {
				/*GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
				bulletInstance.transform.Rotate (Vector3.forward * 90);
				bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(direction*bulletSpeed, 0);
				lastShot = Time.time;
				shooting = false;*/
				shootable = false;
				Invoke ("shootOn", shootCD);
				shootBullets ();
			}
		}
	}
	
	void shootOn() 
	{
		shootable = true;
	}
	
	void shootBullets() //90 for straight forward
	{
		spread = 20f;
		//Quaternion rotation = Quaternion.LookRotation(player.position);
		Vector3 playerPos = player.transform.position;
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, playerPos - transform.position); //point to player
		Quaternion rotToPlayer = rot;

		GameObject bulletInstance;
		for(int i = 0; i < bullets; i++){
			bulletInstance = Instantiate(bullet, transform.position, rotToPlayer) as GameObject;
			bulletInstance.transform.Rotate(0f, 0f, Random.Range (-spread, spread));
			bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * bulletSpeed);
		}

		rotToPlayer *= Quaternion.Euler (0, 0, 90);
		GameObject particles = (GameObject)Instantiate(shotParticles, transform.position, rotToPlayer); //apply particles
		particles.GetComponent<ParticleSystem>().Play ();
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.startLifetime.constant);
    }

    public void isShooting(bool x){
		shooting = x;
	}
}
