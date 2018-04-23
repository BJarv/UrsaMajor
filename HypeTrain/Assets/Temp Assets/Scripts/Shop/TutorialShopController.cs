using UnityEngine;
using System.Collections;

public class TutorialShopController : LogController {

	public static bool tutorial = false;
	public static bool shop = false;

	// Use this for initialization
	void Start () {
		if (PlayerPrefsBool.GetBool ("firstTime")) {
			PlayerPrefsBool.SetBool ("firstTime", false);
			TutorialShopController.tutorial = true;
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
