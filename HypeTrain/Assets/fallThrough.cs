using UnityEngine;
using System.Collections;

public class fallThrough : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Checks to see if player is pressing down while on a one way to let him drop through
	void OnCollisionEnter(Collision col) {
		Debug.Log ("Hello");
		if (col.gameObject.name == "character" && Input.GetKey(KeyCode.S)) { //Not entering here
			Debug.Log ("FALL THROUGH");
			//Physics2D.IgnoreCollision (col.gameObject.collider2D, transform.parent.gameObject.collider2D, true);	
		}
	}
	void OnCollisionStay(Collision col) {
		Debug.Log ("Hello");
		if (col.gameObject.name == "character" && Input.GetKey(KeyCode.S)) { //Not entering here
			Debug.Log ("FALL THROUGH");
			//Physics2D.IgnoreCollision (col.gameObject.collider2D, transform.parent.gameObject.collider2D, true);	
		}
	}
}
