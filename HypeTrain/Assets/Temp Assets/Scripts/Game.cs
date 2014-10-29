using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game {
	public static Game current;
	public static int lifetimeLoot;
	public static int currLoot;
	public int carsCleared;
	public int enemiesKilled;
	public int hypeModesActivated;
	public int shotsFired;
	public int deaths;
	public float accuracy;

	public Game () { //constructor

	}

	public static void addLoot(int amount) {
		currLoot += amount;
		lifetimeLoot += amount;
		SaveLoad.Save ();
	}

}
