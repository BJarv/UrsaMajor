using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class trainSpawner : MonoBehaviour {

	private int carsCompleted;
	public GameObject[] possTrains;
	private Queue<GameObject> trains;
	private GameObject tempTrain;
	public GameObject player;
	private getWidthCar widthFind;
	private float theWidth;
	public float widthBetween;
	private float begTim = Time.time;

	// Use this for initialization
	void Start () {
		widthBetween = 2f;
		trains = new Queue<GameObject>();
		QueueAndMove();
		QueueAndMove();
		player = GameObject.Find ("character");
	}

	void QueueAndMove(){
		tempTrain = (GameObject)Instantiate(possTrains[Random.Range(0, possTrains.GetLength(0))], transform.position, Quaternion.identity);
		trains.Enqueue(tempTrain);
		widthFind = tempTrain.GetComponent<getWidthCar>();
		theWidth = widthFind.carWidth();
		gameObject.transform.position = new Vector2(transform.position.x + theWidth + widthBetween, transform.position.y);
	}

	//queue train
	//move spawner to end of train
	//queue train
	//move spawner
	//wait for player to move to 2nd train
	//queue train
	//move spawner
	//wait for player to move to 3rd train
	//queue train, move spawner, and delete first
	//wait
	//queue and delete
	//repeat last 2


	//only kill train if !(train.left.x < player.x && player.x < train.right.x)
	public void KillTrain() {
		if(!playerWithinFirst ()){
			Destroy((GameObject)trains.Dequeue());
			QueueAndMove();
		}
	}

	public bool playerWithinFirst()
	{
		GameObject trainCheck = (GameObject)trains.Peek();
		float leftPos = trainCheck.transform.Find ("left").transform.position.x;
		float rightPos = trainCheck.transform.Find ("right").transform.position.x;
		return (leftPos < player.transform.position.x && player.transform.position.x < rightPos);
	}

	public float headCenter() {
		GameObject trainCheck = (GameObject)trains.Peek ();
		return trainCheck.transform.Find ("center").transform.position.x;
	}

	public GameObject headPanel(){
		GameObject trainCheck = (GameObject)trains.Peek ();
		return trainCheck.transform.FindChild ("sidepanel").gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (!playerWithinFirst ()) {
			bool timer = (Time.time > begTim + 2.0f);
			if(timer){
				Destroy((GameObject)trains.Dequeue());
				QueueAndMove();
				begTim = Time.time;
			}
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("MainMenu");
		}
	}
} 
