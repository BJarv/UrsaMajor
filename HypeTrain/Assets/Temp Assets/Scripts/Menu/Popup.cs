using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popup : LogController {

	public static bool paused = false;
	private float unmuteVolume;
	CameraShake cameraShake;

	public Sprite unmutedSprite;
	public Sprite mutedSprite;

	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject pauseMenu;
	[HideInInspector] public GameObject deathMenu;
	[HideInInspector] public GameObject pSlide;
	[HideInInspector] public GameObject dSlide; 
	[HideInInspector] public GameObject pMute;
	[HideInInspector] public GameObject dMute;
	[HideInInspector] public bool dead;

	public BountyController bountyController;

    private void OnEnable()
    {
        EventManager.StartListening("PlayerDeath", OnDeath);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerDeath", OnDeath);
    }

    void Start () {
		bountyController = GameObject.Find ("BountyCanvas").GetComponent<BountyController> ();

		cameraShake = transform.parent.GetComponent<CameraShake>();
		AudioListener.volume = PlayerPrefs.GetFloat ("volume");
		unmuteVolume = PlayerPrefs.GetFloat ("volume");
		player = GameObject.Find ("Player");
		//Set volume sliders to saved Pref
		pSlide = GameObject.Find ("pSlider");
		dSlide = GameObject.Find ("dSlider");
		pMute = GameObject.Find ("pMuteButton");
		dMute = GameObject.Find ("dMuteButton");

		pSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
		dSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");


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
				bountyController.unpauseBounties(); //remove active bounties from pause menu	

			} else if (!paused) { //pause game if not pause
				cameraShake.stopAllShake();
				paused = true;
				Time.timeScale = 0;
				pauseMenu.SetActive(true);

				Cursor.visible = true;
				bountyController.pauseBounties(); //show active bounties on pause menu	
			}
		}

		//Stop game and show pause menu on death
		if(dead) {
			if((Input.anyKeyDown || Input.GetButton ("Submit") || Input.GetAxis ("LTrig") > 0.1) && !Input.GetMouseButton(0)){ //if any key is pressed that isnt a mouse button, delay is set in PlayerHealth
				//If any key is pressed and ticking isn't done, end it
				if(ScoreKeeper.DisplayScore != ScoreKeeper.Score){
					ScoreKeeper.DisplayCarsCompleted = ScoreKeeper.CarsCompleted;
					ScoreKeeper.DisplayEnemiesKilled = ScoreKeeper.EnemiesKilled;
					ScoreKeeper.DisplayScore = ScoreKeeper.Score;
				} 
				//Otherwise retry game as usual
				else {
					deathMenu.SetActive(false);
					Cursor.visible = false;
					CharControl.dead = false;
					Time.timeScale = 1;
					SceneManager.LoadScene (SceneManager.GetActiveScene().name);
				}
			}
		}
	}

    //Stop game and show pause menu on death
    void OnDeath() {
        dead = true;
        paused = true;
        cameraShake.stopAllShake();
        Cursor.visible = true;
        deathMenu.SetActive(true);
        bountyController.pauseBounties(); //show active bounties on death menu
    }

	public void SetVolume(float newVolume){
        Log("Setting Volume: " + newVolume);
        //Update the local volume and pref
        if (newVolume == 0) {
            pMute.GetComponent<Image>().overrideSprite = mutedSprite;
            dMute.GetComponent<Image>().overrideSprite = mutedSprite;
        } else {
            pMute.GetComponent<Image>().overrideSprite = unmutedSprite;
            dMute.GetComponent<Image>().overrideSprite = unmutedSprite;
        }
        AudioListener.volume = newVolume;
		PlayerPrefs.SetFloat ("volume", newVolume);
	}

	//Function called by the Mute button
	public void MuteButton(){
		//If volume is not zero, mute and update sliders
		if (PlayerPrefs.GetFloat ("volume") != 0) {
			pMute.GetComponent<Image>().overrideSprite = mutedSprite;
			dMute.GetComponent<Image>().overrideSprite = mutedSprite;
			unmuteVolume = PlayerPrefs.GetFloat ("volume");
			AudioListener.volume = 0;
			PlayerPrefs.SetFloat ("volume", 0);
		} 
		//Otherwise return to last saved volume and update sliders
		else {
			pMute.GetComponent<Image>().overrideSprite = unmutedSprite;
			dMute.GetComponent<Image>().overrideSprite = unmutedSprite;
			AudioListener.volume = unmuteVolume;
			PlayerPrefs.SetFloat ("volume", unmuteVolume);
		}
	}

    //Function called by the Resume button
    public void ResumeButton(){
        paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		Cursor.visible = false;
	}

    //Function called by the Main Menu button
    public void MainMenuButton(){
		SceneManager.LoadScene ("MainMenu");
	}

}
