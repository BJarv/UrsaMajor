using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasVar : LogController {

	private Text var;

    public enum ScoreVariable {
        TICKER_CARS_COMPLETED,
        CARS_COMPLETED,
        TICKER_KILLS,
        KILLS,
        TICKER_LOOT,
        LOOT,
        LIFETIME_LOOT,
        SONG_NAME
    }

    public ScoreVariable valueToDisplay;

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
        /*switch (testes) {
            case Test.TICKER_CARS_COMPLETED:
                var.text = ScoreKeeper.DisplayCarsCompleted.ToString();
                break;
            case Test.CARS_COMPLETED:
                var.text = ScoreKeeper.CarsCompleted.ToString();
                break;
            case Test.TICKER_KILLS:
                var.text = ScoreKeeper.DisplayEnemiesKilled.ToString();
                break;
            case Test.KILLS:
                var.text = ScoreKeeper.EnemiesKilled.ToString();
                break;
            case Test.TICKER_LOOT:
                var.text = "$" + ScoreKeeper.DisplayScore.ToString();
                break;
            case Test.LOOT:
                var.text = "$" + ScoreKeeper.Score.ToString();
                break;
            case Test.LIFETIME_LOOT:
                var.text = "$" + PlayerPrefs.GetInt("lifetimeLoot");
                break;
            case Test.SONG_NAME:
                var.text = "♪ " + Jukebox.trackName;
                break;
        }*/
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