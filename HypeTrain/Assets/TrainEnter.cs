using UnityEngine;
using System.Collections.Generic;

public class TrainEnter : MonoBehaviour {

<<<<<<< HEAD
	public GameObject trainSpawn;
	public GameObject cameraObj;
=======
	private GameObject trainSpawn;

	public bool lockCamera;
>>>>>>> f02a42cb383dfbc6b945446b03c67a2a44c60702

	//private GameObject Player = null;
	// Use this for initialization
	void Start () {
		//Player = GameObject.Find("Character");
<<<<<<< HEAD
		cameraObj = GameObject.Find("Main Camera");
=======
		trainSpawn = GameObject.Find ("TrainSpawner");

>>>>>>> f02a42cb383dfbc6b945446b03c67a2a44c60702
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			//trainSpawn.GetComponent<trainSpawner>().KillTrain();
			cameraObj.GetComponent<Camera2D>().setLock(true);
			//How do I change the camera height in Camera2D only when these conditions are met?
			//Camera2D.lockCamera = true; //How do I reference this variable here?
		}
	}
	void OnTriggerStay2D(Collider2D hit) 
	{
		if(Input.GetKey(KeyCode.E)){
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			//trainSpawn.GetComponent<trainSpawner>().KillTrain();
			cameraObj.GetComponent<Camera2D>().setLock(true);
			//How do I change the camera height in Camera2D only when these conditions are met?
			//Camera2D.lockCamera = true; //How do I reference this variable here?
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
