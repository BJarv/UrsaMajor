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
	public float widthBetween = 2f;
	private float begTim;
	public float railHeight = 0f;
	public float despawnBuffer = 10f;

	// Use this for initialization
	void Start () {
		begTim = Time.time;
		trains = new Queue<GameObject>();
		if (MainMenu.tutorial) {
			QueueAndMove(possTrains[0]);
			QueueAndMove(possTrains[1]);		
		} else {
			QueueAndMove();
			QueueAndMove();
		}
		player = GameObject.Find ("character");
	}

	void QueueAndMove(){
		tempTrain = (GameObject)Instantiate(possTrains[Random.Range(2, possTrains.GetLength(0))], transform.position, Quaternion.identity); //Instantiate random train at position of trainspawner
		theWidth = tempTrain.GetComponent<getWidthCar> ().carWidth (); //get car width				
		float railToCenter = railHeight - tempTrain.transform.Find ("base").transform.localPosition.y;
		tempTrain.transform.position = new Vector2(tempTrain.transform.position.x + theWidth/2, railToCenter);
		trains.Enqueue(tempTrain); //put train game object into trains queue
		gameObject.transform.position = new Vector2(transform.position.x + theWidth + widthBetween, transform.position.y); //move transform width forward
	}

	void QueueAndMove(GameObject traincar){
		tempTrain = (GameObject)Instantiate(traincar, transform.position, Quaternion.identity); //Instantiate random train at position of trainspawner
		theWidth = tempTrain.GetComponent<getWidthCar> ().carWidth (); //get car width				
		float railToCenter = railHeight - tempTrain.transform.Find ("base").transform.localPosition.y;
		tempTrain.transform.position = new Vector2(tempTrain.transform.position.x + theWidth/2, railToCenter);
		trains.Enqueue(tempTrain); //put train game object into trains queue
		gameObject.transform.position = new Vector2(transform.position.x + theWidth + widthBetween, transform.position.y); //move transform width forward
	}
	

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
		return (leftPos - despawnBuffer < player.transform.position.x && player.transform.position.x < rightPos + despawnBuffer);
	}

	public float headCenter() 
	{
		GameObject trainCheck = (GameObject)trains.Peek ();
		if (trainCheck.tag == "bigCar") {
			return 1f; //Camera2D knows that 1 means it's a long car
		} else {
		return trainCheck.transform.Find ("center").transform.position.x;
		}
	}

	public GameObject headPanel()
	{
		GameObject trainCheck = (GameObject)trains.Peek();
		return trainCheck.transform.FindChild ("sidepanel").gameObject;
	}

	public Vector3 headVault()
	{
		GameObject trainCheck = (GameObject)trains.Peek();
		return trainCheck.transform.FindChild ("objects").transform.FindChild ("vault").transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (!playerWithinFirst () && player.transform.position.x > -40f) {
			bool timer = (Time.time > begTim + 2.0f);
			if(timer)
			{
				Destroy((GameObject)trains.Dequeue());
				QueueAndMove();
				begTim = Time.time;
			}

		}
	}

	public Vector3 exitTele() 
	{
		GameObject trainCheck = (GameObject)trains.Peek();

		return  trainCheck.transform.Find("train_car_roof").transform.Find ("exit").gameObject.transform.position;
	}


} 
