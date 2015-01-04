using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class trainSpawner : MonoBehaviour {
	
	//variables
	private int carsCompleted;
	public GameObject[] possTrains;
	public string[] exemptCars; //Array  of car names with (Clone) appended, where traintop enemies shouldn't spawn
	public Queue<GameObject> trains;
	private GameObject tempTrain;
	public GameObject player;
	private GameObject cameraObj;
	private getWidthCar widthFind;
	private float theWidth;
	public float widthBetween = 2f;
	private float begTim;
	public float railHeight = 0f;
	public float despawnBuffer = 8f; //if this is increased, there could be problems with entering trains properly
	public float trainFallSpeed = .07f;
	public GameObject deadTrain;
	public float deathDelay = 8f;
	public Vector2 fallAwayPoint;
	public GameObject tutCar1;
	public GameObject tutCar2;
	public GameObject shopCar;
	public GameObject testCar;
	private bool testCarOn = false;

	public static int exPoint = 1; //Trains on the end of the list are exlcluded, when number is lowered more included
	
	void Start () {
		//begTim = Time.time;
		trains = new Queue<GameObject>();
		player = GameObject.Find ("character");
		cameraObj = GameObject.Find ("Main Camera");
		//if testCar is set, spawn only testCars
		if (testCar) {
			testCarOn = true;
			QueueAndMove(testCar);
			QueueAndMove(testCar);
		} else if (TutShopController.tutorial) { //if tutorial is on, load tutorial cars first and set camera and player to spawn correctly
			QueueAndMove(tutCar1);
			QueueAndMove(tutCar2);
			GameObject tutorialCar = (GameObject)trains.Peek();
			cameraObj.transform.position = new Vector3(tutorialCar.transform.Find ("tutorial_Spawn").transform.position.x, tutorialCar.transform.Find ("tutorial_Spawn").transform.position.y, -30);

			player.transform.position = tutorialCar.transform.Find ("tutorial_Spawn").transform.position;
		} else if (TutShopController.shop) {
			QueueAndMove(shopCar);
		TutShopController.shop = false;
				QueueAndMove();
		} else {  				 //otherwise load 2 random cars
			QueueAndMove();
			QueueAndMove();
		}
	}

	//loads a random car from a list of posible cars, spawn car right justified of this objects position, then moves transform by car width
	void QueueAndMove(){ 
		if(testCarOn){
			QueueAndMove (testCar);
		} else {
			tempTrain = (GameObject)Instantiate(possTrains[Random.Range(0, possTrains.Length - trainSpawner.exPoint)], transform.position, Quaternion.identity); //Instantiate random train at position of trainspawner
			theWidth = tempTrain.GetComponent<getWidthCar> ().carWidth (); //get car width				
			float railToCenter = railHeight - tempTrain.transform.Find ("base").transform.localPosition.y;
			tempTrain.transform.position = new Vector2(tempTrain.transform.position.x + theWidth/2, railToCenter); //right justify the car to properly space them
			trains.Enqueue(tempTrain); //put train game object into trains queue
			gameObject.transform.position = new Vector2(transform.position.x + theWidth + widthBetween, transform.position.y); //move transform width forward
		}
	}

	void QueueAndMove(GameObject traincar){ //loads specific car passed in rather than random car
		tempTrain = (GameObject)Instantiate(traincar, transform.position, Quaternion.identity); //Instantiate random train at position of trainspawner
		theWidth = tempTrain.GetComponent<getWidthCar> ().carWidth (); //get car width				
		float railToCenter = railHeight - tempTrain.transform.Find ("base").transform.localPosition.y;
		tempTrain.transform.position = new Vector2(tempTrain.transform.position.x + theWidth/2, railToCenter);
		trains.Enqueue(tempTrain); //put train game object into trains queue
		gameObject.transform.position = new Vector2(transform.position.x + theWidth + widthBetween, transform.position.y); //move transform width forward
	}
	

	public void KillTrain() { 
		//NOTE: if trains are despawning improperly, may need to make this function on its own instantiated object
		if(!playerWithinFirst ()){
			Destroy (deadTrain);
			CancelInvoke();
			deadTrain = (GameObject)trains.Dequeue();
			fallAwayPoint = new Vector2(deadTrain.transform.position.x - deadTrain.GetComponent<getWidthCar>().carWidth() * 3f,  deadTrain.transform.position.y);
			Destroy(deadTrain, deathDelay);
			//Invoke ("emptyDeadTrain", deathDelay);
			QueueAndMove();
			string c = trains.Peek ().name;
			//If upcoming car is NOT in the exemptCars array, try to spawn enemies of top of it
			if(!exemptCars.Contains (c)) {
				gameObject.GetComponent<topTrainEnemies>().spawnEnemies();
			}
		}
	}

	public void emptyDeadTrain ()
	{
		deadTrain = null;
		//fallAwayPoint = null;
	}

	public bool playerWithinFirst() //checks if player is within the left and right bounds of the first car in the queue, with a bit of a buffer to make sure cars despawn correctly
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
		if (!playerWithinFirst () && player.transform.position.x > -40f) { //if the player isn't within bounds of a train car, kill the first train of the queue.
			//bool timer = (Time.time > begTim + 2.0f);
			//if(timer)
			//{
			KillTrain ();
			//	begTim = Time.time;
			//}
		}
	}
	void FixedUpdate() {
		if(deadTrain != null) {
			deadTrain.transform.position = Vector2.Lerp (deadTrain.transform.position, fallAwayPoint, Time.deltaTime * trainFallSpeed);
		}
	}

	public Vector3 exitTele() 
	{
		GameObject trainCheck = (GameObject)trains.Peek();

		return  trainCheck.transform.Find("train_car_roof").transform.Find ("exitHatch").gameObject.transform.position;
	}

	public void appendTrain(GameObject train){
		possTrains[possTrains.Length] = train;
	}
	
} 
