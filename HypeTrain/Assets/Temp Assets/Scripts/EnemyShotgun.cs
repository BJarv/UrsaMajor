using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShotgun : MonoBehaviour {
	
	private bool shooting = false;
	private int direction = 0;
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
		player = GameObject.Find ("character").transform;
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
		spread = 15f;//temp fix, take out to set spreads in inspector
		int BR = bullets; //bullets remaining
		if (BR < 3) {
			Debug.LogError("Shotguns must shoot atleast 3 bullets");
		}
		bulletRotsToPlayer.Clear ();
		//Quaternion rotation = Quaternion.LookRotation(player.position);
		Vector3 playerPos = player.transform.position;
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, playerPos - transform.position); //point to player
		Quaternion rotToPlayer = rot;
		if (bullets % 2 == 1) { // if odd number of bullets, aim 1 at player, then do the rest as if you had even number of bullets
			bulletRotsToPlayer.Add (rotToPlayer);
			BR--;
		}
		rot.Set (rot.x, rot.y, rot.z, rot.w - (spread/2));
		rot = Quaternion.AngleAxis(-(spread/2), transform.TransformDirection(Vector3.forward));
		bulletRotsToPlayer.Add (rot);
		float adjust = spread / BR; // used move cursor (spread/BR) degrees
		BR--;
		while (BR > 0) {
			rot.Set (rot.x, rot.y, rot.z, rot.w + adjust);
			bulletRotsToPlayer.Add (rot);
			BR--;
		}
		/*int checkList = 0;
		while (checkList < bulletRotsToPlayer.Count) {
			Debug.Log (bulletRotsToPlayer[checkList]);
			checkList++;
		}
		*/
		int i = 0;
		GameObject bulletInstance;
		while(i < bulletRotsToPlayer.Count){
			bulletInstance = Instantiate(bullet, transform.position, bulletRotsToPlayer[i]) as GameObject;
			bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * bulletSpeed);
			i++;
		}

		rotToPlayer *= Quaternion.Euler (0, 0, 90);
		GameObject particles = (GameObject)Instantiate(shotParticles, transform.position, rotToPlayer); //apply particles
		particles.GetComponent<ParticleSystem>().Play ();
		Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);
	}
	
	public void isShooting(bool x, int y){
		shooting = x;
		direction = y;
	}
}
