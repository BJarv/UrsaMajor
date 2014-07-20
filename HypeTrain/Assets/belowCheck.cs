using UnityEngine;
using System.Collections;

public class belowCheck : MonoBehaviour {

	public bool oneway;
	private Vector2 colliderSize = Vector2.zero;
	// Use this for initialization
	void Start () {
		colliderSize = (collider as BoxCollider).size;
	}

	// Update is called once per frame
	void Update () {
		transform.parent.gameObject.collider2D.enabled = !oneway;
		//Physics.IgnoreCollision (Character.collider, parent.gameObject.collider2D) = !oneway;
	}

	void OnTriggerEnter2D(Collider2D hit) {
		if(hit.gameObject.tag == "Player"){
			oneway = true;
		}
	}
	void OnTriggerStay2D(Collider2D hit) {
		if(hit.gameObject.tag == "Player"){
			oneway = true;
		}
	}

	void OnTriggerExit2D(Collider2D hit) {
		if(hit.gameObject.tag == "Player"){
			oneway = false;
		}
	}
}
