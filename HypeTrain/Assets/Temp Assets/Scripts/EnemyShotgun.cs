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
	
	// Use this for initialization
	void Start () {
		if (bullets < 2){
			bullets = 2;
		}
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
		int BR = bullets; //bullets remaining
		bulletRotsToPlayer.Clear ();
		//Quaternion rotation = Quaternion.LookRotation(player.position);
		Vector3 playerPos = player.transform.position;
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, playerPos - transform.position); //point to player
		if (bullets % 2 == 1) { // if odd number of bullets, aim 1 at player, then do the rest as if you had even number of bullets
			bulletRotsToPlayer.Add (rot);
			BR--;
		}
		rot.Set (rot.x, rot.y, rot.z, rot.w - (spread/2));
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
	}
	
	public void isShooting(bool x, int y){
		shooting = x;
		direction = y;
	}
}
