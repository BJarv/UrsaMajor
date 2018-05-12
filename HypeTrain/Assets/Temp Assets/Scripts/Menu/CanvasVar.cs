using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasVar : LogController {

	private Text var;

    public enum ScoreVariable {
        TICKER_CARS_COMPLETED,
        TOTAL_CARS_COMPLETED,
        TICKER_KILLS,
        TOTAL_KILLS,
        TICKER_LOOT,
        TOTAL_LOOT,
        LIFETIME_LOOT,
        SONG_NAME
    }

    [Tooltip("Enumerated value which allows designer to easily access various common values for display.")]
    public ScoreVariable valueToDisplay;

	// Use this for initialization
	void Start () {
		var = transform.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //Retrieve value for text based on selected "valueToDisplay"
        switch (valueToDisplay) {
            case ScoreVariable.TICKER_CARS_COMPLETED:
                var.text = ScoreKeeper.DisplayCarsCompleted.ToString();
                break;
            case ScoreVariable.TOTAL_CARS_COMPLETED:
                var.text = ScoreKeeper.CarsCompleted.ToString();
                break;
            case ScoreVariable.TICKER_KILLS:
                var.text = ScoreKeeper.DisplayEnemiesKilled.ToString();
                break;
            case ScoreVariable.TOTAL_KILLS:
                var.text = ScoreKeeper.EnemiesKilled.ToString();
                break;
            case ScoreVariable.TICKER_LOOT:
                var.text = "$" + ScoreKeeper.DisplayScore.ToString();
                break;
            case ScoreVariable.TOTAL_LOOT:
                var.text = "$" + ScoreKeeper.Score.ToString();
                break;
            case ScoreVariable.LIFETIME_LOOT:
                var.text = "$" + PlayerPrefs.GetInt("lifetimeLoot");
                break;
            case ScoreVariable.SONG_NAME:
                var.text = "♪ " + Jukebox.trackName;
                break;
        }
	}
}