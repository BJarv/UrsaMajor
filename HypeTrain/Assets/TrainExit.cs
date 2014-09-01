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


	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera");
		Player = GameObject.Find("character");
		sidePanel = GameObject.Find ("sidepanel");
		trainSpawn = GameObject.Find ("trainSpawner");
		velocity = new Vector2 (0.5f, 0.5f);


	}
	
	// Update is called once per frame
	void Update () {

	}
	//Check if E is pressed in trigger zone
	void OnTriggerEnter2D(Collider2D hit) {
		if(Input.GetKey(KeyCode.E)){
			ExitedTrain(hit);
		}
	}
	void OnTriggerStay2D(Collider2D hit) {
		if (Input.GetKey(KeyCode.E)){
			ExitedTrain(hit);
		}
	}
	//What to do once trigger zone is left
	void OnTriggerExit2D(Collider2D hit) {
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}

	//What to do if E is pressed in trigger
	void ExitedTrain(Collider2D hit) {
		//Play exit 
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
		//Unlock camera, hard zoom out, slow zoom in
		cameraObj.GetComponent<Camera2D>().setLock(false);
		Camera2D.setCameraTarget(30.0f, 1f);
		cameraObj.GetComponent<Camera2D>().scheduleZoomIn();
	}

}
