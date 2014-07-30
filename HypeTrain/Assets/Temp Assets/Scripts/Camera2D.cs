using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

	public Transform player;
	public float smoothrate = 0.5f;

	public bool lockCamera = false;
	public GameObject Player = null;

	private Transform thisTransform;
	private Vector2 velocity;

	// Use this for initialization
	void Start () {
		thisTransform = transform;
		velocity = new Vector2 (0.5f, 0.5f);
		Player = GameObject.Find("character");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 newPos2D = Vector2.zero;
			
		if (Player.transform.position.y < 16) {	//Lock on second car (for now)
			newPos2D.x = 0.6f;
			newPos2D.y = 4.5f;
		} else { 								//Left-right tracking an train-top level
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, player.position.x, 
			                               ref velocity.x, smoothrate);
			newPos2D.y = 18; //*****Replace 18 with the line below for omnidirectional tracking
			//Mathf.SmoothDamp (thisTransform.position.y, player.position.y, ref velocity.y, smoothrate);
		}
		//Update the camera
		Vector3 newPos = new Vector3 (newPos2D.x, newPos2D.y, transform.position.z);
		transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
		
		     
	}
}
