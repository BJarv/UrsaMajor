using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class bountyMock : MonoBehaviour {

	public bool Cumulative = false;
	public int total = 0;
	public string varToTrack = "enemiesKilled";
	private int counter = 0;

	//private Text display;

	// Use this for initialization
	void Start () {
		//isplay = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(!Cumulative && PlayerHealth == 0) counter = 0;
		if (Cumulative) {
			//counter += varToTrack;
			//display = counter.ToString() +"/" + total.ToString();
		} else {
			//counter = varToTrack;
		}
		if(counter == total){
			Debug.Log ("Bounty complete!");
		} //completed 
	}
}
