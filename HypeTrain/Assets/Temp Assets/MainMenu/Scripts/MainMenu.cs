using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture playButton;
	public GUISkin playSkin;

	public static bool tutorial;

	void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//Display buttons
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .335f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("haydenZone");
		};

		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .21f, Screen.width * .3f, Screen.height * .12f), "Start w/ Tutorial")) {
			Debug.Log ("Start with Tutorial");
			tutorial = true;
			Application.LoadLevel("haydenZone");
		};

		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .48f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
			Debug.Log ("Pressed Quit!");	
			Application.Quit ();
		};
	}
}
