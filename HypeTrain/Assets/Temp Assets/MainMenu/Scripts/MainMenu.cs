using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

	void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//Display buttons
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .34f, Screen.width * .3f, Screen.height * .1f), "Play Game")) {
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("Alpha");
		};

		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .48f, Screen.width * .3f, Screen.height * .1f), "Quit")) {
			Debug.Log ("Pressed Quit!");	
			Application.Quit ();
		};
	}
}
