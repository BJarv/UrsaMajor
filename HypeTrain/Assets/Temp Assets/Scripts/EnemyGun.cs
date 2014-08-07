using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

	private bool shooting = false;
	private int direction = 0;
	public float bulletSpeed = 500f;
	public GameObject bullet;

	float lastShot = 0.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (shooting) {
			bool timer = (Time.time > lastShot + 1.0f);
			if(timer){
				GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
				bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(direction*bulletSpeed, 0);
				lastShot = Time.time;
				shooting = false;
			}
		}
	}

	public void isShooting(bool x, int y){
		shooting = x;
		direction = y;
	}
}
