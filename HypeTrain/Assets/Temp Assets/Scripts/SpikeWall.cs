using UnityEngine;
using System.Collections;

public class SpikeWall : MonoBehaviour {

	public float speed = 4f;
	public float delay = 1f;

	[HideInInspector] public bool spikeTimerOn = false;
	[HideInInspector] public bool spikeTimerEnd = false;
	[HideInInspector] public GameObject Player;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("character");
	}

	//Start moving the spike wall once the timer has ended
	void FixedUpdate () {
		if(spikeTimerOn) Invoke ("SpikeTimer", delay);
		if (spikeTimerEnd) {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); //Prevent the spike wall from being affected by outside forces
			transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z); //Move wall at given speed
		}
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
			Destroy(gameObject);
		}
	}

	//For destroying oneway platforms and money, which have a trigger box collider
	void OnTriggerEnter2D(Collider2D hit){
		if (hit.tag == "bonus") {
			Destroy(hit.gameObject);
		}
	}

	public void activateSpikeTimer() {
		spikeTimerOn = true;
	}

	void SpikeTimer () {
		spikeTimerEnd = true;
	}

}