using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject volSlide;

	void Start () {
		volSlide = GameObject.Find ("Slider");
		volSlide.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("volume");
	}

	public void startGame(){
		Application.LoadLevel ("haydensBetaAF");
	}

	public void quitGame(){
		Application.Quit ();
	}

	public void setVolume(float newVol){
		AudioListener.volume = newVol;
		PlayerPrefs.SetFloat ("volume", newVol);
	}

	/*void OnGUI(){
		//Display Background Texture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//Display buttons
		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .335f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
			Debug.Log ("Pressed Play!");
			Application.LoadLevel("samsFuckinScene");
		}

		if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .48f, Screen.width * .3f, Screen.height * .12f), playButton, GUIStyle.none)) {
			Debug.Log ("Pressed Quit!");	
			Application.Quit ();
		}
	}*/
}
