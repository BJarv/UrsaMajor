using UnityEngine;
using System.Collections.Generic;

public class TrainExit : MonoBehaviour {
	
	private GameObject Player = null;
	private Vector2 velocity;
	public GameObject cameraObj;
	public GameObject sidePanel;

	private bool soundPlayed;
	public AudioClip exitSound;

	// Use this for initialization
	void Start () {
		cameraObj = GameObject.Find("Main Camera");
		Player = GameObject.Find("character");
		sidePanel = GameObject.Find ("sidepanel");
		velocity = new Vector2 (0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void exitZoom() {
		//while (Camera.main.orthographicSize < 34) {
		Camera.main.orthographicSize = Mathf.SmoothDamp (12.79f, 35, ref velocity.y, 0.5f);
		//}
		Camera.main.orthographicSize = Mathf.SmoothDamp (35, 13, ref velocity.y, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D hit) {
		if(Input.GetKey(KeyCode.E)){
			//Play exit sound
			if(!soundPlayed){
				AudioSource.PlayClipAtPoint(exitSound, transform.position);
				soundPlayed = true;
			}
			//Temporary Exit Solution
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			Player.rigidbody2D.AddForce(new Vector2(0, 300));
			//Hard zoom out, then unlock camera
			cameraObj.GetComponent<Camera2D>().setLock(false);
			exitZoom();
			//Make sidePanel visible again
			sidePanel.SetActive(true);
		}
	}
	void OnTriggerStay2D(Collider2D hit) {
		if(Input.GetKey(KeyCode.E)){
			if(!soundPlayed){
				AudioSource.PlayClipAtPoint(exitSound, transform.position);
				soundPlayed = true;
			}
			//Temporary Exit Solution
			Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, true);
			Player.rigidbody2D.AddForce(new Vector2(0, 300));
			//Hard zoom out, then unlock camera
			cameraObj.GetComponent<Camera2D>().setLock(false);
			exitZoom();
			//Make sidePanel visible again
			sidePanel.SetActive(true);
		}
	}
	void OnTriggerExit2D(Collider2D hit) 
	{
		Physics2D.IgnoreCollision (hit, transform.parent.gameObject.collider2D, false);
	}
}
