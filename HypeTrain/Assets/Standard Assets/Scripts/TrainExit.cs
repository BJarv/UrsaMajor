using UnityEngine;
using System.Collections.Generic;

public class TrainExit : MonoBehaviour {
	
	private GameObject Player = null;
	public GameObject cameraObj;
	public GameObject sidePanel;

	private bool soundPlayed;
	public AudioClip exitSound;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera");
		Player = GameObject.Find("character");
		sidePanel = GameObject.Find ("sidepanel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			if(!soundPlayed){
				AudioSource.PlayClipAtPoint(exitSound, transform.position);
				soundPlayed = true;
			}
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			Player.rigidbody2D.AddForce(new Vector2(0, 300));
			cameraObj.GetComponent<Camera2D>().setLock(false);
			//Camera2D.lockCamera = false; //How do I reference this variable here?
			//How do I change the camera height in Camera2D only when these conditions are met?
			sidePanel.SetActive(true);
		}
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			if(!soundPlayed){
				AudioSource.PlayClipAtPoint(exitSound, transform.position);
				soundPlayed = true;
			}
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			Player.rigidbody2D.AddForce(new Vector2(0, 300));
			cameraObj.GetComponent<Camera2D>().setLock(false);
			//Camera2D.lockCamera = false; //How do I reference this variable here?
			//How do I change the camera height in Camera2D only when these conditions are met?
			sidePanel.SetActive(true);
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
