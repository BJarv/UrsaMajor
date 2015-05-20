using UnityEngine;
using System.Collections;

public class SpikeWall : MonoBehaviour {

	public float speed = 4f;
	public float delay = 1f;

	[HideInInspector] public static bool spikeTimerOn = false;
	[HideInInspector] public static bool spikeTimerEnd = false;
	[HideInInspector] public GameObject Player;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("character");
	}

	void FixedUpdate () {
		if(spikeTimerOn) Invoke ("SpikeTimer", delay);
		if(spikeTimerEnd) transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);
	}

	void OnCollisionEnter2D(Collision2D colObj){
		//Hurt the player on hit
		if (colObj.collider.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 575));
			Player.GetComponent<CharControl>().hitAnim();
		}
		if (colObj.collider.tag == "breakable" || colObj.collider.tag == "bonus") {
			Destroy(colObj.gameObject);
		}
		if (colObj.collider.tag == "wall") {
			Debug.Log ("Hit a wall!");
			spikeTimerEnd = false;
		}
	}

	void OnTriggerEnter2D(Collider2D hit){
		if (hit.tag == "bonus") {
			Destroy(hit.gameObject);
		}
	}

	void SpikeTimer () {
		spikeTimerEnd = true;
	}

}