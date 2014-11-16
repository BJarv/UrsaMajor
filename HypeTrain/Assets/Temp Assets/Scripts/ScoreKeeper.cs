using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int Score;
	public static string HYPE;

	void Awake () 
	{
		Score = 0;
		HYPE = "LAME";
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label (new Rect (7, 3, 500, 25), "Loot: $" + Score, scoreStyle);
		GUI.Label (new Rect (150, 3, 500, 25), "HYPE: " + HYPE, scoreStyle);
	}

	/*void setHype(){
		Increment the HYPE up one each time. (LAME->GNAR->HYPE->OVERHYPED)
	}*/
}
