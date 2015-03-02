using UnityEngine;
using System.Collections;

public class fallThrough : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.GetAxis ("Vertical"));
		//Debug.Log (Input.GetAxis ("Horizontal"));
	}

	//Checks to see if player is pressing down while on a one way to let him drop through
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "character" && Input.GetAxis("Vertical") < 0) {
			Physics2D.IgnoreCollision (col.gameObject.collider2D, transform.parent.gameObject.collider2D, true);	
		}
	}
	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.name == "character" && Input.GetAxis("Vertical") < 0) { //Not entering here
			Physics2D.IgnoreCollision (col.gameObject.collider2D, transform.gameObject.collider2D, true);	
		}
	}
}
