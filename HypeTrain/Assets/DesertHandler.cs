using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DesertHandler : MonoBehaviour {

	public GameObject desert;
	
	private Queue<GameObject> deserts;

	public float speed = .05f;
	public bool true4Left = true;
	public GameObject firstDesert;
	public GameObject secondDesert;
	Vector3 oldDesertPos;
	float desertSize;
	int flipper= -1;
	float dimension;

	//We will make this into a reference to the character.
	private GameObject player = null;

	// Use this for initialization
	void Start () {

		//This is now a reference to the character.
		player = GameObject.Find("character");

		//New queue for all the deserts
		deserts = new Queue<GameObject>();

		//firstDesert spawns at the camera position
		firstDesert = (GameObject)Instantiate(desert, new Vector3(player.transform.position.x + 1, -2, 1),Quaternion.identity);

		//firstDesert is added to the queue
		deserts.Enqueue(firstDesert);

		//Record x dimension of desert
		dimension = deserts.Dequeue().renderer.bounds.size.x;

		//
		secondDesert = (GameObject)Instantiate(desert, new Vector3(player.transform.position.x + dimension, -2, 1),Quaternion.identity);

		deserts.Enqueue (secondDesert);

		//Horizontally mirror the first desert.
		//firstDesert.transform.localScale = new Vector3(-dimension,dimension,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{	
		//Peek at the size of the next thing in the queue
		desertSize = deserts.Peek ().renderer.bounds.size.x;

		//
		if (deserts.Peek ().transform.position.x < player.transform.position.x + desertSize) {
			oldDesertPos = deserts.Dequeue().transform.position;
			deserts.Enqueue ((GameObject)Instantiate (desert, oldDesertPos + new Vector3(desertSize,0,0), Quaternion.identity));
			if (flipper == -1) {
				deserts.Peek().transform.localScale = Vector3.Scale (deserts.Peek ().transform.localScale , new Vector3(-1,1,1));
			}
			flipper = flipper * -1;
			Debug.Log (flipper);
				}
	}
}
