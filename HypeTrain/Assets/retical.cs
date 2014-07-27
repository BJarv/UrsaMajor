using UnityEngine;
using System.Collections;

public class retical : MonoBehaviour {

	public int reticalSize;
	public Texture2D reticalSprite;
	private int retX;
	private int retY;
	private int retOffset;

	// Use this for initialization
	void Start () {
		reticalSize = 32;
		retX = reticalSize;
		retY = reticalSize;
		retOffset = reticalSize/2;
		Screen.showCursor = false;
	}

	void OnGUI() {
		GUI.DrawTexture(new Rect(Input.mousePosition.x - retOffset, Screen.height - Input.mousePosition.y - retOffset, retX, retY), reticalSprite);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
