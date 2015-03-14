using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesertHandler : MonoBehaviour {

	//This is the first background we want to access.
	public GameObject tile;


	//public GameObject desert;
	//public GameObject forest;
	//public GameObject mountains;
	//public GameObject HYPEcity;


	//The following variable is to be set to avoid z fighting
	public float zPos = 1;

	//The following variable controls where your objects are going to spawn
	public float yPos = -2;

	private Queue<GameObject> tileQ;

	//if(ScoreKeeper.carsCompleted){}
	
	GameObject firstTile;
	GameObject secondTile;
	float tileSize;
	int flipper= -1;
	float dimension;
	public float buffer = 0;

	//We will make this into a reference to the character.
	private GameObject player = null;

	// Use this for initialization
	void Start () {

		//This is now a reference to the character.
		player = GameObject.Find("character");

		//New queue for all the tiles
		tileQ = new Queue<GameObject>();

		//firstTile spawns at the camera position
		firstTile = (GameObject)Instantiate(tile, new Vector3(player.transform.position.x + 1, yPos, zPos),Quaternion.identity);
		firstTile.GetComponent<Renderer>().sortingOrder = -1; //quick fix to make dust particles visible in front of tile
		//firstTile is added to the queue
		tileQ.Enqueue(firstTile);

		//Record x dimension of tile
		dimension = tileQ.Dequeue().GetComponent<Renderer>().bounds.size.x;

		//Second tile is made at approp position and added to tileQ
		secondTile = (GameObject)Instantiate(tile, new Vector3(player.transform.position.x + dimension, yPos, zPos),Quaternion.identity);
		secondTile.GetComponent<Renderer>().sortingOrder = -1; //quick fix to make dust particles visible in front of tile
		tileQ.Enqueue (secondTile);

		//Horizontally mirror the first tile
		//firstTile.transform.localScale = new Vector3(-dimension,dimension,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{	
		//Peek at the size of the next thing in the queue

		tileSize = tileQ.Peek ().GetComponent<Renderer>().bounds.size.x;

		//If the last thing in the queue is approaching the player, make a new thing adjacent to it.
		if (tileQ.Peek ().transform.position.x < player.transform.position.x + tileSize) {
			makeNewTile (tileSize, tile, tileQ);
				}
		}

	public void changeTile(GameObject bg){
		tile = bg;
	}

	//Instantiates a game object that you choose and places it in the given game Object queue.
	//Assumes that the queue already has something in it.
	void makeNewTile(float tileSize, GameObject tile, Queue<GameObject> tileQ) {
			tileQ.Enqueue ((GameObject)Instantiate (tile, new Vector3(tileQ.Peek ().transform.position.x + tileSize - buffer,yPos,tileQ.Dequeue().transform.position.z), Quaternion.identity));
			if (flipper == -1) {
				tileQ.Peek().transform.localScale = Vector3.Scale (tileQ.Peek ().transform.localScale , new Vector3(-1,1,1));
			}
			flipper = flipper * -1;
		}
}
