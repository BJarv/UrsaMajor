using UnityEngine;
using System.Collections;

public class buy : MonoBehaviour {

	public float price;
	public static bool purchased;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if (colObj.tag == "Player") {
			//DISPLAY PRICE AND E to buy
			if (Input.GetKey (KeyCode.E) && PlayerPrefs.GetInt ("currLoot") >= price){
				purchased = true;
			}	
		}
	}

	void OnTriggerStay2D(Collider2D colObj) {
		if (colObj.tag == "Player") {
			//DISPLAY PRICE AND E to buy
			if (Input.GetKey (KeyCode.E) && PlayerPrefs.GetInt ("currLoot") >= price){
				purchased = true;
			}	
		}
	}
}
