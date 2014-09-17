using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour {

	//Camera Tracking Variables
	public Transform player;
	public float smoothrate = 0.5f;

	public bool lockCamera = false;
	private float centerLock;
	public float zoomInSpeed = 0.4f;
	public float timeBeforeZoomIn = .5f;
	
	private Transform thisTransform;
	private Vector2 velocity;

	public float maxCamHeight = 20f;
	private float heightTest;

	//Zoom variables
	private float cameraPosition = 0;
	private static float zoomTime = 1f;
	private static float targetCameraPosition;
	private Vector2 cameraVelocity = new Vector2 (0.5f, 0.5f);

	// Use this for initialization
	void Start () {
		thisTransform = transform;
		velocity = new Vector2 (0.5f, 0.5f);
		targetCameraPosition = Camera.main.orthographicSize;
		AudioListener.volume = 0.35f;
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
		cameraPosition = Camera.main.orthographicSize;
		Camera.main.orthographicSize = Mathf.SmoothDamp (cameraPosition, targetCameraPosition, ref cameraVelocity.y, zoomTime);
	}

	public static void setCameraTarget(float target, float time) {
		targetCameraPosition = target;
		zoomTime = time;
	}
	//Toggles the locking/unlocking of the camera
	public void setLock(bool x) {
		lockCamera = x;
	}
	//Determines
	public void setCenter(float x) {
		centerLock = x;
	}

	private void zoomIn () {
		setCameraTarget(12.79f, zoomInSpeed);
	}

	public void scheduleZoomIn() {
		Invoke ("zoomIn", timeBeforeZoomIn);
	}
}
