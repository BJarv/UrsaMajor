using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int carsCompleted;
	public static int Score;
	public static string HYPE;
	public static bool HYPED;

	void Awake () 
	{
		carsCompleted = 0;
		Score = 0;
		HYPE = "LAME";
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label (new Rect (7, 3, 500, 25), "Loot: $" + Score, scoreStyle);
		GUI.Label (new Rect (150, 3, 500, 25), "HYPE: " + HYPE, scoreStyle);
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public string incrementHype(bool increment){ 
		if 		(HYPE == "LAME" && !HYPED) HYPE = "GNAR";
		else if (HYPE == "GNAR" && !HYPED) HYPE = "HYPE";
		else if (HYPE == "HYPE" && !HYPED) HYPE = "OVERHYPED";
		else if (HYPE == "OVERHYPED" && !increment) HYPE = "LAME";

		return HYPE;
	}

}
