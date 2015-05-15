using UnityEngine;
using System.Collections;

public class BountyController : MonoBehaviour {

	public GameObject[] bounties;
	public GameObject[] actives;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < bounties.Length; i++) {
			if(PlayerPrefs.GetInt ("activeBounty1") == (i + 1) || PlayerPrefs.GetInt ("activeBounty2") == (i + 1)){
				addActive(bounties[i]);
			} else {
				bounties[i].SetActive (false); 
				Debug.Log ("fix this");//needs to be changed so that bounty is grayed out instead of set inactive
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


}
