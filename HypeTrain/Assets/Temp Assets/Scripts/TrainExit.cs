using UnityEngine;
using System.Collections.Generic;

public class TrainExit : MonoBehaviour {
	
	private GameObject Player = null;
	public GameObject cameraObj;
	public GameObject sidePanel;
	private GameObject trainSpawn;
	private Vector2 exitPos;
	private Collider2D playerColl;
	public float vertForce = 7500f;
	public float zoomOutSpeed = .5f;
	public bool check;
	
	//Audio variables
	private bool soundPlayed;
	public AudioClip exitSound;


	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera");
		Player = GameObject.Find("character");
		sidePanel = GameObject.Find ("sidepanel");
		trainSpawn = GameObject.Find ("trainSpawner");
	}
	
	// Update is called once per frame
	void Update () {

	}
	//Check if E is pressed in trigger zone
	void OnTriggerEnter2D(Collider2D hit) {
		playerColl = hit;
		if (Input.GetKey (KeyCode.E) && hit.tag == "Player")
		{
			ExitTrain (hit);
		}
	}
	void OnTriggerStay2D(Collider2D hit) {
		if (Input.GetKey (KeyCode.E) && hit.tag == "Player")
		{
			ExitTrain (hit);
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		check = false;
		Invoke ("ignoreExitCollide", .5f);
	}

	void ignoreExitCollide()
	{
		Physics2D.IgnoreCollision (playerColl, transform.parent.gameObject.collider2D, false);
	}

	void soundPlayedOff()
	{
		soundPlayed = false;
	}

	void ExitTrain(Collider2D hit)
	{
		//Play exit sound
		if (!soundPlayed) {
			soundPlayed = true;
			Invoke ("soundPlayedOff", 3f);
			AudioSource.PlayClipAtPoint (exitSound, transform.position);

			ScoreKeeper.carsCompleted += 1;

			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);

			Player.rigidbody2D.velocity = Vector3.zero;
			exitPos = trainSpawn.GetComponent<trainSpawner> ().exitTele ();
			exitPos.y -= .4f;
			Player.rigidbody2D.position = exitPos;
			
			Player.rigidbody2D.AddForce (new Vector2 (0, 2500f));
			//Make sidePanel visible again
			sidePanel = trainSpawn.GetComponent<trainSpawner> ().headPanel ();
			sidePanel.SetActive (true);
			//Unlock camera, hard zoom out, slow zoom in
			cameraObj.GetComponent<Camera2D> ().setLock (false);
			Camera2D.setCameraTarget (25.0f, zoomOutSpeed);
			cameraObj.GetComponent<Camera2D> ().scheduleZoomIn ();

		}
	}
}
