using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvasVar : MonoBehaviour {

	private Text var;

	public bool cars;
	public bool kills;
	public bool loot;
	public bool track;
	public bool totalLoot;

	// Use this for initialization
	void Start () {
		var = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cars) var.text = ScoreKeeper.DisplayCarsCompleted.ToString();
		else if(kills) var.text = ScoreKeeper.DisplayEnemiesKilled.ToString();
		else if(loot) var.text = "$" + ScoreKeeper.DisplayScore;
		else if(track) var.text = "♪ " + Jukebox.trackName;
		else if(totalLoot) var.text = "$" + PlayerPrefs.GetInt ("lifetimeLoot");
	}
}