// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class Camera2D : LogController {

	//Camera Tracking Variables

	[HideInInspector] public Transform player;
	[HideInInspector] public float smoothrate = 0.5f;
	[HideInInspector] public bool lockCamera = false;

	private float centerLock;
	public float zoomInSpeed = .7f;
	public float timeBeforeZoomIn = 1.25f;
	public float lockCameraSize = 14.5f;

	private Vector2 velocity;

	public float trainTopLower = 15f; //Locks camera on top of cars when player's
	public float trainTopUpper = 35f; // y position is between these values

	//Zoom variables
	private float cameraPosition = 0;
	private static float zoomTime = 2f;
	private static float targetCameraPosition;
	private Vector2 cameraVelocity = new Vector2 (0.5f, 0.5f);

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
		velocity = new Vector2 (0.5f, 0.5f);
		targetCameraPosition = Camera.main.orthographicSize;
		AudioListener.volume = 0.35f;
	}

	// Update is called once per frame
	void Update () {
		Vector2 newPos2D = Vector2.zero;
		if (centerLock == 1f) { //1 means it's a long car, so shift down the y value and keep the x scrolling
			newPos2D.x = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, smoothrate);
			newPos2D.y = Mathf.SmoothDamp (transform.position.y, 6.5f, ref velocity.y, smoothrate);
		} else if (lockCamera) {	//Lock on second car (for now)
			newPos2D.x = Mathf.SmoothDamp (transform.position.x, centerLock, ref velocity.x, smoothrate);			  //trainleft + trainright / 2
			newPos2D.y = Mathf.SmoothDamp (transform.position.y, 6.5f, ref velocity.y, smoothrate);      			  //default y for now
		} else { 																									      //Tracking at train-top level
			newPos2D.x = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, smoothrate);
			if (trainTopUpper > player.position.y &&  player.position.y > trainTopLower){ 								  //If near the top of traincars, lock y camera
				newPos2D.y = Mathf.SmoothDamp (transform.position.y, 20f, ref velocity.y, smoothrate);
			} else if (player.position.y < 3f) {
				newPos2D.y = Mathf.SmoothDamp (transform.position.y, 3f, ref velocity.y, smoothrate);
			} else {
				newPos2D.y = Mathf.SmoothDamp (transform.position.y, player.position.y, ref velocity.y, smoothrate);  //If way higher or lower than the top, unlock y camera
			}
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
	//Toggles the locking/unlocking of the camera, if train's tag is bigCar, handles differently
	public void setLock(bool x) {
		lockCamera = x;  //Disables regular car camera lock
		if(!x){  //Disables big car camera lock
			centerLock = 2f;
		}
	}
	//Determines
	public void setCenter(float x) {
		centerLock = x;
	}

	public void zoomIn () {
		player.GetComponent<Rigidbody2D>().gravityScale = 8f; //Revert gravity's effect
		setCameraTarget(lockCameraSize, zoomInSpeed);
	}

	/* No longer necessary due to velocity based zoom
	public void scheduleZoomIn() {
		Invoke ("zoomIn", timeBeforeZoomIn);
	}*/
}
