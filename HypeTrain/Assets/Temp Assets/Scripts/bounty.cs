using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bounty : MonoBehaviour {

	public string bountyName = "";
	public bool cumulative = false;
	int valAtStart = 0;
	public int completeAmount = 0;
	public string valToTrack = "";
	private Text display;
	//private Image picture;
	public string description = "";
	private Text mouseOverText;
	

	// Use this for initialization
	void Start () {
		if(name == "" || completeAmount == 0 || valToTrack == "" || description == "") {
			Debug.LogError(gameObject.name + " has missing values.");
		} else {
			display = transform.Find ("counter").GetComponent<Text>();
			updateVal();
			mouseOverText = transform.Find ("description").GetComponent<Text>();
			mouseOverText.enabled = false;
		}

	}

	public void choose() {
		valAtStart = PlayerPrefs.GetInt(valToTrack);

	}

	public void updateVal() {
		display.text = (PlayerPrefs.GetInt(valToTrack) - valAtStart) + "/" + completeAmount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
