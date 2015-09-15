// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class EnemyLauncher : MonoBehaviour {
	
	private bool shooting = false;
	private float timer;
	public float shootCD = 4f;

	public GameObject missile;
	public GameObject shotParticles;
	public Transform shootFrom;
	
	// Use this for initialization
	void Start () {
		timer = shootCD;
		shootFrom = GameObject.Find ("missilePoint").transform;
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
	
	/*GameObject shootBird(){ //90 for straight forward
		//Quaternion rotation = Quaternion.LookRotation(player.position);
		Vector3 playerPos = player.transform.position;
		Quaternion rotToPlayer = Quaternion.FromToRotation(Vector3.up, playerPos - transform.position);
		rotToPlayer.Set (rotToPlayer.x, rotToPlayer.y, rotToPlayer.z, rotToPlayer.w);
		GameObject bulletInstance = Instantiate(bullet, transform.position, rotToPlayer) as GameObject;
		bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletInstance.transform.up * bulletSpeed);
		
		rotToPlayer *= Quaternion.Euler (0, 0, 90);
		GameObject particles = (GameObject)Instantiate(shotParticles, transform.position, rotToPlayer); //apply particles
		particles.GetComponent<ParticleSystem>().Play ();
		Destroy (particles, particles.GetComponent<ParticleSystem>().startLifetime);
		
		return bulletInstance;
	}*/
	
	public void isShooting(bool x){
		shooting = x;
	}
}

