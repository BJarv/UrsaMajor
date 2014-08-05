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

	// Use this for initialization
	void Start () {
		widthBetween = 2f;
		trains = new Queue<GameObject>();
		QueueAndMove();
		QueueAndMove();
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
		GameObject trainCheck = (GameObject)trains.Peek();
		float leftPos = trainCheck.transform.Find ("left").transform.position.x;
		float rightPos = trainCheck.transform.Find ("right").transform.position.x;
		if(!(leftPos < player.transform.position.x && player.transform.position.x < rightPos)){
			Destroy((GameObject)trains.Dequeue());
			QueueAndMove();
		}
	}

	public float headCenter() {
		GameObject trainCheck = (GameObject)trains.Peek ();
		return trainCheck.transform.Find ("center").transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		
	}
} 
