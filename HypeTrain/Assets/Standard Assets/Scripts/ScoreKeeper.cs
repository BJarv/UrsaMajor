using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int Score;
	void Awake () 
	{
		Score = 0;
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect (0, 0, 125, 25), "Loot Collected: $" + Score);
	}
}
