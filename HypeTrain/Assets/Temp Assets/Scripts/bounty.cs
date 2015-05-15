using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bounty : MonoBehaviour {

	public string bountyName = "";
	public bool cumulative = false;
	int valAtStart = 0;
	public int completeAmount = 0;
	public int valToTrack = -1; //constructs key for playerprefs from this number
	private Text display;
	//private Image picture;
	public string description = "";
	private Text mouseOverText;
	public bool active = false;
	public GameObject grayOut;
	

	// Use this for initialization
	void Start () {
		if(name == "" || completeAmount == 0 || valToTrack == -1 || description == "") {
			Debug.LogError(gameObject.name + " has missing values.");
		} else {
			display = transform.Find ("counter").GetComponent<Text>(); //should be replaced for inspector references
			mouseOverText = transform.Find ("description").GetComponent<Text>(); //should be replaced for inspector references
			mouseOverText.enabled = false;
			if(PlayerPrefs.GetInt ("activeBounty1") == valToTrack || PlayerPrefs.GetInt ("activeBounty2") == valToTrack){
				active = true;
				choose ();
			}
		}

	}

	public void choose() { //called on start, or when choosen as a new bounty on click
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
		display.text = (PlayerPrefs.GetInt("bounty" + valToTrack) - valAtStart) + "/" + completeAmount;
	}
	
	// Update is called once per frame
	void Update () {
		if(active) {
			updateVal ();
		}
	}
}
