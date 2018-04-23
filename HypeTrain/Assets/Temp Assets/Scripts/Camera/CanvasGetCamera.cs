using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasGetCamera : LogController {

	// Use this for initialization
	void Start () {
		GetComponent<Canvas>().worldCamera = GameObject.Find ("CameraHolder/CameraBumber/Main Camera").GetComponent<Camera>();
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
