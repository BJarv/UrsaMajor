using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour {

	public float maxSpeed = 2f;
	
	//Ground stuff
	bool grounded = false;
	bool hasJumped = false;
	public Transform groundCheck;
	float raycastLength = 0.01f;
	public LayerMask whatIsGround;
	public float jumpForce = 300f;

	void Start() {

	}

	void Update () {
		if(grounded && Input.GetKey(KeyCode.Space) && !hasJumped) {
			Debug.Log ("space pressed");
			if(Mathf.Abs (rigidbody2D.velocity.y) <= .5){
				rigidbody2D.AddForce(new Vector2(0, jumpForce));
				hasJumped = true;
			}
		}
		if (!Input.GetKey(KeyCode.Space) && hasJumped) {
			Debug.Log ("Reset jump");
			hasJumped= false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		grounded = Physics2D.Raycast (groundCheck.position, -Vector2.up, raycastLength, whatIsGround);

		float moveH = Input.GetAxis ("Horizontal");
		rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);

	}
}
