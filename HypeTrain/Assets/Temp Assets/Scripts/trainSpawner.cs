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

	// Use this for initialization
	void Start () {
		begTim = Time.time;
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

	public float headCenter() 
	{
		GameObject trainCheck = (GameObject)trains.Peek ();
		return trainCheck.transform.Find ("center").transform.position.x;
	}

	public GameObject headPanel()
	{
		GameObject trainCheck = (GameObject)trains.Peek();
		return trainCheck.transform.FindChild ("sidepanel").gameObject;
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
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel ("MainMenu");
		}
	}

	public Vector3 exitTele() 
	{
		GameObject trainCheck = (GameObject)trains.Peek();

		return  trainCheck.transform.Find("train_car_roof").transform.Find ("exit").gameObject.transform.position;
	}


} 
