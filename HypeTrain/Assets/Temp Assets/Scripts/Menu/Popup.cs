using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popup : LogController
{

    public static bool paused = false;

    public GameObject baseMenu;
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

    private void Awake() {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void Start() {
        bountyController = GameObject.Find("BountyCanvas").GetComponent<BountyController>();
        player = GameObject.Find("Player");
    }

    void Update() {
        if (Input.GetButtonDown("Start") && !ShopKeeper.isOnScreen && !dead) {
            //Unpause game if paused
            if (paused) {
                ResumeGame();
            }
            //Pause game if not paused
            else if (!paused) {
                PauseGame();
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        //Stop game and show pause menu on death
        if (dead) {
            //if any key is pressed that isnt a mouse button, delay is set in PlayerHealth
            if ((Input.anyKeyDown || Input.GetButton("Submit") || Input.GetAxis("LTrig") > 0.1) && !Input.GetMouseButton(0)) { 
                //If any key is pressed and ticking isn't done, skip it
                if (ScoreKeeper.DisplayScore != ScoreKeeper.Score) {
                    ScoreKeeper.DisplayCarsCompleted = ScoreKeeper.CarsCompleted;
                    ScoreKeeper.DisplayEnemiesKilled = ScoreKeeper.EnemiesKilled;
                    ScoreKeeper.DisplayScore = ScoreKeeper.Score;
                }
                //Otherwise retry game as usual
                else {
                    paused = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    //Stop game and show pause menu on death
    void OnDeath() {
        PauseGame();
        dead = true;
        deathMenu.SetActive(true);
    }

    void PauseGame() {
        paused = true;
        Camera.main.transform.parent.transform.GetComponent<CameraShake>().StopAllShake();
        Cursor.visible = true;
        baseMenu.SetActive(true);
        bountyController.pauseBounties();
    }

    //Function called by the Resume button
    public void ResumeGame() {
        paused = false;
        Cursor.visible = false;
        baseMenu.SetActive(false);
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        bountyController.unpauseBounties(); //remove active bounties from pause menu	
    }

    //Function called by the Main Menu button
    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }

}
