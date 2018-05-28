using UnityEngine;
using System.Collections;

public class BountyController : LogController {

	public GameObject[] bounties;
	public GameObject[] actives;

	public GameObject pauseBounty1;
	public GameObject pauseBounty2;

	public Vector3 activeBoardPos1;
	public Vector3 activeBoardPos2;

	// Use this for initialization
	void Start () {
		//Must be dragged in from inspector?
		//pauseBounty1 = GameObject.Find ("pBounty1").GetComponent<RectTransform> ();
		//pauseBounty2 = GameObject.Find("pBounty2").GetComponent<RectTransform> ();

		//Loop through all bounties on play
		for(int i = 1; i <= bounties.Length; i++) {
			Log ("Checking bounty #" + i);
			Log (PlayerPrefsBool.GetBool("bounty" + i));

			//If the bounty is an active previously and hasn't been collected, throw it back in the actives array
			if(PlayerPrefsBool.GetBool ("bounty" + i)){
				Log ("Actually revived an active!!! $$$$$$$$$$$$$$$$$");
				addActive(bounties[i - 1]);
			}
			//Otherwise...
			else {
				//bounties[i].SetActive (false); 
				//Debug.Log ("gray out inactives here");//needs to be changed so that bounty is grayed out instead of set inactive
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Adds bounty to actives array
	public void addActive(GameObject newActive){
		if(actives[0] && actives[1]) {
			Log ("already 2 active bounties");
		} else {
			if(actives[0]){
				actives[1] = newActive;
			} else {
				actives[0] = newActive;
			}
			repositionActives();
		}
	}

	//Removes collected bounty from actives array, repositions accordingly
	public void removeActive(GameObject completedActive){
		if (actives [0] == completedActive) {
			actives [0] = actives [1];
			actives [1] = null;
		} else if (actives [1] == completedActive) {
			actives [1] = null;
		}
	}

	public void repositionActives() {
		if(actives[0] == null && actives[1] != null) {
			actives[0] = actives[1];
			actives[1] = null;
		} else if(actives[0] != null && actives[1] == null) {
			Log ("bounty1 active, but bounty2 not active");
		} else if(actives[0] != null && actives[1] != null) {
			Log("Both bounties active");
		} else {
			Debug.LogError ("null check failed in bountycontroller");
		}
	}

	//Moves active bounties on screen while paused
	public void pauseBounties(){
		//Move both bounties if both are set, otherwise one or none
		if(actives[0] != null && actives[1] != null){
			activeBoardPos1 = actives[0].transform.position;
			activeBoardPos2 = actives[1].transform.position;
			actives[0].transform.position = pauseBounty1.transform.position;
			actives[1].transform.position = pauseBounty2.transform.position;
		} else if(actives[0] != null && actives[1] == null){
			activeBoardPos1 = actives[0].transform.position;
			actives[0].transform.position = pauseBounty1.transform.position;
		}
	}

	//Moves active bounties back off screen and onto the board
	public void unpauseBounties(){
		if(actives[0] != null && actives[1] != null) {
			actives[0].transform.position = activeBoardPos1;
			actives[1].transform.position = activeBoardPos2;
		}
		else if(actives[0] != null && actives[1] == null) actives[0].transform.position = activeBoardPos1;
	}


}
