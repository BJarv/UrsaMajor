using UnityEngine;
using System.Collections;

public class coinMagnet : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log (col.gameObject.name);
		if(col.GetComponent<magnetic>()) {
			//Debug.Log (col.gameObject.name);
			col.rigidbody2D.gravityScale = 0f;
			col.GetComponent<magnetic>().magnetized = true;
		}
	}
}
