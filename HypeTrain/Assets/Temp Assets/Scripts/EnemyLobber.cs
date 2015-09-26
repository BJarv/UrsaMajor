// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class EnemyLobber : MonoBehaviour {

	private bool shooting = false;
	private float timer;
	public float shootCD = 2f;
	
	public GameObject lobbedProjectile;
	public Vector2 lobForce = new Vector2(400f, 200f);
	public GameObject shotParticles;

	private Transform player;
	private Transform shootFrom;
	
	// Use this for initialization
	void Start () {
		timer = 1f; //Fires 1 second after aggro initially, the every shootCD seconds
		player = GameObject.Find ("character").transform;
		shootFrom = GameObject.Find ("lobPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (shooting) { //if true, enemy has aggro'd and shooting at player
			//Every (shootCD) seconds, lob a projectile
			timer -= Time.deltaTime;
			if(timer <= 0){
				GameObject lobInstance = Instantiate (lobbedProjectile, shootFrom.position, Quaternion.identity) as GameObject;
				//Lob right
				if (transform.position.x < player.position.x){
					lobInstance.GetComponent<Rigidbody2D>().AddForce(lobForce);
				}
				//Lob left
				else {
					lobInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-lobForce.x, lobForce.y));
				}
				timer = shootCD;
			}
		}
	}
	
	public void isShooting(bool x){
		shooting = x;
	}
}