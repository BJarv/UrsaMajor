// AUTHORS
// Hayden Platt     (platt@ursamajorgames.com)

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//Logic to advance past the logo screen after X seconds or input detected.
public class Logo : LogController {

	[Tooltip("How long the long the logo will display for if nothing is pressed")]
	public float logoLifetime = 3f;
	
	// Update is called once per frame
	void Update () {
        logoLifetime -= Time.deltaTime;

		//If lifetime expires or player presses any key, load menu
		if(logoLifetime <= 0 || Input.anyKeyDown){
			Log("Now loading!");
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
