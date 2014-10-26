using UnityEngine;
using System.Collections;

public class DesertHandler : MonoBehaviour {

	public GameObject desert;

	public float speed = .05f;
	public bool true4Left = true;

	//We will make this into a reference to the character.
	private GameObject player = null;

	// Use this for initialization
	void Start () {

		//This is now a reference to the character.
		player = GameObject.Find("character");


		float playerPosition;

		if (transform.position.x + 100 > player.transform.position.x)
		Instantiate(desert, new Vector3(transform.position.x + (transform.localScale.x), transform.position.y, 1),Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{	
		//Move the object in the specified direction
		if (true4Left == true)
			transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
		else 
			transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);

		//If the object goes too far away, destroy it
		if (transform.position.x + 100 < player.transform.position.x)
			Destroy (gameObject);
	}
}
