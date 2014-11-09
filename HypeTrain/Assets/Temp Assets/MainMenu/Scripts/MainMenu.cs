using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture playButton;
	public GUISkin playSkin;

	public static bool tutorial = true;

	void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//Display buttons
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .335f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("entrances");
		};

		if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .1f, Screen.width * .2f, Screen.height * .1f), "Start w/ Tutorial")) {
			Debug.Log ("Start with Tutorial");
			tutorial = true;
		};

		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .48f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
			Debug.Log ("Pressed Quit!");	
			Application.Quit ();
		};
	}
}
