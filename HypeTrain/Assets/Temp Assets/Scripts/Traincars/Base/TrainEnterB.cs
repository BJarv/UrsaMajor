using UnityEngine;
using System.Collections.Generic;

public class TrainEnterB : LogController {
	
	[HideInInspector] public GameObject cameraObj;
	[HideInInspector] public GameObject sidePanel;
	private GameObject trainSpawn;
	
	private bool soundPlayed;
	public AudioClip enterSound;
	
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
		EnteredTrain(hit);
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		EnteredTrain (hit);
	}
	
	void EnteredTrain(Collider2D hit){
		if(!soundPlayed && enterSound != null){
			AudioSource.PlayClipAtPoint(enterSound, Camera.main.transform.position);
			soundPlayed = true;
		}
		//Pass through
		//Remove previous train
		trainSpawn.GetComponent<TrainSpawner>().KillTrain();
		//Lock camera on the current car
		cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<TrainSpawner>().headCenter());
		cameraObj.GetComponent<Camera2D>().setLock(true);
		//Remove side panel
		sidePanel = trainSpawn.GetComponent<TrainSpawner>().headPanel();
		sidePanel.SetActive(false);
	}
}
