using UnityEngine;
using System.Collections;

public class gun : MonoBehaviour {


	public Rigidbody2D bullet;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetButtonDown ("Fire1")) {
			var pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);

			var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
			Rigidbody2D go = Instantiate(bullet, transform.position, q) as Rigidbody2D;
			go.rigidbody2D.AddForce(go.transform.up * 500);
		}

	}
}
