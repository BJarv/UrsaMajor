using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bounty : MonoBehaviour {

	public string bountyName = "";
	public string description = "";
	public bool cumulative = false;
	int valAtStart = 0;
	public int completeAmount = 0;
	public int valToTrack = -1; //constructs key for playerprefs from this number
	public Text title;
	public Text info;
	public Text counter;

	//private Image picture;
	private Text mouseOverText;
	public bool active = false;
	public GameObject grayOut;
	

	// Use this for initialization
	void Start () {
		//Set inspector defined text
		title.text = bountyName;
		info.text = description;
		if(bountyName == "" || completeAmount == 0 || valToTrack == -1 || description == "") {
			Debug.LogError(gameObject.name + " has missing values."); //ensure bounty is properly initialized in inspector, all these fields must have values
		} else {
			//display = transform.Find ("counter").GetComponent<Text>(); //should be replaced for inspector references
			//mouseOverText = transform.Find ("description").GetComponent<Text>(); //should be replaced for inspector references
			//mouseOverText.enabled = false;

			if(PlayerPrefs.GetInt ("activeBounty1") == valToTrack || PlayerPrefs.GetInt ("activeBounty2") == valToTrack) { //if this bounty is active, call choose(based on bounty number)
				active = true;
				choose ();
			}
		}

	}

	public void choose() { //called on start, or when choosen as a new bounty on click to prepare scripts to track values
		if(active && PlayerPrefs.GetInt("savedBounty" + valToTrack) != -1) { //on start
			valAtStart = PlayerPrefs.GetInt("savedBounty" + valToTrack);
			grayOut.SetActive (false);
		} else if (!active) { //on click, not active to ensure a second click doesnt save-over values
			active = true;
			PlayerPrefs.SetInt ("savedBounty" + valToTrack, PlayerPrefs.GetInt ("bounty" + valToTrack));
			valAtStart = PlayerPrefs.GetInt ("savedBounty" + valToTrack);
			grayOut.SetActive (false);
		}
	}


	public void updateVal() {
		counter.text = (PlayerPrefs.GetInt("savedBounty" + valToTrack) - valAtStart) + "/" + (completeAmount); //derived current amount out of complete amount ie 12/25
	}
	
	// Update is called once per frame
	void Update () {
		if(active) {
			updateVal ();
		}
	}
}
