using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

	public Transform player;
	public float smoothrate = 0.5f;

	public bool lockCamera = false;
	private float centerLock;

	private Transform thisTransform;
	private Vector2 velocity;

	public float maxCamHeight = 20f;
	private float heightTest;

	// Use this for initialization
	void Start () {
		thisTransform = transform;
		velocity = new Vector2 (0.5f, 0.5f);
	}

	// Update is called once per frame
	void Update () {
		Vector2 newPos2D = Vector2.zero;
			
		if (lockCamera) {	//Lock on second car (for now)
			newPos2D.x = centerLock; //trainleft + trainright / 2
			newPos2D.y = 6.5f; //default for now
		} else { 								//Left-right tracking an train-top level
			newPos2D.x = Mathf.SmoothDamp (thisTransform.position.x, player.position.x, 
			                               ref velocity.x, smoothrate);
			heightTest = Mathf.SmoothDamp (thisTransform.position.y, player.position.y, ref velocity.y, smoothrate);
			if (heightTest > maxCamHeight){
				newPos2D.y = 20f;
			} else {
				newPos2D.y = heightTest;   //default for now
			}
			//*****Replace 20 with the line below for omnidirectional tracking
			//Mathf.SmoothDamp (thisTransform.position.y, player.position.y, ref velocity.y, smoothrate);
		}
		//Update the camera
		Vector3 newPos = new Vector3 (newPos2D.x, newPos2D.y, transform.position.z);
		transform.position = Vector3.Slerp (transform.position, newPos, Time.time);
	}

	public void setLock(bool x) {
		lockCamera = x;
	}

	public void setCenter(float x) {
		centerLock = x;
	}
}
