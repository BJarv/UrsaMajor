using UnityEngine;
using System.Collections.Generic;

public class TrainEnter : MonoBehaviour {
	
	public GameObject cameraObj;
	public GameObject sidePanel;
	private GameObject trainSpawn;

	//private GameObject Player = null;
	// Use this for initialization
	void Start () {
		//Player = GameObject.Find("Character");
		cameraObj = GameObject.Find("Main Camera");
		trainSpawn = GameObject.Find ("TrainSpawner");
		sidePanel = GameObject.Find ("sidepanel");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			trainSpawn.GetComponent<trainSpawner>().KillTrain();
			cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter());
			sidePanel.SetActive(false);
			//Find center of current train before locking
			cameraObj.GetComponent<Camera2D>().setLock(true);
			//How do I change the camera height in Camera2D only when these conditions are met?
		}
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			trainSpawn.GetComponent<trainSpawner>().KillTrain();
			cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter());
			sidePanel.SetActive(false);
			//Find center of current train before locking
			cameraObj.GetComponent<Camera2D>().setLock(true);
			//How do I change the camera height in Camera2D only when these conditions are met?
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
