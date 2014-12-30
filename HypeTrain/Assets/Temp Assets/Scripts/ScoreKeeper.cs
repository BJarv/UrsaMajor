using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int carsCompleted;
	public static int Score;
	public static string HYPE;
	//public GameObject HYPEBAR;
	public static bool HYPED;

	void Awake () 
	{
		carsCompleted = 0;
		Score = 0;
		HYPE = "HYPE"; //CHANGE BACK TO CHILL
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label (new Rect (7, 3, 500, 25), "Loot: $" + Score, scoreStyle);
		GUI.Label (new Rect (150, 3, 500, 25), "HYPE: " + HYPE, scoreStyle);
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public string incrementHype(bool increment){ 
		if 		(HYPE == "CHILL" && !HYPED) HYPE = "RAD";
		else if (HYPE == "RAD" && !HYPED) HYPE = "GNAR";
		else if (HYPE == "GNAR" && !HYPED) HYPE = "HYPE";
		else if (HYPE == "HYPE" && !increment) HYPE = "CHILL";

		return HYPE;
	}

}
