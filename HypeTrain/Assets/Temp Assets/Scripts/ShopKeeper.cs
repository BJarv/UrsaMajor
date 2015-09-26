using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopKeeper : MonoBehaviour {

	public Text text;
	public GameObject bountyBoard;

	bool keyUp = false;
	bool withinTrig = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.E) && withinTrig) { //check for input to show bounty board
			keyUp = true;
		}
	}

	void OnTriggerEnter2D(Collider2D colObj){ // when player gets near shopkeeper, show text
		if(colObj.tag == "Player"){
			text.enabled = true;
			withinTrig = true;
		}
	}
	void OnTriggerExit2D(Collider2D colObj){ // turn off text when player leaves
		if(colObj.tag == "Player"){
			text.enabled = false;
			withinTrig = false;
			if(bountyBoard.activeSelf) {
				bountyBoard.SetActive (false);
			}
		}

	}
	void OnTriggerStay2D(Collider2D colObj){ //choose what to show while player stays in trigger area
		if(colObj.tag == "Player"){
			if(keyUp && !bountyBoard.activeSelf) { // show board
				keyUp = false;
				bountyBoard.SetActive (true);
			} else if(keyUp && bountyBoard.activeSelf) { // hide board
				keyUp = false;
				bountyBoard.SetActive (false);
			}
		}
	}
}
