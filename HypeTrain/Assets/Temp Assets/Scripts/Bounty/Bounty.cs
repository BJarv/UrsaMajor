using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bounty : MonoBehaviour {

	public int bountyNumber = -1; //constructs key for playerprefs from this number
	public string bountyName = "";
	public string description = "";
	public int completeAmount = 0;
	public bool cumulative = false;

	/// <summary>
	/// LINE 64
	/// </summary>

	public Text title;
	public Text info;
	public Text counter;

	//private Image picture;
	private Text mouseOverText;
	public BountyController bountyConch;

	public bool completed = false;
	public bool collected = false;
	

	// Use this for initialization
	void Start () {
		//Set inspector defined text
		title.text = bountyName;
		info.text = description;
		counter.text = 0 + "/" + (completeAmount);

		//Bounty controller reference
		bountyConch = transform.parent.transform.parent.GetComponent<BountyController> ();

		//Ensure bounty is properly initialized in inspector, all these fields must have values
		if(bountyName == "" || completeAmount == 0 || bountyNumber == -1 || description == "") {
			Debug.LogError(gameObject.name + " has missing values."); 
		}
	}

	//Called on click
	public void choose() { 
		//If -1, the bounty has been completed and collected so don't click, otherwise...
		if(PlayerPrefs.GetInt ("bounty" + bountyNumber) != -1){
			//This check is used to activate bounties in-game for the first time
			if (!PlayerPrefsBool.GetBool ("bounty" + bountyNumber) && (!bountyConch.actives[1] || !bountyConch.actives[0])){
				PlayerPrefsBool.SetBool ("bounty" + bountyNumber, true);
				PlayerPrefs.Save();
				bountyConch.addActive(gameObject);
			} 
			//If the player clicks on a completed bounty, reward them on the first click
			else if (completed && !collected){
				Debug.Log ("PAYOUT");
				ScoreKeeper.Score += 1000;
				PlayerPrefs.SetInt ("bounty" + bountyNumber, -1); //-1 means it has been comleted, so cannot be done again
				collected = true;
				bountyConch.removeActive(gameObject);
			} else {
				Debug.LogError ("2 bounties already selected OR already collected reward");
			}
		} else Debug.LogError("ALREADY DONE");
	}

	// Update is called once per frame
	void Update () {
		//If this bounty# is active, update and check value for completion
		if(PlayerPrefsBool.GetBool ("bounty" + bountyNumber)) {
			//Update the counter display on the bounty object
			counter.text = (PlayerPrefs.GetInt("savedBounty" + bountyNumber) + "/" + (completeAmount)); 
			//If the savedBounty#'s value is equivalent to the complete amount, deactivate it
			if((PlayerPrefs.GetInt("savedBounty" + bountyNumber) >= completeAmount)){
				Debug.Log("BOUNTY COMPLETE!");
				counter.text = completeAmount + "/" + completeAmount;
				completed = true;
			}
		}
	}
}
