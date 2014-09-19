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
	public int delay = 0;

	// Use this for initialization
	void Start () {

	}

	void brighten () {
		t = t + .01f;
		}

	void darken () {
		t = t - .01f;
		}

	void Update () {
		//Increment t
		if (countSwitch == 0)
						brighten ();
				else if (countSwitch == 1)
						darken ();

		if (t <= 0f && delay % 1000 == 0)
						countSwitch = 0; 

		if (t >= 1f && delay % 1000 == 0)
						countSwitch = 1;

		lerpedColor = Color.Lerp(dayday, nightynight, t);

		guiTexture.color = lerpedColor;
		delay = delay + 1;

		//Debug
		//print("Red : " + red + "Green : " + green + "Blue : " + blue);
	}
}
