﻿using UnityEngine;
using System.Collections;

public class Jukebox : MonoBehaviour {

	//0 = Cowboy
	//1 = 8-Bit
	public int trackNo;
	public static string trackName;

	public AudioClip title;
	public AudioClip gameReg;
	public AudioClip gameFast;
	public AudioClip HYPE;
	public AudioClip death;

	public AudioClip diff;

	public AudioSource jukebox;

	public GameObject player;

	// Use this for initialization
	void Start () {
		jukebox = gameObject.GetComponent<AudioSource> ();
		trackNo = PlayerPrefs.GetInt ("track");

		if(trackNo == 0) trackName = "Cowboy";
		else if(trackNo == 1) trackName = "8-Bit";

		diff = jukebox.clip;
		player = GameObject.Find("character");
	}
	
	// Update is called once per frame
	void Update () {
		if (diff != jukebox.clip) {
			Debug.Log ("CHANGED");
			jukebox.Play();
			diff = jukebox.clip;
			if(trackNo == 0) trackName = "Cowboy";
			else if(trackNo == 1) trackName = "8-Bit";
		}
		//Change clips to the correct track
		//Cowboy
		if (trackNo == 0) {
			title = Resources.Load ("cowboy") as AudioClip;
			gameReg = Resources.Load ("cowboyFaster") as AudioClip;
			gameFast = Resources.Load ("cowboyFastest") as AudioClip;
			HYPE = Resources.Load ("cowboyHype") as AudioClip;
			death = Resources.Load ("cowboyDeath") as AudioClip;
		}
		//8-BIT
		if (trackNo == 1) {
			title = Resources.Load ("8BITloop") as AudioClip;
			gameReg = Resources.Load ("8BITfasterloop") as AudioClip;
			gameFast = Resources.Load ("8BITfastestloop") as AudioClip;
			HYPE = Resources.Load ("cowboyHype") as AudioClip;
			death = Resources.Load ("8BITDeath") as AudioClip;
		}

		//Audio state switcher
		if (PlayerHealth.playerHealth <= 0 || player.transform.position.y < -5f) {
			jukebox.clip = death;
		}
		else if (ScoreKeeper.HYPED) {
			jukebox.clip = HYPE;
		}
		else if (ScoreKeeper.carsCompleted >= 1) {
			jukebox.clip = gameFast;
		}
		else {
			jukebox.clip = gameReg;
		}
	}

	public void prevTrack(){
		if (trackNo == 0) PlayerPrefs.SetInt ("track", 1);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") - 1));
		trackNo = PlayerPrefs.GetInt ("track");
	}
	
	public void nextTrack(){
		if (trackNo == 1) PlayerPrefs.SetInt ("track", 0);
		else PlayerPrefs.SetInt ("track", (PlayerPrefs.GetInt ("track") + 1));
		trackNo = PlayerPrefs.GetInt ("track");
	}
}