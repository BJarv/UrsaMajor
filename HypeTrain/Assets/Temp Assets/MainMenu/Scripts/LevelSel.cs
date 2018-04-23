using UnityEngine;
using System.Collections;

public class LevelSel : LogController {
	
	public Texture backgroundTexture;
	
	void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);
		
		//Display buttons
		if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .1f, Screen.width * .2f, Screen.height * .1f), "firsttest")) {
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("firsttest");
		};
		if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .3f, Screen.width * .2f, Screen.height * .1f), "guntest")) {
			Debug.Log ("Pressed Options!");	
			Application.LoadLevel("guntest");
		};
		if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .5f, Screen.width * .2f, Screen.height * .1f), "AITest")) {
			Debug.Log ("Pressed Options!");	
			Application.LoadLevel("AITest");
		};
		if (GUI.Button (new Rect (Screen.width * .4f, Screen.height * .7f, Screen.width * .2f, Screen.height * .1f), "Anim")) {
			Debug.Log ("Pressed Options!");	
			Application.LoadLevel("Anim");
		};
		if (GUI.Button (new Rect (Screen.width * .7f, Screen.height * .1f, Screen.width * .2f, Screen.height * .1f), "trainGen")) {
			Debug.Log ("Pressed Options!");	
			Application.LoadLevel("trainGen");
		};
	}
}

