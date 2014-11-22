using UnityEngine;
using System.Collections;

public class topTrainEnemies : MonoBehaviour {

	public GameObject[] possEnemies;
	private GameObject trainSpawner;
	private Vector2 spawn1;
	private Vector2 spawn2;
	private GameObject[] trains;

	// Use this for initialization
	void Start () {
		trainSpawner = GameObject.Find("trainSpawner");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setSpawns() { //sets 2 spawns, each a third of the way in from the top corners of the trains
		trains = trainSpawner.GetComponent<trainSpawner>().trains.ToArray();
		float spawnOffset = trains[1].GetComponent<getWidthCar>().carWidth() / 3f;
		spawn1 = new Vector2(trains[1].GetComponent<getWidthCar>().left.transform.position.x + spawnOffset, trains[1].GetComponent<getWidthCar>().left.transform.position.y - .4f);
		spawn2 = new Vector2(spawn1.x + spawnOffset, spawn1.y);
	}

	//on exit of train, make an enemy possibly jump onto top of next train
	public void spawnEnemies() {
		setSpawns ();
		//GameObject nuEnemy = (GameObject)Instantiate(possEnemies[Random.Range(0, possEnemies.GetLength(0))], spawn1, Quaternion.identity);
		//turn off collision for enemy
		//Physics2D.IgnoreCollision (trains[1].gameObject.collider2D, transform.parent.gameObject.collider2D, true);
		//invoke it to turn back on in a bit
		//set the aggro distance to shorter if it needs it
		//give it a force upwards
		//set state to idle if need be

		//clear array here?
	}

	void collisionOn() 
	{

	}
}
