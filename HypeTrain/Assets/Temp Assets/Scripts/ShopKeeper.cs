using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopKeeper : MonoBehaviour {

	public Text text;
	[HideInInspector] public GameObject bountyBoard;

	bool keyUp = false;
	bool withinTrig = false;
	bool isOnScreen = false;
	// Use this for initialization
	void Start () {
		bountyBoard = GameObject.Find ("BountyCanvas");
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
			if(isOnScreen) {
				isOnScreen = false;
				bountyBoard.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
				bountyBoard.transform.position = new Vector3(-1000f, -1000f, 0);
			}
		}

	}
	void OnTriggerStay2D(Collider2D colObj){ //choose what to show while player stays in trigger area
		if(colObj.tag == "Player"){
			if(keyUp && !isOnScreen) { // show board
				keyUp = false;
				isOnScreen = true;
				bountyBoard.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
			} else if(keyUp && isOnScreen) { // hide board
				keyUp = false;
				isOnScreen = false;
				bountyBoard.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
				bountyBoard.transform.position = new Vector3(-1000f, -1000f, 0);
			}
		}
	}
}
