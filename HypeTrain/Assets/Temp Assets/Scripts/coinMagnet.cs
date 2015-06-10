using UnityEngine;
using System.Collections;

public class coinMagnet : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log (col.gameObject.name);
		if(col.GetComponent<magnetic>()) {
			//Debug.Log (col.gameObject.name);
			col.GetComponent<Rigidbody2D>().gravityScale = 0f;
			col.GetComponent<magnetic>().magnetized = true;
			GameObject tucker = GameObject.Find ("Tucker");
			if(tucker) {
				col.GetComponent<magnetic>().target = tucker;
			}
		}
	}
}
