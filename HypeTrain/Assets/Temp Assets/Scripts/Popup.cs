using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	private bool paused = false;


	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject pauseMenu;
	[HideInInspector] public GameObject deathMenu;
	[HideInInspector] public bool dead;

	void Start () {
		player = GameObject.Find ("character");
		pauseMenu = GameObject.Find ("Pause");
		pauseMenu.SetActive(false);
		paused = false;
		deathMenu = GameObject.Find ("Death");
		deathMenu.SetActive(false);
		Time.timeScale = 1;
		//AudioListener.volume = vol;  //VOLUME SET ON NEW UI
		Cursor.visible = false;
	}

	void Update () {
		if(Input.GetButtonDown ("Start")){
			if(paused) { //unpause game if paused
				paused = false;
				Time.timeScale = 1;
				pauseMenu.SetActive(false);

				Cursor.visible = false;

			} else if (!paused) { //pause game if not pause
				paused = true;
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
 
				Cursor.visible = true;
			}
		}
		if(player.transform.position.y < -15f || dead) {
			Time.timeScale = 0;

			deathMenu.SetActive(true);

			if((Input.anyKeyDown || Input.GetButton ("Submit") || Input.GetAxis ("LTrig") > 0.1) && !Input.GetMouseButton(0)){ //if any key is pressed that isnt a mouse button, delay is set in PlayerHealth
				deathMenu.SetActive(false);
				Time.timeScale = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}

	public void setVolume(float newVol){
		AudioListener.volume = newVol;
	}

	public void pauseButton(){
		//unpause game
		paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
			
		Cursor.visible = false;
	}

	public void returnMain(){
		Application.LoadLevel ("MainMenu");
	}



	/*void OnGUI() {
		if(paused) {
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 250, 200), "Paused"); //main background box
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 250, 50), "Main Menu")) {
				Application.LoadLevel ("MainMenu");
			}
			if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2, 250, 50), "Quit")) {
				Application.Quit ();
			}
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
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 175, 250, 25), "Total Cars Completed: " + PlayerPrefs.GetInt ("carsCleared")); //loot counter
			GUI.Box (new Rect(Screen.width/2 - 100, Screen.height/2 + 200, 250, 25), "Total Loot: " + PlayerPrefs.GetInt ("currLoot")); //loot counter
			if((Input.anyKeyDown || Input.GetButton ("Submit") || Input.GetAxis ("LTrig") > 0.1) && !Input.GetMouseButton(0)){ //if any key is pressed that isnt a mouse button, delay is set in PlayerHealth
				//PlayerHealth.endOfLife = false;
				Time.timeScale = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}

	}*/
	
}
