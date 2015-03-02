using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	private bool paused = false;
	public float vol = .35f;
	public float volOffset = .3f; //offset amount volume gets decreased during pause
	[HideInInspector] public GameObject player;
	[HideInInspector] public bool dead;

	void Start () {
		player = GameObject.Find ("character");
		paused = false;
		Time.timeScale = 1;
		AudioListener.volume = vol;
		Screen.showCursor = false;
	}
	
	void Update () {
		if(Input.GetKeyDown (KeyCode.Escape)){
			if(paused) { //unpause game if paused
				paused = false;
				Time.timeScale = 1;
				if(vol + volOffset > 1.0){ //properly adjust audio during pause and nonpause
					vol = 1.0f;
				} else if(vol <= 0) {
					vol = 0f;
				} else {
					vol += volOffset;
				}
				Screen.showCursor = false;

			} else if (!paused) { //pause game if not pause
				paused = true;
				Time.timeScale = 0;
				if(vol - volOffset < 0){ 
					vol = 0.0f;
				} else {
					vol -= volOffset;
				}

				Screen.showCursor = true;
			}
		}
		AudioListener.volume = vol; //VOLUME THAT IS SET EVERY FRAME HERE, could cause problems if edit sound in another place!!!!
	}

	void OnGUI() {
		if(paused) {
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 250, 200), "Paused"); //main background box
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 250, 50), "Main Menu")) {
				Application.LoadLevel ("MainMenu");
			}
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2, 250, 50), "Quit")) {
				Application.Quit ();
			}
			vol = GUI.HorizontalSlider (new Rect(Screen.width/2 - 100, Screen.height/2 + 75, 250, 50), vol, 0, 1); //set vol based on slider
		}
		if(player.transform.position.y < -15f || dead) {

			Time.timeScale = 0;
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 250, 300), "Press Any Key to Retry"); //main background box
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 250, 50), "Main Menu")) {
				Application.LoadLevel ("MainMenu");
			}
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2, 250, 50), "Shop")) {
				TutShopController.tutorial = false;
				TutShopController.shop = true;
				Application.LoadLevel (Application.loadedLevelName);
			}
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 + 50, 250, 50), "Tutorial")) {
				TutShopController.tutorial = true;
				Application.LoadLevel (Application.loadedLevelName);
			}
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 + 100, 250, 50), "Quit")) {
				Application.Quit ();
			}
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 150, 250, 25), "Cars Completed: " + ScoreKeeper.carsCompleted); //loot counter
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 175, 250, 25), "Total Cars Completed: " + Game.carsCleared); //loot counter
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 200, 250, 25), "Total Loot: " + Game.currLoot); //loot counter
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 175, 250, 25), "Total Cars Completed: " + PlayerPrefs.GetInt ("carsCleared")); //loot counter
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 200, 250, 25), "Total Loot: " + PlayerPrefs.GetInt ("currLoot")); //loot counter
			if(Input.anyKeyDown && !Input.GetMouseButton(0)){ //if any key is pressed that isnt a mouse button, delay is set in PlayerHealth
				//PlayerHealth.endOfLife = false;
				Time.timeScale = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}

	}
	
}
