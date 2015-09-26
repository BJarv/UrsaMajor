// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class EnemyLobber : MonoBehaviour {

	private bool shooting = false;
	private float timer;
	public float shootCD = 2f;
	
	public GameObject lobbedProjectile;
	private Vector2 lobForce = new Vector2(400f, 200f);
	public Vector2 lobLong = new Vector2(600f, 300f);
	public Vector2 lobMid = new Vector2(400f, 200f);
	public Vector2 lobShort = new Vector2(200f, 150f);

	public GameObject shotParticles;

	private Transform player;
	private Transform shootFrom;
	
	// Use this for initialization
	void Start () {
		timer = 1f; //Fires 1 second after aggro initially, the every shootCD seconds
		player = GameObject.Find ("character").transform;
		shootFrom = GetComponentInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (shooting) { //if true, enemy has aggro'd and shooting at player
			//Determine how much force to use
			Debug.Log (Mathf.Abs (transform.position.x - player.position.x));
			if(Mathf.Abs (transform.position.x - player.position.x) > 10) lobForce = lobLong;
			else if(Mathf.Abs (transform.position.x - player.position.x) > 5) lobForce = lobMid;
			else lobForce = lobShort;

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