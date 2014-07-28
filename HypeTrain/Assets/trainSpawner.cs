using UnityEngine;
using System.Collections;

public class trainSpawner : MonoBehaviour {

	private int carsCompleted;
	public GameObject[] possTrains;
	private Queue trains;
	private GameObject tempTrain;
	public GameObject player;

	// Use this for initialization
	void Start () {
		trains = new Queue();
		QueueAndMove();
		QueueAndMove();
	}

	void QueueAndMove(){
		tempTrain = Instantiate(possTrains[Random.Range(0, possTrains.GetLength(0))], transform.position, Quaternion.identity);
		trains.Enqueue(tempTrain);
		transform.position.x += tempTrain.carWidth();
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
	void KillTrain(GameObject train) {
		if(!(train.left.x < player.x && player.x < train.right.x)){
			Destroy(trains.Dequeue());
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
