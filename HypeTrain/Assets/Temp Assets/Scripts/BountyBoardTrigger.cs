using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BountyBoardTrigger : MonoBehaviour {

	public GameObject bountyCanvas;  //ISSUE WITH PLAYER NOT MOVING ONCE IN TRIGGER RADIUS???????
	public Text pressE;


    /// 
    /// WE ARE NOT USING THIS SCRIPT ANYMORE.
	/// 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colObj) {
		if(colObj.tag == "Player") {
			pressE.enabled = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D colObj) {
		if(colObj.tag == "Player") {
			pressE.enabled = false;
		}
	}

	void OnTriggerStay2D(Collider2D colObj) {
		if(colObj.tag == "Player" && bountyCanvas.activeSelf && Input.GetKeyDown(KeyCode.E)) { //hide bounty board
			bountyCanvas.transform.position = new Vector3(-1000f, -1000f, 0f);
			Popup.paused = false;

		} else if(colObj.tag == "Player" && !bountyCanvas.activeSelf && Input.GetKeyDown(KeyCode.E)) { //show bounty board
			bountyCanvas.transform.position = Camera.main.transform.position;
			Popup.paused = true;
		}

	
	}
}
