using UnityEngine;
using System.Collections.Generic;

public class TrainEnter : MonoBehaviour {
	
	//private GameObject Player = null;
	// Use this for initialization
	void Start () {
		//Player = GameObject.Find("Character");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			//How do I change the camera height in Camera2D only when these conditions are met?
		}
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			//How do I change the camera height in Camera2D only when these conditions are met?
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
