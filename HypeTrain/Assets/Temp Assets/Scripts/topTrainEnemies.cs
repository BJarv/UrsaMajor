using UnityEngine;
using System.Collections;

public class TopTrainEnemies : MonoBehaviour {

	public GameObject[] possEnemies;

	private GameObject trainSpawner;
	private Vector2 spawn1;
	private Vector2 spawn2;
	private GameObject[] trains;
	private GameObject enemy1;
	private GameObject enemy2;

	// Use this for initialization
	void Start () {
		trainSpawner = GameObject.Find("TrainSpawner");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setSpawns() { //sets 2 spawns, each a third of the way in from the top corners of the trains
		trains = trainSpawner.GetComponent<TrainSpawner>().trains.ToArray();
		float spawnOffset = trains[0].GetComponent<GetWidthCar>().carWidth() / 3f;
		spawn1 = new Vector2(trains[0].GetComponent<GetWidthCar>().left.transform.position.x + spawnOffset, trains[1].GetComponent<GetWidthCar>().left.transform.position.y - .4f);
		spawn2 = new Vector2(spawn1.x + spawnOffset, spawn1.y);
		spawn1 = new Vector2(spawn1.x + spawnOffset/3f, spawn1.y);
	}

	//called when outside of train bounds in TrainSpawner, make enemies possibly jump onto top of train
	public void spawnEnemies() {
		setSpawns ();
		int roll = Random.Range(1, 100);
		if(ScoreKeeper.CarsCompleted < 10) {
			if(roll <= 10) spawn2enemy();
			else if(roll <= 50) spawn1enemy();
			else Debug.Log ("no enemies spawned");
		} 
		else if (ScoreKeeper.CarsCompleted >= 10 && ScoreKeeper.CarsCompleted < 20) {
			if(roll <= 25) spawn2enemy();
			else if(roll <= 60) spawn1enemy();
			else Debug.Log ("no enemies spawned");
		} else {
			if(roll <= 40) spawn2enemy();
			else if(roll <= 70) spawn1enemy();
			else Debug.Log ("no enemies spawned");
		}

	}

	private void spawn1enemy() //private because spawns are only set in the main spawnEnemies function, same for other spawn functions
	{
		if(Random.value < .5f)
		{
			spawn1left();
		}
		else
		{
			spawn1right();
		}
	}

	private void spawn2enemy() {
		spawn1left ();
		spawn1right ();
	}

	private void spawn1left() {
		enemy1 = (GameObject)Instantiate(possEnemies[Random.Range(0, possEnemies.GetLength(0))], spawn1, Quaternion.identity); //spawn enemy behind train on left spawn point
		enemy1.GetComponent<Collider2D>().enabled = false;					//make it not collide with train
		Invoke ("collisionOn", .5f);						//turn collision back on in a bit
		enemy1.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 900f)); //pop him up from behind train
	}

	private void spawn1right() {
		enemy2 = (GameObject)Instantiate(possEnemies[Random.Range(0, possEnemies.GetLength(0))], spawn2, Quaternion.identity); //spawn enemy behind train on right spawn point
		enemy2.GetComponent<Collider2D>().enabled = false;
		Invoke ("collisionOn", .5f);
		enemy2.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 900f));
	}


	void collisionOn() {
		//Physics2D.IgnoreCollision (trains[1].gameObject.collider2D, enemy1.collider2D, false);
		try {
			enemy1.GetComponent<Collider2D>().enabled = true;
		} catch {}
		try {
			enemy2.GetComponent<Collider2D>().enabled = true;
		} catch {}
	}
}
