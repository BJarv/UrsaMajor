using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int carsCompleted;
	public static int Score;
	public static int HYPE;
	//public GameObject HYPEBAR;
	public static bool HYPED;

	void Awake () 
	{
		carsCompleted = 0;
		Score = 0;
		HYPE = 0; //CHANGE BACK TO CHILL
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label (new Rect (260, 60, 1000, 500), "$" + Score, scoreStyle);
		GUI.Label (new Rect (260, 180, 1000, 500), "Completed:" + carsCompleted, scoreStyle);
	}

	//Called to increment HYPE level by 1 on kill, or reset upon entering HYPE Mode
	public int incrementHype(bool increment){ 
		if 		(HYPE == 0 && !HYPED) HYPE = 1;
		else if (HYPE == 1 && !HYPED) HYPE = 2;
		else if (HYPE == 2 && !HYPED) HYPE = 3;
		else if (HYPE == 3 && !increment) HYPE = 0;

		return HYPE;
	}

}
