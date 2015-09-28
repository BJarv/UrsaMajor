using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Popup : MonoBehaviour {

	public static bool paused = false;
	private float unmuteVolume;
	CameraShake shaker;

	public Sprite unmuted;
	public Sprite muted;


	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject pauseMenu;
	[HideInInspector] public GameObject deathMenu;
	[HideInInspector] public GameObject pSlide;
	[HideInInspector] public GameObject dSlide; 
	[HideInInspector] public GameObject pMute;
	[HideInInspector] public GameObject dMute;
	[HideInInspector] public bool dead;

	public BountyController bountyConch;



	void Start () {
		shaker = transform.parent.GetComponent<CameraShake>();
		AudioListener.volume = PlayerPrefs.GetFloat ("volume");
		unmuteVolume = PlayerPrefs.GetFloat ("volume");
		player = GameObject.Find ("character");
		//Set volume sliders to saved Pref
		pSlide = GameObject.Find ("pSlider");
		pSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
		dSlide = GameObject.Find ("dSlider");
		dSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
		pMute = GameObject.Find ("pMuteButton");
		dMute = GameObject.Find ("dMuteButton");
		//Find references to the pause and death menus, then disable them
		pauseMenu = GameObject.Find ("Pause");
		pauseMenu.SetActive(false);
		paused = false;
		deathMenu = GameObject.Find ("Death");
		deathMenu.SetActive(false);
		Time.timeScale = 1;
		Cursor.visible = false;
	}

	void Update () {
		if(Input.GetButtonDown ("Start") && !ShopKeeper.isOnScreen){
			if(paused) { //unpause game if paused
				paused = false;
				Time.timeScale = 1;
				pauseMenu.SetActive(false);

				Cursor.visible = false;
				bountyConch.unpauseBounties(); //remove active bounties from pause menu	

			} else if (!paused) { //pause game if not pause
				shaker.stopAllShake();
				paused = true;
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
				Cursor.visible = true;

				//Update slider position
				pSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
 
				Cursor.visible = true;
				//Debug.Log (bountyConch);
				bountyConch.pauseBounties(); //show active bounties on pause menu	
			}
		}
		if(player.transform.position.y < -15f || dead) {
			paused = true;
			shaker.stopAllShake();
			Time.timeScale = 0;
			Cursor.visible = true;

			deathMenu.SetActive(true);
			//Update slider position
			dSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
			bountyConch.pauseBounties(); //show active bounties on death menu


			if((Input.anyKeyDown || Input.GetButton ("Submit") || Input.GetAxis ("LTrig") > 0.1) && !Input.GetMouseButton(0)){ //if any key is pressed that isnt a mouse button, delay is set in PlayerHealth
				deathMenu.SetActive(false);
				Cursor.visible = false;
				Time.timeScale = 1;
				Application.LoadLevel (Application.loadedLevelName);
			}
		}
	}

	public void setVolume(float newVol){
		//Update the local volume and pref
		AudioListener.volume = newVol;
		PlayerPrefs.SetFloat ("volume", newVol);
	}

	//Function called by the mute button
	public void muteButton(){
		//If volume is not zero, mute and update sliders
		if (PlayerPrefs.GetFloat ("volume") != 0) {
			pMute.GetComponent<UnityEngine.UI.Image>().overrideSprite = muted;
			dMute.GetComponent<UnityEngine.UI.Image>().overrideSprite = muted;
			unmuteVolume = PlayerPrefs.GetFloat ("volume");
			AudioListener.volume = 0;
			PlayerPrefs.SetFloat ("volume", 0);
			pSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
			dSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
		} 
		//Otherwise return to last saved volume and update sliders
		else {
			pMute.GetComponent<UnityEngine.UI.Image>().overrideSprite = unmuted;
			dMute.GetComponent<UnityEngine.UI.Image>().overrideSprite = unmuted;
			AudioListener.volume = unmuteVolume;
			PlayerPrefs.SetFloat ("volume", unmuteVolume);
			pSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
			dSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
		}
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

}
