using UnityEngine;
using System.Collections;

public class fallThrough : MonoBehaviour {

	//Checks to see if player is pressing down while on a one way to let him drop through
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Player" && Input.GetAxis("Vertical") < 0) {
			Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D>(), transform.parent.gameObject.GetComponent<Collider2D>(), true);	
		}
	}
	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.name == "Player" && Input.GetAxis("Vertical") < 0) { //Not entering here
			Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D>(), transform.gameObject.GetComponent<Collider2D>(), true);	
		}
	}
}
