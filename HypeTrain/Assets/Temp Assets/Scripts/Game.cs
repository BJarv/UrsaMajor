using UnityEngine;
using System.Collections;

[System.Serializable]
public class Game {
	public static Game current;
	/*public static Game current;
	public static int lifetimeLoot;
	public static int currLoot;
	public static int skin;
	public static int hype;
	public static int carsCleared;
	public int enemiesKilled;
	public int hypeModesActivated;
	public int shotsFired;
	public int deaths;
	public float accuracy;
	public static bool skin1 = true;
	public static bool skin2 = false;
	public static bool skin3 = false;
	public static bool skin4 = false;
	public static bool hype1 = true;
	public static bool hype2 = false;
	public static bool firstTime = true;
	public static bool firstTime = true;*/

	void Start() {
		if(!PlayerPrefs.HasKey ("lifetimeLoot")) {
			PlayerPrefs.SetInt ("lifetimeLoot", 0);
			PlayerPrefs.SetInt ("currLoot", 0);
			PlayerPrefs.SetInt ("skin", 0);
			PlayerPrefs.SetInt ("hype", 0);
			PlayerPrefs.SetInt ("carsCleared", 0);
			PlayerPrefs.SetInt ("enemiesKilled", 0);
			PlayerPrefs.SetInt ("hypeModesActivated", 0);
			PlayerPrefs.SetInt ("shotsFired", 0);
			PlayerPrefs.SetInt ("deaths", 0);
			PlayerPrefs.SetFloat ("accuracy", 0);
			PlayerPrefsBool.SetBool ("skin1", true);
			PlayerPrefsBool.SetBool ("skin2", false);
			PlayerPrefsBool.SetBool ("skin3", false);
			PlayerPrefsBool.SetBool ("skin4", false);
			PlayerPrefsBool.SetBool ("hype1", true);
			PlayerPrefsBool.SetBool ("hype2", false);
			PlayerPrefsBool.SetBool ("firstTime", true);
		}
	}

	public Game () { //constructor

	}

	public static void addLoot(int amount) {
		currLoot += amount;
		lifetimeLoot += amount;
		SaveLoad.Save ();
		PlayerPrefs.SetInt ("currLoot", (PlayerPrefs.GetInt ("currLoot") + amount));
		PlayerPrefs.SetInt ("lifetimeLoot", (PlayerPrefs.GetInt ("lifetimeLoot") + amount));
		PlayerPrefs.Save ();
	}

	public static void addCarsCleared(int amount) {
		carsCleared += amount;
		SaveLoad.Save ();
		PlayerPrefs.SetInt ("carsCleared", (PlayerPrefs.GetInt ("carsCleared") + amount));
		PlayerPrefs.Save ();
	}

}
