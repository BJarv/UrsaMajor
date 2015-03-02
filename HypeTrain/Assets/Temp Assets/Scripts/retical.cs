using UnityEngine;
using System.Collections;

public class retical : MonoBehaviour {

	public int reticalSize = 32;
	private int retX;
	private int retY;
	private int retOffset;
	public static Vector3 recPos;
	public Vector3 joystickPosition;
	[HideInInspector] public GameObject cameraObj;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find ("Main Camera");
		retX = reticalSize;
		retY = reticalSize;
		retOffset = reticalSize/2;
		Screen.showCursor = false;
	}

	// Update is called once per frame
	void Update () {
		joystickPosition = new Vector3(Input.GetAxis ("Mouse X") * 1000, Input.GetAxis ("Mouse Y") * 1000, 0); //Use for controller aim
		recPos = Input.mousePosition;
		recPos.z = 100;
		transform.position = cameraObj.GetComponent<Camera2D> ().camera.ScreenToWorldPoint(recPos);
	}
}
