using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour {

	public float maxSpeed = 10f;
	
	//Ground stuff
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;

	void Start() {

	}

	void Update () {
		if(grounded && Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log ("space pressed");
			rigidbody2D.AddForce(new Vector2(0, jumpForce));

		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		float moveH = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);

	}
}
