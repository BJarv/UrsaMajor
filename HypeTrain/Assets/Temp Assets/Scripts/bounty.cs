using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bounty : MonoBehaviour {

	public string bountyName = "";
	public string description = "";
	public bool cumulative = false;
	int valOnActivate = 0;
	public int completeAmount = 0;
	public int bountyNumber = -1; //constructs key for playerprefs from this number
	public Text title;
	public Text info;
	public Text counter;

	//private Image picture;
	private Text mouseOverText;
	public bool active = false;
	//public GameObject grayOut;

	public bool completed = false;
	

	// Use this for initialization
	void Start () {
		//Set inspector defined text
		title.text = bountyName;
		info.text = description;
		if(bountyName == "" || completeAmount == 0 || bountyNumber == -1 || description == "") {
			Debug.LogError(gameObject.name + " has missing values."); //ensure bounty is properly initialized in inspector, all these fields must have values
		} else {
			//display = transform.Find ("counter").GetComponent<Text>(); //should be replaced for inspector references
			//mouseOverText = transform.Find ("description").GetComponent<Text>(); //should be replaced for inspector references
			//mouseOverText.enabled = false;

			//if this bounty is active, call choose(based on bounty number)
			if(PlayerPrefs.GetInt ("activeBounty1") == bountyNumber || PlayerPrefs.GetInt ("activeBounty2") == bountyNumber) { 
				//active = true;
				//choose ();
				//now called from within bountycontroller
			}
		}

	}

	//called on start, or when choosen as a new bounty on click to prepare scripts to track values
	public void choose() { 
		//This check is used to reactivate bounties when the game is started
		if(active && PlayerPrefs.GetInt("savedBounty" + bountyNumber) != -1) {
			//Return the tracked value to the active bounty
			valOnActivate = PlayerPrefs.GetInt("savedBounty" + bountyNumber);
			//grayOut.SetActive (false);
		} 

		//This check is used to activate bounties in-game for the first time
		else if (!active) { //on click, not active to ensure a second click doesnt save-over values
			transform.parent.GetComponent<BountyController>().addActive(gameObject);
			active = true;
			//PlayerPrefs.SetInt ("savedBounty" + bountyNumber, PlayerPrefs.GetInt ("bounty" + bountyNumber));
			PlayerPrefsBool.SetBool ("bounty" + bountyNumber, true);
			PlayerPrefs.Save();
			//grayOut.SetActive (false);
		}
	}


	public void updateVal() {
		counter.text = (PlayerPrefs.GetInt("savedBounty" + bountyNumber) - valOnActivate) + "/" + (completeAmount); //derived current amount out of complete amount ie 12/25
	}
	
	// Update is called once per frame
	void Update () {
		//If this bounty is active
		if(active) {
			//Update it's value every frame
			updateVal ();
			//If it's value is equivalent to the complete amount, deactivate it
			if((PlayerPrefs.GetInt("savedBounty" + bountyNumber) - valOnActivate) >= completeAmount) {
				active = false;
				//grayOut.SetActive(true);
				counter.text = completeAmount + "/" + completeAmount;

			}
		} else {

		}
	}
}
