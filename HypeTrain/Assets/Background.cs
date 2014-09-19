using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	/*public float red = 0.5f;
	public float green = 0.5f;
	public float blue = 0;*/
	public float t = 0;
	public Color nightynight = new Color(.01f, .01f, .5f,1);
	public Color dayday = new Color(1f, 1f, 1f, 0);
	public Color lerpedColor = new Color(.5f, .5f, 0, 1);
	public int countSwitch = 0; // 0 = let's count up, 1 = let's count down

	// Use this for initialization
	void Start () {

	}

	void Update () {
		//Increment t
		if (countSwitch == 0)
						t = t + .01f;
		else if (countSwitch == 1)
						t = t - .01f;

		if (t <= 0f)
						countSwitch = 0; 

		if (t >= 1f)
						countSwitch = 1;

		/*Increment red
		red = red + 0.001f;
		if (red + .001 >= 1)
						red = 0;

		//Increment green
		green = green + 0.001f;
		if (green + .001 >= 1)
						green = 0;

		//Increment blue
		blue = blue + 0.001f;
		if (blue + .001 >= 1)
						blue = 0;*/

		//Throw red, green, blue into a new color
		lerpedColor = Color.Lerp(dayday, nightynight, t);

		//Set the GUI to that color
		guiTexture.color = lerpedColor;

		//Debug
		//print("Red : " + red + "Green : " + green + "Blue : " + blue);
	}
}
