using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popup : LogController {

	public static bool paused = false;
	CameraShake cameraShake;

    public GameObject generalMenu;
    public GameObject pauseMenu;
    public GameObject deathMenu;

    [HideInInspector] public GameObject player;
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
		player = GameObject.Find ("Player");

		Time.timeScale = 1;
		Cursor.visible = false;
	}

	void Update () {
		if(Input.GetButtonDown ("Start") && !ShopKeeper.isOnScreen){
			if(paused) { //unpause game if paused
				paused = false;
				Time.timeScale = 1;
                generalMenu.SetActive(false);
                pauseMenu.SetActive(false);

				Cursor.visible = false;
				bountyController.unpauseBounties(); //remove active bounties from pause menu	

			} else if (!paused) { //pause game if not pause
				cameraShake.stopAllShake();
				paused = true;
				Time.timeScale = 0;
                generalMenu.SetActive(true);
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
                    generalMenu.SetActive(false);
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
        generalMenu.SetActive(true);
        deathMenu.SetActive(true);
        bountyController.pauseBounties(); //show active bounties on death menu
    }

    //Function called by the Resume button
    public void ResumeButton(){
        paused = false;
		Time.timeScale = 1;
        generalMenu.SetActive(false);
        pauseMenu.SetActive(false);
		Cursor.visible = false;
	}

    //Function called by the Main Menu button
    public void MainMenuButton(){
		SceneManager.LoadScene ("MainMenu");
	}

}
