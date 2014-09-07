using UnityEngine;
using System.Collections;

public class retical : MonoBehaviour {

	public int reticalSize = 32;
	public Texture2D reticalSprite;
	private int retX;
	private int retY;
	private int retOffset;
	public Vector3 currPlace;

	// Use this for initialization
	void Start () {
		retX = reticalSize;
		retY = reticalSize;
		retOffset = reticalSize/2;
		Screen.showCursor = false;
	}

	void OnGUI() {
		//GUI.DrawTexture(new Rect(Input.mousePosition.x - retOffset, Screen.height - Input.mousePosition.y - retOffset, retX, retY), reticalSprite);
	}

	// Update is called once per frame
	void Update () {
		transform.position = Input.mousePosition;
		currPlace = transform.position;
	}
}
