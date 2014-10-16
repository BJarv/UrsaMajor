using UnityEngine;
using System.Collections;

// <summary>
// Intended to scroll background objects across the screen. The user can adjust speed, direction (true4Left), and check handleSpeed
// if they want the script to guess a speed (based on the object's scale).
// by Sam and Hayden
// </summary>

public class Scroller : MonoBehaviour 
{

	public float speed = .05f;
	public bool true4Left = true;
	public bool handleSpeed = false;
	public float e = Mathf.Exp (1);

	//We will make this into a reference to the character.
	private GameObject player = null;

	void Start() {
		//This is now a reference to the character.
		player = GameObject.Find("character");
		float dimension = Random.Range (1,3);
		gameObject.transform.localScale = new Vector3 (-dimension, dimension, 1);
		}
	
	void Update() 
	{
	}
	
	void FixedUpdate()
	{
				if (handleSpeed == true) {
						speed = (Mathf.Log (Mathf.Abs (transform.localScale.x)) + e) * .5f;
				}
				if (true4Left == true)
						transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
				else 
						transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);

		if (transform.position.x + 100 < player.transform.position.x)
			Destroy (gameObject);
		}
		}