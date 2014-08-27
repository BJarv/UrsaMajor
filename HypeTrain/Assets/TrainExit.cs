using UnityEngine;
using System.Collections.Generic;

public class TrainExit : MonoBehaviour {
	
	private GameObject Player = null;
	private Vector2 velocity;
	public GameObject cameraObj;
	public GameObject sidePanel;
	private GameObject trainSpawn;

	//Audio variables
	private bool soundPlayed;
	public AudioClip exitSound;

	//Camera variables
	private float cameraPosition;
	private float targetCameraPosition;
	private Vector2 cameraVelocity = new Vector2 (0.5f, 0.5f);

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera");
		Player = GameObject.Find("character");
		sidePanel = GameObject.Find ("sidepanel");
		trainSpawn = GameObject.Find ("trainSpawner");
		velocity = new Vector2 (0.5f, 0.5f);

		targetCameraPosition = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("TARGET: " + targetCameraPosition);
		cameraPosition = Camera.main.orthographicSize;
		Camera.main.orthographicSize = Mathf.SmoothDamp (cameraPosition, targetCameraPosition, ref cameraVelocity.y, 1f);
	}

	void zoomIn () {
		targetCameraPosition = 12.79f;
	}

	void OnTriggerEnter2D(Collider2D hit) {
		if(Input.GetKey(KeyCode.E)){
			//Play exit sound
			if(!soundPlayed){
				AudioSource.PlayClipAtPoint(exitSound, transform.position);
				soundPlayed = true;
			}
			//Temporary Exit Solution
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			Player.rigidbody2D.AddForce(new Vector2(0, 300));
			//Make sidePanel visible again
			sidePanel = trainSpawn.GetComponent<trainSpawner>().headPanel();
			sidePanel.SetActive(true);
			//Hard zoom out, zoom in, then unlock camera
			cameraObj.GetComponent<Camera2D>().setLock(false);
			targetCameraPosition = 35;
			Invoke ("zoomIn", 1.5f);

		}
	}
	void OnTriggerStay2D(Collider2D hit) {
		if(Input.GetKey(KeyCode.E)){
			//Play exit sound
			if(!soundPlayed){
				AudioSource.PlayClipAtPoint(exitSound, transform.position);
				soundPlayed = true;
			}
			//Temporary Exit Solution
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			Player.rigidbody2D.AddForce(new Vector2(0, 300));
			//Make sidePanel visible again
			sidePanel = trainSpawn.GetComponent<trainSpawner>().headPanel();
			sidePanel.SetActive(true);
			//Hard zoom out, zoom in, then unlock camera
			cameraObj.GetComponent<Camera2D>().setLock(false);
			targetCameraPosition = 35;
			Invoke ("zoomIn", 1.5f);

		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
