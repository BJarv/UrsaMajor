using UnityEngine;
using System.Collections;

public class AirBlast : MonoBehaviour {

	public Vector2 direction;

	private float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//If this airblast has existed for 1/10 s and hasn't hit anything, kill itself.
		timer += Time.deltaTime;
		if (timer >= .1f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		airBlastForce (other);
	}

	void OnTriggerStay2D(Collider2D other) {
		airBlastForce (other);
	}

	void airBlastForce(Collider2D enemy) {
		enemy.gameObject.GetComponent<Enemy>().blastedByAir();
		enemy.gameObject.GetComponent<Rigidbody2D> ().AddForce (direction * 300);
		Destroy (gameObject);
	}
}
