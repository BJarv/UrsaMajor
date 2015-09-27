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

		for(int i = 0; i < bounties.Length; i++) {
			if(PlayerPrefs.GetInt ("activeBounty1") == (i + 1) || PlayerPrefs.GetInt ("activeBounty2") == (i + 1)){
				addActive(bounties[i]);
			} else {
				//bounties[i].SetActive (false); 
				Debug.Log ("gray out inactives here");//needs to be changed so that bounty is grayed out instead of set inactive
			}
		}
		sendActives();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void sendActives() {
		//find deathmenu/pausemenu and pass actives array to them to display bounties
		//find shopkeeper and pass bounties array to him to display all bounties(with only actives ones as active)
		Debug.Log ("not sending actives yet");
	}


	public void addActive(GameObject newActive){
		if(actives[0] && actives[1]) {
			Debug.Log ("already 2 active quests");
		} else {
			repositionActives();
			if(actives[0]){
				actives[1] = newActive;
			} else {
				actives[0] = newActive;
			}
		}
	}

	public void repositionActives() {
		if(!actives[0] && actives[1]) {
			actives[0] = actives[1];
			actives[1] = null;
		} else if(actives[0] && !actives[1]) {
			Debug.Log ("bounty1 active, but bounty2 not active");
		} else {
			Debug.LogError ("null check failed in bountycontroller");
		}
	}

	public void pauseBounties(){
		activeBoardPos1 = actives[0].transform.position;
		activeBoardPos2 = actives[1].transform.position;
		actives[0].transform.position = pauseBounty1.position;
		actives[1].transform.position = pauseBounty2.position;
	}

	public void unpauseBounties(){
		actives[0].transform.position = activeBoardPos1;
		actives[1].transform.position = activeBoardPos2;
	}


}
