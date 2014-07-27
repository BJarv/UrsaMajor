using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

	void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//Display buttons
		if (GUI.Button (new Rect (Screen.width * .44f, Screen.height * .5f, Screen.width * .2f, Screen.height * .1f), "Play Game")) {
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("firsttest");
		};

		if (GUI.Button (new Rect (Screen.width * .73f, Screen.height * .5f, Screen.width * .2f, Screen.height * .1f), "Options")) {
			Debug.Log ("Pressed Options!");	
		};
	}
}
