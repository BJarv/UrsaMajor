using UnityEngine;
using System.Collections;

public class BountyController : MonoBehaviour {

	public GameObject[] bounties;
	public GameObject[] actives;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
