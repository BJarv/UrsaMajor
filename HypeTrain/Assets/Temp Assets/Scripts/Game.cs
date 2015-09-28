using UnityEngine;
using System.Collections;

//[System.Serializable]
public class Game : MonoBehaviour{
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
		//Create all playerprefs if they don't exist yet
		if(!PlayerPrefs.HasKey ("lifetimeLoot")) {
			PlayerPrefs.SetInt ("lifetimeLoot", 0);
			PlayerPrefs.SetInt ("currLoot", 0);
			PlayerPrefs.SetInt ("skin", 0);
			PlayerPrefs.SetInt ("hype", 0);
			PlayerPrefs.SetInt ("carsCleared", 0);
			PlayerPrefs.SetInt ("enemiesKilled", 0);
			PlayerPrefs.SetInt ("bossesKilled", 0);
			PlayerPrefs.SetInt ("hypeModesActivated", 0);
			PlayerPrefs.SetInt ("shotsFired", 0);
			PlayerPrefs.SetInt ("deaths", 0);
			PlayerPrefs.SetInt ("track", 1);
			PlayerPrefs.SetFloat ("volume", 1);
			PlayerPrefs.SetFloat ("accuracy", 0);
			PlayerPrefsBool.SetBool ("skin1", true);
			PlayerPrefsBool.SetBool ("skin2", false);
			PlayerPrefsBool.SetBool ("skin3", false);
			PlayerPrefsBool.SetBool ("skin4", false);
			PlayerPrefsBool.SetBool ("hype1", true);
			PlayerPrefsBool.SetBool ("hype2", false);
			//Bool used to run tutorial on the first play
			PlayerPrefsBool.SetBool ("firstTime", true);
			//these need to be commented with names
			PlayerPrefsBool.SetBool ("bounty1", false);
			PlayerPrefsBool.SetBool ("bounty2", false);
			PlayerPrefsBool.SetBool ("bounty3", false);
			PlayerPrefsBool.SetBool ("bounty4", false);
			PlayerPrefsBool.SetBool ("bounty5", false);
			PlayerPrefsBool.SetBool ("bounty6", false);
			PlayerPrefsBool.SetBool ("bounty7", false);
			PlayerPrefsBool.SetBool ("bounty8", false);
			PlayerPrefsBool.SetBool ("bounty9", false);
			PlayerPrefsBool.SetBool ("bounty10", false);
			PlayerPrefsBool.SetBool ("bounty11", false);
			PlayerPrefsBool.SetBool ("bounty12", false);
			PlayerPrefsBool.SetBool ("bounty13", false);
			PlayerPrefsBool.SetBool ("bounty14", false);
			PlayerPrefsBool.SetBool ("bounty15", false);
			PlayerPrefsBool.SetBool ("bounty16", false);

			//STORES THE ALL THE COMPLETION VALUES OF BOUNTIES.
			//-1 for not being tracked yet. also need to be name commented
			PlayerPrefs.SetInt ("savedBounty1", -1);
			PlayerPrefs.SetInt ("savedBounty2", -1);
			PlayerPrefs.SetInt ("savedBounty3", -1);
			PlayerPrefs.SetInt ("savedBounty4", -1);
			PlayerPrefs.SetInt ("savedBounty5", -1);
			PlayerPrefs.SetInt ("savedBounty6", -1);
			PlayerPrefs.SetInt ("savedBounty7", -1);
			PlayerPrefs.SetInt ("savedBounty8", -1);
			PlayerPrefs.SetInt ("savedBounty9", -1);
			PlayerPrefs.SetInt ("savedBounty10", -1);
			PlayerPrefs.SetInt ("savedBounty11", -1);
			PlayerPrefs.SetInt ("savedBounty12", -1);
			PlayerPrefs.SetInt ("savedBounty13", -1);
			PlayerPrefs.SetInt ("savedBounty14", -1);
			PlayerPrefs.SetInt ("savedBounty15", -1);
			PlayerPrefs.SetInt ("savedBounty16", -1);
			//currently active bounties
			PlayerPrefs.SetInt ("activeBounty1", -1);
			PlayerPrefs.SetInt ("activeBounty2", -1);
		}
	}

	public static void addLoot(int amount) {
		PlayerPrefs.SetInt ("currLoot", (PlayerPrefs.GetInt ("currLoot") + amount));
		PlayerPrefs.SetInt ("lifetimeLoot", (PlayerPrefs.GetInt ("lifetimeLoot") + amount));
		PlayerPrefs.Save ();
	}

	public static void addCarsCleared(int amount) {
		PlayerPrefs.SetInt ("carsCleared", (PlayerPrefs.GetInt ("carsCleared") + amount));
		PlayerPrefs.Save ();
	}
	public static void incEnemiesKilled() {
		PlayerPrefs.SetInt ("enemiesKilled", (PlayerPrefs.GetInt ("enemiesKilled") + 1));
		PlayerPrefs.Save ();
	}

	public static void incDeaths() {
		PlayerPrefs.SetInt ("deaths", (PlayerPrefs.GetInt ("deaths") + 1));
		PlayerPrefs.Save ();
	}

	public static void incBossesKilled() {
		PlayerPrefs.SetInt ("bossesKilled", (PlayerPrefs.GetInt ("bossesKilled") + 1));
		PlayerPrefs.Save ();
	}

	public static void incBounty(int bountyNo){
		if(PlayerPrefsBool.GetBool ("bounty" + bountyNo)){
			PlayerPrefs.SetInt ("savedBounty" + bountyNo, (PlayerPrefs.GetInt ("savedBounty" + bountyNo) + 1));
			PlayerPrefs.Save ();
		}
	}
}
