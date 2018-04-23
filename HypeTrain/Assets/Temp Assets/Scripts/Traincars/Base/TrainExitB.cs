using UnityEngine;
using System.Collections;

public class TrainExitB : LogController {

	[HideInInspector] public GameObject cameraObj;
	[HideInInspector] public GameObject sidePanel;
	private GameObject trainSpawn;
	
	private bool soundPlayed;
	public AudioClip exitSound;
	
	//private GameObject Player = null;
	// Use this for initialization
	void Start () {
		//Player = GameObject.Find("Player");
		cameraObj = GameObject.Find("Main Camera");
		trainSpawn = GameObject.Find ("TrainSpawner");
		sidePanel = GameObject.Find ("sidepanel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D hit) 
	{
		ExitedTrain(hit);
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		ExitedTrain (hit);
	}
	
	void ExitedTrain(Collider2D hit){
		if(!soundPlayed && exitSound != null){
			AudioSource.PlayClipAtPoint(exitSound, Camera.main.transform.position);
			soundPlayed = true;
		}

		ScoreKeeper.CarsCompleted += 1;

		//Make sidePanel visible again
		sidePanel = trainSpawn.GetComponent<TrainSpawner> ().headPanel ();
		sidePanel.SetActive (true);
		//Unlock camera
		cameraObj.GetComponent<Camera2D> ().setLock (false);

	}
}