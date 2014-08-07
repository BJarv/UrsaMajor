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
		trainSpawn = GameObject.Find ("trainSpawner");
		sidePanel = GameObject.Find ("sidepanel");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			//Pass through
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			//Remove previous train
			trainSpawn.GetComponent<trainSpawner>().KillTrain();
			//Lock camera on the current car and remove side panel
			cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter());
			cameraObj.GetComponent<Camera2D>().setLock(true);
			sidePanel.SetActive(false);
		}
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			//Pass through
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			//Remove previous train
			trainSpawn.GetComponent<trainSpawner>().KillTrain();
			//Lock camera on the current car and remove side panel
			cameraObj.GetComponent<Camera2D>().setCenter(trainSpawn.GetComponent<trainSpawner>().headCenter());
			cameraObj.GetComponent<Camera2D>().setLock(true);
			sidePanel.SetActive(false);
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
