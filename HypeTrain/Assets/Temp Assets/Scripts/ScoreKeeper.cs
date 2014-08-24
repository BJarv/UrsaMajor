using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public GUIStyle scoreStyle;

	public static int Score;
	void Awake () 
	{
		Score = 0;
	}

	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label (new Rect (7, 3, 500, 25), "Loot: $" + Score, scoreStyle);
	}
}
