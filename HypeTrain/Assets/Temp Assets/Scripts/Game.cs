using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game {
	public static Game current;
	public int lifetimeLoot;
	public int currLoot;
	public int carsCleared;
	public int enemiesKilled;
	public int hypeModesActivated;
	public int shotsFired;
	public int deaths;
	public float accuracy;

	public Game () {


	}

}
