using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int carsCompleted;
	public static int enemiesKilled;
	public static int Score;
	public static int HYPE;
	//public GameObject HYPEBAR;
	public static bool HYPED;

	void Awake () {
		carsCompleted = 0;
		enemiesKilled = 0;
		Score = 0;
		HYPE = 6;
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public int incrementHype(bool increment){ 
		if 		(HYPE < 6 && !HYPED) HYPE++;
		else if (HYPE == 6 && !increment) HYPE = 0;

		return HYPE;
	}

}
