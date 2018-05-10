using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Jukebox : LogController {
	
	private GameObject player;
	private AudioSource jukebox;

	public int trackNo;
	public static string trackName;

	//Transition clips(static)
	public AudioClip toFaster;
	public AudioClip toHYPE;
	public AudioClip toDeath;

    //Track clips (dynamic)
    private AudioClip gameReg;
    private AudioClip gameFaster;
    private AudioClip gameFastest;
    private AudioClip HYPE;
    private AudioClip death;

    [System.Serializable]
    public struct Song {
        public string songName;
        public AudioClip[] songClips;
    }

    public Song[] songs;

    //Ignores transition when clip changed from UI player
    private bool menuSwap = false;

    private void OnEnable()
    {
        EventManager.StartListening("PlayerDeath", OnDeath);
        EventManager.StartListening("StartHYPE", OnHYPE);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerDeath", OnDeath);
        EventManager.StopListening("StartHYPE", OnHYPE);
    }

    //Initialize references
    private void Awake()
    {
        jukebox = GetComponent<AudioSource>();
        trackNo = PlayerPrefs.GetInt("track");
        jukebox.clip = gameFaster;
        SetTrack(songs[trackNo]);
    }

    // Use this for initialization
    void Start () { }
	
	// Update is called once per frame
	void Update () {
		if (ScoreKeeper.CarsCompleted >= 1) {
			jukebox.clip = gameFastest;
            jukebox.PlayOneShot(toFaster, 1);
            jukebox.PlayDelayed(toFaster.length - .2f);
        }
		else {
			jukebox.clip = gameFaster;
		}
	}

    void OnHYPE() {
        jukebox.clip = HYPE;
        jukebox.PlayOneShot(toHYPE, 1);
        jukebox.PlayDelayed(toHYPE.length - .2f);
    }

    void OnDeath() {
        jukebox.clip = death;
        jukebox.PlayOneShot(toDeath, 1);
        jukebox.PlayDelayed(toDeath.length - .2f);
    }

    void SetTrack(Song s) {
        trackName = s.songName;
        gameReg = s.songClips[0];
        gameFaster = s.songClips[1];
        gameFastest = s.songClips[2];
        HYPE = s.songClips[3];
        death = s.songClips[4];
    }

	//Function for the back arrow in the menu's jukebox
	public void PrevTrack(){
		if (trackNo == 0) PlayerPrefs.SetInt ("track", songs.Length - 1);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") - 1));
		trackNo = PlayerPrefs.GetInt ("track");
        SetTrack(songs[trackNo]);
		menuSwap = true;
	}

	//Function for the forward arrow in the menu's jukebox
	public void NextTrack(){
		if (trackNo == songs.Length - 1) PlayerPrefs.SetInt ("track", 0);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") + 1));
		trackNo = PlayerPrefs.GetInt ("track");
        SetTrack(songs[trackNo]);
        menuSwap = true;
	}
}
