using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;
	public Texture playButton;
	public GUISkin playSkin;

	void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//Display buttons
<<<<<<< HEAD
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .34f, Screen.width * .3f, Screen.height * .1f), "Play Game")) {
=======
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .335f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
>>>>>>> 7421647b6276be0afd95b2185c01cab148a3b228
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("entrances");
		};

<<<<<<< HEAD
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .48f, Screen.width * .3f, Screen.height * .1f), "Quit")) {
=======
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .48f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
>>>>>>> 7421647b6276be0afd95b2185c01cab148a3b228
			Debug.Log ("Pressed Quit!");	
			Application.Quit ();
		};
	}
}
