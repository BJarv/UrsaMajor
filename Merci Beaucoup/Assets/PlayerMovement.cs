using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	float speed = 10f;
	public Text scoreText;
	public float score = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag.Equals ("collectible")){
			Destroy (other.gameObject);
			score += 1;
			scoreText.text = "Score: " + score;
		}
	}
}
