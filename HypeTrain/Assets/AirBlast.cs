using UnityEngine;
using System.Collections;

public class AirBlast : MonoBehaviour {

	public Vector2 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		airBlastForce (other);
	}

	void OnTriggerStay2D(Collider2D other) {
		airBlastForce (other);
	}

	void airBlastForce(Collider2D enemy) {
		enemy.GetComponent<Collider2D>().gameObject.GetComponent<Enemy>().blastedByAir();
		enemy.gameObject.GetComponent<Rigidbody2D> ().AddForce (direction * 300);
		Destroy (gameObject);
	}
}
