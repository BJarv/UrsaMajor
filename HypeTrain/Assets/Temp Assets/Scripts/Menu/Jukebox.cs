using UnityEngine;
using System.Collections;

public class Jukebox : MonoBehaviour {
	
	public GameObject player;
	public AudioSource jukebox;

	//0 = Cowboy
	//1 = 8-Bit
	public int trackNo;
	public static string trackName;

	//Transition clips(static)
	public AudioClip toFaster;
	public AudioClip toHYPE;
	public AudioClip toDeath;

	public AudioClip[] Cowboy;
	public AudioClip[] EightBit;

	//Track clips (dynamic)
	public AudioClip title;
	public AudioClip gameReg;
	public AudioClip gameFast;
	public AudioClip HYPE;
	public AudioClip death;

	//Comparison clip to check for clip change
	public AudioClip diff;
	//Ignores transition when clip changed from UI player
	public bool menuSwap = false;
	
	// Use this for initialization
	void Start () {
		jukebox = gameObject.GetComponent<AudioSource> ();
		trackNo = PlayerPrefs.GetInt ("track");

		if(trackNo == 0) trackName = "Cowboy";
		else if(trackNo == 1) trackName = "8-Bit";

		diff = jukebox.clip;

		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//Transition sound handler
		if (diff != jukebox.clip) {
			if(jukebox.clip == death && !menuSwap){
				jukebox.PlayOneShot(toDeath, 1);
				jukebox.PlayDelayed(2.9f);
			} else if(jukebox.clip == HYPE && !menuSwap){
				jukebox.PlayOneShot(toHYPE, 1);
				jukebox.PlayDelayed(1.5f);
			} else if(jukebox.clip == gameFast && !menuSwap){
				jukebox.PlayOneShot(toFaster, 1);
				jukebox.PlayDelayed(.8f);
			} else { 
				jukebox.Play();
			}
			diff = jukebox.clip;
			menuSwap = false;
		}
		//Change clips to the correct track
		//Cowboy
		if (trackNo == 0) {
			trackName = "Cowboy";
			title = Cowboy[0];
			gameReg = Cowboy[1];
			gameFast = Cowboy[2];
			HYPE = Cowboy[3];
			death = Cowboy[4];
		}
		//8-BIT
		if (trackNo == 1) {
			trackName = "8-Bit";
			title = EightBit[0];
			gameReg = EightBit[1];
			gameFast = EightBit[2];
			HYPE = EightBit[3];
			death = EightBit[4];
		}

		//Audio state switcher
		if (player.GetComponent<playerCharacter>().currentHealth <= 0 || player.transform.position.y < -5f) {
			jukebox.clip = death;
		}
		else if (ScoreKeeper.HYPED) {
			jukebox.clip = HYPE;
		}
		else if (ScoreKeeper.CarsCompleted >= 1) {
			jukebox.clip = gameFast;
		}
		else {
			jukebox.clip = gameReg;
		}
	}

	//Function for the back arrow in the menu's jukebox
	public void prevTrack(){
		if (trackNo == 0) PlayerPrefs.SetInt ("track", 1);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") - 1));
		trackNo = PlayerPrefs.GetInt ("track");
		menuSwap = true;
	}

	//Function for the forward arrow in the menu's jukebox
	public void nextTrack(){
		if (trackNo == 1) PlayerPrefs.SetInt ("track", 0);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") + 1));
		trackNo = PlayerPrefs.GetInt ("track");
		menuSwap = true;
	}
}
