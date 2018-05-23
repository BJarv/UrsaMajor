using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class Jukebox : LogController {
	
	private AudioSource jukebox;

    //Track info variables
	private int trackNo;
	public static string trackName;

	//Transition clips(static)
	public AudioClip toFaster;
	public AudioClip toHYPE;
	public AudioClip toDeath;

    //Track-specific clips (dynamic)
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

    [Tooltip("Array of song titles paired with their respective tracks.")]
    public Song[] songs;

    //Volume Control variables
    public Sprite unmutedSprite;
    public Sprite mutedSprite;
    public GameObject muteButton;
    public GameObject volumeSlider;
    private float unmuteVolume;

    private void OnEnable()
    {
        EventManager.StartListening("PlayerDeath", OnDeath);
        EventManager.StartListening("StartHYPE", OnHYPE);
        EventManager.StartListening("CarGroupComplete", OnCarGroupComplete);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerDeath", OnDeath);
        EventManager.StopListening("StartHYPE", OnHYPE);
        EventManager.StopListening("CarGroupComplete", OnCarGroupComplete);
    }

    //Initialize references
    void Awake() {
        jukebox = GetComponent<AudioSource>();
        trackNo = PlayerPrefs.GetInt("track");
        SetTrack(songs[trackNo]);
        jukebox.clip = gameFaster;
        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume");
        unmuteVolume = PlayerPrefs.GetFloat("volume");
    }

    // Use this for initialization
    void Start () {
        jukebox.Play();
    }
	
	// Update is called once per frame
	void Update () { }

    //Handle speed up transition
    void OnCarGroupComplete() {
        jukebox.clip = gameFastest;
        jukebox.PlayOneShot(toFaster, 1);
        jukebox.PlayDelayed(toFaster.length - .2f);
    }

    //Handle HYPE activation transition
    void OnHYPE() {
        jukebox.clip = HYPE;
        jukebox.PlayOneShot(toHYPE, 1);
        jukebox.PlayDelayed(toHYPE.length - .2f);
    }

    //Handle death transition
    void OnDeath() {
        jukebox.clip = death;
        jukebox.PlayOneShot(toDeath, 1);
        jukebox.PlayDelayed(toDeath.length - .2f);
    }

    //Set each track-specific clip based on the current song
    void SetTrack(Song s) {
        trackName = s.songName;
        gameReg = s.songClips[0];
        gameFaster = s.songClips[1];
        gameFastest = s.songClips[2];
        HYPE = s.songClips[3];
        death = s.songClips[4];
    }

	// [<<] Function for the back arrow in the menu's jukebox
	public void PrevTrack(){
		if (trackNo == 0) PlayerPrefs.SetInt ("track", songs.Length - 1);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") - 1));
		trackNo = PlayerPrefs.GetInt ("track");
        SetTrack(songs[trackNo]);
	}

	// [>>] Function for the forward arrow in the menu's jukebox
	public void NextTrack(){
		if (trackNo == songs.Length - 1) PlayerPrefs.SetInt ("track", 0);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") + 1));
		trackNo = PlayerPrefs.GetInt ("track");
        SetTrack(songs[trackNo]);
	}

    //Function called by the Volume Slider
    public void SetVolume(float newVolume) {
        Log("Setting Volume: " + newVolume);
        //Update the local volume and pref
        if (newVolume == 0) {
            muteButton.GetComponent<Image>().overrideSprite = mutedSprite;
        }
        else {
            muteButton.GetComponent<Image>().overrideSprite = unmutedSprite;
        }
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("volume", newVolume);
    }

    //Function called by the Mute button
    public void MuteButton() {
        //If volume is not zero, mute and update sliders
        if (PlayerPrefs.GetFloat("volume") != 0) {
            muteButton.GetComponent<Image>().overrideSprite = mutedSprite;
            unmuteVolume = PlayerPrefs.GetFloat("volume");
            AudioListener.volume = 0;
            PlayerPrefs.SetFloat("volume", 0);
        }
        //Otherwise return to last saved volume and update sliders
        else {
            muteButton.GetComponent<Image>().overrideSprite = unmutedSprite;
            AudioListener.volume = unmuteVolume;
            PlayerPrefs.SetFloat("volume", unmuteVolume);
        }
    }
}
