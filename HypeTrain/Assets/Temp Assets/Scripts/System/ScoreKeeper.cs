using UnityEngine;
using System.Collections;

public class ScoreKeeper : LogController {

	public GUIStyle scoreStyle;

	public static int CarsCompleted;
	public static int DisplayCarsCompleted;
	public static int EnemiesKilled;
	public static int DisplayEnemiesKilled;
	public static int Score;
	public static int DisplayScore;

	public static int HYPE;
	public static bool HYPED;

	public float carsTickSpeed = 0.25f;
	public float killsTickSpeed = 0.12f;
	public float scoreTickSpeed = 0.01f;
	public int scoreTickInterval = 10;

	public AudioClip carTickSound;
	public AudioClip killTickSound;
	public AudioClip scoreTickSound;

	//public Vector2 velocity = new Vector2 (.5f, .5f);

	void Awake () {
		DisplayCarsCompleted = 0;
		CarsCompleted = 4;
		DisplayEnemiesKilled = 0;
		EnemiesKilled = 9;
		DisplayScore = 0;
		Score = 1200;
		HYPE = 6;
	}

	void Start () {
		StartCoroutine("AliveScoreTicker");
		StartCoroutine("CarTicker");
		StartCoroutine("KillTicker");
		StartCoroutine("ScoreTicker");
	}

	void Update () {
		//ScoreKeeper.DisplayCarsCompleted = ScoreKeeper.CarsCompleted;
		//ScoreKeeper.DisplayEnemiesKilled = ScoreKeeper.EnemiesKilled;
		//ScoreKeeper.DisplayScore = ScoreKeeper.Score;
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public int incrementHype(bool increment){ 
		if 		(HYPE < 6 && !HYPED) HYPE++;
		else if (HYPE == 6 && !increment) HYPE = 0;

		return HYPE;
	}
	
	//RUNS ALWAYS
	//this will increment the CurrentScore towards the TargetScore over time
	public IEnumerator AliveScoreTicker(){
		//this loop will run forever so you can just call AddScore and the ticker will continue automatically
		while (true){
			if(!PlayerCharacter.playerDead){
				//Use the Ticker function to rapidly increment toward the actual score
				ScoreKeeper.DisplayScore = Ticker(ScoreKeeper.DisplayScore, ScoreKeeper.Score, scoreTickInterval);
            }
			//wait for some time before incrementing again
			yield return new WaitForSeconds(scoreTickSpeed);
		}
	}

	//RUNS ON DEATH
	//Increment CARS COMPLETED on death
	public IEnumerator CarTicker(){
		while (true){
			if(PlayerCharacter.playerDead && ScoreKeeper.DisplayCarsCompleted != ScoreKeeper.CarsCompleted){
				ScoreKeeper.DisplayCarsCompleted = Ticker(ScoreKeeper.DisplayCarsCompleted, ScoreKeeper.CarsCompleted, 1);
				AudioSource.PlayClipAtPoint(carTickSound, Camera.main.transform.position);
			}
			yield return new WaitForSeconds(carsTickSpeed); //wait for some time before incrementing again
		}
	}
	//Increment KILLS on death
	public IEnumerator KillTicker(){
		while (true){
			if(PlayerCharacter.playerDead && ScoreKeeper.DisplayCarsCompleted == ScoreKeeper.CarsCompleted && ScoreKeeper.DisplayEnemiesKilled != ScoreKeeper.EnemiesKilled){
				ScoreKeeper.DisplayEnemiesKilled = Ticker(ScoreKeeper.DisplayEnemiesKilled, ScoreKeeper.EnemiesKilled, 1);
				AudioSource.PlayClipAtPoint(killTickSound, Camera.main.transform.position);
			}
			yield return new WaitForSeconds(killsTickSpeed); //wait for some time before incrementing again
		}
	}
	//Increment SCORE on death
	public IEnumerator ScoreTicker(){
		while (true){
			if(PlayerCharacter.playerDead && ScoreKeeper.DisplayEnemiesKilled == ScoreKeeper.EnemiesKilled && ScoreKeeper.DisplayScore != ScoreKeeper.Score){
				ScoreKeeper.DisplayScore = Ticker(ScoreKeeper.DisplayScore, ScoreKeeper.Score, scoreTickInterval);
				AudioSource.PlayClipAtPoint(scoreTickSound, Camera.main.transform.position);
			}
			yield return new WaitForSeconds(scoreTickSpeed); //wait for some time before incrementing again
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
