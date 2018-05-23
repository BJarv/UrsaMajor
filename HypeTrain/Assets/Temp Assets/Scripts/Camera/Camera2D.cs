// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using System.Collections;

public class Camera2D : LogController {

	//Camera Tracking Variables

	[HideInInspector] public Transform player;
	private bool lockCamera = false;

	private float centerLock;

    //Camera Control Tuning Variables
    public float smoothrate = 0.5f;
    public float zoomInSpeed = .7f;
	public float timeBeforeZoomIn = 1.25f;
	public float lockCameraSize = 14.5f;

	private Vector2 velocity;

    //Locks camera on top of cars when player's y position is between these values
    public float trainTopLower = 15f;
	public float trainTopUpper = 35f;
    
    //HAYDEN START HERE: See if you can display an int / float range in editor, and if it has any comparison functions
    //public RangeInt trainTopRange = new RangeInt(15, 35);

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
        //1 means it's a long car, so shift down the y value and keep the x scrolling
        if (centerLock == 1f) {
			newPos2D.x = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, smoothrate);
			newPos2D.y = Mathf.SmoothDamp (transform.position.y, 6.5f, ref velocity.y, smoothrate);
		}
        //Lock on second car (for now)
        else if (lockCamera) {
            //trainleft + trainright / 2
            newPos2D.x = Mathf.SmoothDamp (transform.position.x, centerLock, ref velocity.x, smoothrate);
            //default y for now
            newPos2D.y = Mathf.SmoothDamp(transform.position.y, 6.5f, ref velocity.y, smoothrate);
		}
        //Tracking at train-top level
        else {
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

	public static void SetCameraTarget(float target, float time) {
		targetCameraPosition = target;
		zoomTime = time;
	}
	//Toggles the locking/unlocking of the camera, if train's tag is bigCar, handles differently
	public void ToggleCameraLock(bool enabled) {
		lockCamera = enabled;  //Disables regular car camera lock
		if(!enabled) {  //Disables big car camera lock
			centerLock = 2f;
		}
	}
	//Determines
	public void SetCenter(float center) {
		centerLock = center;
	}

	public void ZoomIn() {
		player.GetComponent<Rigidbody2D>().gravityScale = 8f; //Revert gravity's effect
		SetCameraTarget(lockCameraSize, zoomInSpeed);
	}

	/* No longer necessary due to velocity based zoom
	public void scheduleZoomIn() {
		Invoke ("zoomIn", timeBeforeZoomIn);
	}*/
}
