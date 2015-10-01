using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bounty : MonoBehaviour {

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
	

	// Use this for initialization
	void Start () {
		//Set inspector defined text
		title.text = bountyName;
		info.text = description;
		counter.text = 0 + "/" + (completeAmount);
		bountyConch = transform.parent.transform.parent.GetComponent<BountyController> ();
		if(bountyName == "" || completeAmount == 0 || bountyNumber == -1 || description == "") {
			Debug.LogError(gameObject.name + " has missing values."); //ensure bounty is properly initialized in inspector, all these fields must have values
		} 

		/*else {
			//display = transform.Find ("counter").GetComponent<Text>(); //should be replaced for inspector references
			//mouseOverText = transform.Find ("description").GetComponent<Text>(); //should be replaced for inspector references
			//mouseOverText.enabled = false;

			//if this bounty is active, call choose(based on bounty number)
			if(PlayerPrefs.GetInt ("activeBounty1") == bountyNumber || PlayerPrefs.GetInt ("activeBounty2") == bountyNumber) { 
				//active = true;
				//choose ();
				//now called from within bountycontroller
			}
		}*/

	}

	//Called on start, or when choosen as a new bounty on click to prepare scripts to track values
	public void choose() { 
		//This check is used to reactivate bounties when the game is started
		/*if(PlayerPrefsBool.GetBool ("bounty" + bountyNumber) && PlayerPrefs.GetInt("savedBounty" + bountyNumber) != -1) {
			//Return the tracked value to the active bounty
			//valOnActivate = PlayerPrefs.GetInt("savedBounty" + bountyNumber);
			//grayOut.SetActive (false);
		} 

		//This check is used to activate bounties in-game for the first time
		else */

		//This check is used to activate bounties in-game for the first time
		if (!PlayerPrefsBool.GetBool ("bounty" + bountyNumber)){ //&& bountyConch.actives.Length < 2) { //on click, not active to ensure a second click doesnt save-over values
			bountyConch.addActive(gameObject);
			PlayerPrefsBool.SetBool ("bounty" + bountyNumber, true);
			PlayerPrefs.Save();
			//grayOut.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
		//If this bounty# is active, update and check values
		if(PlayerPrefsBool.GetBool ("bounty" + bountyNumber)) {
			//Update the counter display on the bounty object
			counter.text = (PlayerPrefs.GetInt("savedBounty" + bountyNumber) + "/" + (completeAmount)); 
			//If the savedBounty#'s value is equivalent to the complete amount, deactivate it
			if((PlayerPrefs.GetInt("savedBounty" + bountyNumber) >= completeAmount)){
				Debug.Log("BOUNTY COMPLETE!");
				PlayerPrefsBool.SetBool ("bounty" + bountyNumber, false);
				//grayOut.SetActive(true);
				counter.text = completeAmount + "/" + completeAmount;
			}
		}
	}
}
