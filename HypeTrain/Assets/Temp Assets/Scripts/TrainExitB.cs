using UnityEngine;
using System.Collections;

public class TrainExitB : MonoBehaviour {

	[HideInInspector] public GameObject cameraObj;
	[HideInInspector] public GameObject sidePanel;
	private GameObject trainSpawn;
	
	private bool soundPlayed;
	public AudioClip exitSound;
	
	//private GameObject Player = null;
	// Use this for initialization
	void Start () {
		//Player = GameObject.Find("Character");
		cameraObj = GameObject.Find("Main Camera");
		trainSpawn = GameObject.Find ("trainSpawner");
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
			AudioSource.PlayClipAtPoint(exitSound, transform.position);
			soundPlayed = true;
		}

		ScoreKeeper.carsCompleted += 1;

		//Make sidePanel visible again
		sidePanel = trainSpawn.GetComponent<trainSpawner> ().headPanel ();
		sidePanel.SetActive (true);
		//Unlock camera
		cameraObj.GetComponent<Camera2D> ().setLock (false);

	}
}