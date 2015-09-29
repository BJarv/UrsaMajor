using UnityEngine;
using System.Collections;

public class BountyController : MonoBehaviour {

	public GameObject[] bounties;
	public GameObject[] actives;

	public Transform pauseBounty1;
	public Transform pauseBounty2;

	public Vector3 activeBoardPos1;
	public Vector3 activeBoardPos2;

	// Use this for initialization
	void Start () {
		pauseBounty1 = GameObject.Find("pBounty1").transform;
		pauseBounty2 = GameObject.Find("pBounty2").transform;

		//Loop through all bounties on play
		for(int i = 1; i <= bounties.Length; i++) {
			Debug.Log ("Checking bounty #" + i);
			Debug.Log (PlayerPrefsBool.GetBool("bounty" + i));
			//If the bounty is an active previously, throw it back in the actives array, and set its active bool to true
			if(PlayerPrefsBool.GetBool ("bounty" + i)){
				Debug.Log ("Actually revived an active!!! $$$$$$$$$$$$$$$$$");
				addActive(bounties[i - 1]);
				bounties[i - 1].GetComponent<bounty>().choose ();
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
			Debug.Log ("already 2 active quests");
		} else {
			if(actives[0]){
				actives[1] = newActive;
			} else {
				actives[0] = newActive;
			}
			repositionActives();
		}
	}

	public void repositionActives() {
		if(actives[0] == null && actives[1] != null) {
			actives[0] = actives[1];
			actives[1] = null;
		} else if(actives[0] != null && actives[1] == null) {
			Debug.Log ("bounty1 active, but bounty2 not active");
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
			actives[0].transform.position = pauseBounty1.position;
			actives[1].transform.position = pauseBounty2.position;
		} else if(actives[0] != null && actives[1] == null){
			activeBoardPos1 = actives[0].transform.position;
			actives[0].transform.position = pauseBounty1.position;
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
