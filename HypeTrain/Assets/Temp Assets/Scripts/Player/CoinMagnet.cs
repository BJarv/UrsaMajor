using UnityEngine;
using System.Collections;

public class CoinMagnet : LogController {
	
	void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log (col.gameObject.name);
		if(col.GetComponent<Magnetic>()) {
			//Debug.Log (col.gameObject.name);
			col.GetComponent<Rigidbody2D>().gravityScale = 0f;
			col.GetComponent<Magnetic>().magnetized = true;
			GameObject tucker = GameObject.Find ("Tucker");
			if(tucker) {
				col.GetComponent<Magnetic>().target = tucker;
			}
		}
	}
}
