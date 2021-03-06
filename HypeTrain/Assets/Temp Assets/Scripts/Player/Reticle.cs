﻿using UnityEngine;
using System.Collections;

public class Reticle : LogController {

	public int reticalSize = 32;
	public static Vector3 recPos;
	public Vector3 joystickPosition;
	[HideInInspector] public GameObject cameraObj;

	GameObject tucker;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find ("Main Camera");
		Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {
		joystickPosition = new Vector3(Input.GetAxis ("Mouse X") * 10000, Input.GetAxis ("Mouse Y") * 1000, 0); //Use for controller aim
		//if(Input.GetAxis ("LTrig") > 0.1) //COME BACK HERE FOR CONTROLLER AIM
		recPos = Input.mousePosition;
		recPos.z = 100;
		transform.position = cameraObj.GetComponent<Camera2D> ().GetComponent<Camera>().ScreenToWorldPoint(recPos);
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("Colliding with something.");
		if (other.gameObject.tag.Equals ("enemy")) {
			if (tucker = GameObject.Find ("Tucker")) {
				tucker.GetComponent<TuckerController> ().changeTarget(other.transform.gameObject);
		}
	}
}

}