using UnityEngine;
using System.Collections;

public class NotStunTrig : LogController {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D hit) {
		if(hit.tag == "Player") {
			transform.parent.GetComponent<BigDino>().inNotStunRange = true;
		}
	}
	void OnTriggerExit2D(Collider2D hit) {
		Debug.Log (hit.tag);
		if(hit.tag == "Player") {
			transform.parent.GetComponent<BigDino>().inNotStunRange = false;
		}
	}
}