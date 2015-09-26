using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int CarsCompleted;
	public static int DisplayCarsCompleted;
	public static int EnemiesKilled;
	public static int DisplayEnemiesKilled;
	public static int Score;
	public static int DisplayScore;

	public static int HYPE;
	public static bool HYPED;

	public float carsTickSpeed = 0.5f;
	public float killsTickSpeed = 0.25f;
	public float scoreTickSpeed = 0.01f;
	public int scoreTickInterval = 10;

	private GameObject cameraObj;

	void Awake () {
		DisplayCarsCompleted = 0;
		CarsCompleted = 0;
		DisplayEnemiesKilled = 0;
		EnemiesKilled = 0;
		Score = 0;
		DisplayScore = 0;
		HYPE = 6;
	}

	void Start () {
		StartCoroutine("ScoreTicker");
		cameraObj = GameObject.Find("Main Camera");
	}

	void Update () {
		if(cameraObj.GetComponent<Popup>().dead) {
			StartCoroutine("AllTicker");
		}
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public int incrementHype(bool increment){ 
		if 		(HYPE < 6 && !HYPED) HYPE++;
		else if (HYPE == 6 && !increment) HYPE = 0;

		return HYPE;
	}

	//RUNS ALWAYS
	//this will increment the CurrentScore towards the TargetScore over time
	public IEnumerator ScoreTicker()
	{
		//this loop will run forever so you can just call AddScore and the ticker will continue automatically
		while (true){
			//Use the Ticker function to rapidly increment toward the actual score
			ScoreKeeper.DisplayScore = Ticker(ScoreKeeper.DisplayScore, ScoreKeeper.Score, scoreTickInterval);
			//wait for some time before incrementing again
			yield return new WaitForSeconds(scoreTickSpeed);
		}
	}

	//RUNS ON DEATH
	//this will increment all values towards their target values over time
	public IEnumerator AllTicker(){
		//this loop will run forever so you can just call AddScore and the ticker will continue automatically
		while (true){
			//we don't want to increment CurrentScore to infinity, so we only do it if it's lower than TargetScore
			ScoreKeeper.DisplayEnemiesKilled = Ticker(ScoreKeeper.DisplayEnemiesKilled, ScoreKeeper.EnemiesKilled, 1);
			//wait for some time before incrementing again
			yield return new WaitForSeconds(carsTickSpeed);
		}
	}

	public int Ticker (int display, int actual, int interval){
		//we don't want to increment CurrentScore to infinity, so we only do it if it's lower than TargetScore
		if (display < actual){
			display += interval;
			
			//this is a 'safety net' to ensure we never exceed our TargetScore
			if (display > actual){
				display = actual;
			}
		}
		return display;
	}

}
