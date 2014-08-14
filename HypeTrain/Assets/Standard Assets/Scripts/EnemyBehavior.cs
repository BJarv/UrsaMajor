using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D hit){
		if(hit.gameObject.tag == "Player"){
			Debug.Log ("entered enemy detect radius");
			rigidbody2D.AddForce(new Vector2(0, 300f));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
