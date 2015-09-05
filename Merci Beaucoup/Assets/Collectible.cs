using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	float speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.down * speed * Time.deltaTime);

		if (transform.position.y <= -3.5) {
			Destroy (gameObject);
		}
	
	}
}
