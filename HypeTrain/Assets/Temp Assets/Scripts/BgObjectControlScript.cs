using UnityEngine;
using System.Collections;

public class BgObjectControlScript : MonoBehaviour {

	public float interval = 0f;
	public GameObject cloud;
	public int inverseFrequency = 5;
	public int yMin = 0;
	public int yMax = 0;

	//We will make this into a reference to the character.
	private GameObject player = null;

	// Use this for initialization
	void Start () {
		//This is now a reference to the character.
		player = GameObject.Find("character");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// FixedUpdate is called at some constant interval idk
	void FixedUpdate() {
		interval += Random.value;

		int willStuffAppear = Random.Range (1, 100);

		if (willStuffAppear % inverseFrequency == 0)

			Instantiate (cloud, new Vector3( player.transform.position.x + 50 , Random.Range (yMin,yMax) , 1 ),Quaternion.identity);

	}
}
