using UnityEngine;
using System.Collections;

public class deleteIfOffScreen : MonoBehaviour {

	//We will make this a reference to the camera
	private GameObject mainCamera = null;

	// Use this for initialization
	void Start () {
	
		mainCamera = GameObject.Find("Main Camera");

	}
	
	// Update is called once per frame
	void Update () {
		if (mainCamera.transform.position.x - 100 > this.gameObject.transform.position.x) {
			Destroy (this.gameObject);
		}
	}

	void fixedUpdate() {
				
		}
}
