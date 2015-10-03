using UnityEngine;
using System.Collections;

public class logo : MonoBehaviour {

	//How long the long the logo will show if nothing is pressed
	public float logoLifetime = 3f;

	// Use this for initialization
	void Start () {
		logoLifetime = 3f;
		Time.timeScale = 1; //ensures time scale isn't 0 from pausing
	}
	
	// Update is called once per frame
	void Update () {
		logoLifetime -= Time.deltaTime;
		//If lifetime expires or player presses any key, load menu
		if(logoLifetime <= 0 || Input.anyKeyDown || Input.GetButton ("Submit") || Input.GetAxis ("LTrig") > 0.1){
			Debug.Log("Now loading!");
			Application.LoadLevel ("MainMenu");
		}
	}
}
