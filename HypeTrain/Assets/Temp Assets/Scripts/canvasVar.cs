using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class canvasVar : MonoBehaviour {

	private Text var;

	public bool cars;
	public bool kills;
	public bool loot;
	public bool dCars;
	public bool dKills;
	public bool dLoot;
	public bool track;
	public bool totalLoot;

	// Use this for initialization
	void Start () {
		var = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cars) var.text = ScoreKeeper.CarsCompleted.ToString();
		else if(kills) var.text = ScoreKeeper.EnemiesKilled.ToString();
		else if(loot) var.text = "$" + ScoreKeeper.Score;
		else if(dCars) var.text = ScoreKeeper.DisplayCarsCompleted.ToString();
		else if(dKills) var.text = ScoreKeeper.DisplayEnemiesKilled.ToString();
		else if(dLoot) var.text = "$" + ScoreKeeper.DisplayScore;
		else if(track) var.text = "♪ " + Jukebox.trackName;
		else if(totalLoot) var.text = "$" + PlayerPrefs.GetInt ("lifetimeLoot");
	}
}