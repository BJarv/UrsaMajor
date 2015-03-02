using UnityEngine;
using System.Collections;

public class TutShopController : MonoBehaviour {

	public static bool tutorial = false;
	public static bool shop = false;

	// Use this for initialization
	void Start () {
		if (Game.firstTime == true) {
			Game.firstTime = false;
		if (PlayerPrefsBool.GetBool ("firstTime")) {
			PlayerPrefsBool.SetBool ("firstTime", false);
			TutShopController.tutorial = true;
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
