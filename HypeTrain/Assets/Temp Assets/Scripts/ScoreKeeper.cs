using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int carsCompleted;
	public static int enemiesKilled;
	public static int Score;
	public static int DisplayScore;
	public static int HYPE;
	public static bool HYPED;

	public float scoreTickSpeed = 0.01f;
	public int scoreTickInterval = 10;

	void Awake () {
		carsCompleted = 0;
		enemiesKilled = 0;
		Score = 0;
		DisplayScore = 0;
		HYPE = 6;
	}

	void Start () {
		StartCoroutine("Ticker");
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public int incrementHype(bool increment){ 
		if 		(HYPE < 6 && !HYPED) HYPE++;
		else if (HYPE == 6 && !increment) HYPE = 0;

		return HYPE;
	}

	//this will increment the CurrentScore towards the TargetScore over time
	public IEnumerator Ticker()
	{
		//this loop will run forever so you can just call AddScore and the ticker will continue automatically
		while (true){
			//we don't want to increment CurrentScore to infinity, so we only do it if it's lower than TargetScore
			if (ScoreKeeper.DisplayScore < ScoreKeeper.Score){
				ScoreKeeper.DisplayScore += scoreTickInterval;

				//this is a 'safety net' to ensure we never exceed our TargetScore
				if (ScoreKeeper.DisplayScore > ScoreKeeper.Score){
					ScoreKeeper.DisplayScore = ScoreKeeper.Score;
				}
			}
			//wait for some time before incrementing again
			yield return new WaitForSeconds(scoreTickSpeed);
		}
	}

}
