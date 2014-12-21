using UnityEngine;
using System.Collections.Generic;

public class TrainEnterB : MonoBehaviour {
	
	[HideInInspector] public GameObject cameraObj;
	[HideInInspector] public GameObject sidePanel;
	private GameObject trainSpawn;
	
	private bool soundPlayed;
	public AudioClip enterSound;
	
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
		EnteredTrain(hit);
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		EnteredTrain (hit);
	}
	void OnTriggerExit2D(Collider2D hit) 
	{

	}
	
	void EnteredTrain(Collider2D hit){
		if(!soundPlayed){
			AudioSource.PlayClipAtPoint(enterSound, transform.position);
			soundPlayed = true;
		}
		//Pass through
		//Remove previous train
		trainSpawn.GetComponent<trainSpawner>().KillTrain();
		//Lock camera on the current car
		cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter());
		cameraObj.GetComponent<Camera2D>().setLock(true);
		//Remove side panel
		sidePanel = trainSpawn.GetComponent<trainSpawner>().headPanel();
		sidePanel.SetActive(false);
	}
}
