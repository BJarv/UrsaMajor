using UnityEngine;
using System.Collections;

public enum JumpState
{
	GROUNDED,
	JUMPING,
	FALLING
}


public class CharControl : MonoBehaviour {

	public float maxSpeed = 2f;
	private JumpState Jump = JumpState.GROUNDED;
	//Ground stuff
	public Transform groundCheck;
	float raycastLength = 0.3f;
	public LayerMask whatIsGround;
	//Jumpforce variables
	public float PlusJumpForce = 300f;
	public float CurrJumpForce = 0f;
	public float MaxJumpForce = 1100f;

	public int horizDirection = 1;

	void Start() {

	}

	void Update () {
		switch (Jump) {

		case JumpState.GROUNDED: 
			if(Input.GetKey(KeyCode.Space) && isGrounded()) {
				Jump = JumpState.JUMPING;
			}
			break;

		case JumpState.JUMPING: 
			if(Input.GetKey(KeyCode.Space) && CurrJumpForce < MaxJumpForce) {
				var timeDiff = Time.deltaTime * 100;
				var forceToAdd = PlusJumpForce*timeDiff;
				CurrJumpForce += forceToAdd;
				rigidbody2D.AddForce(new Vector2(0, forceToAdd));
			}
			else {
				Jump = JumpState.FALLING;
				CurrJumpForce = 0;
			}
			break;

		case JumpState.FALLING: 
			if (isGrounded() && rigidbody2D.velocity.y <= 0) {
				Debug.Log("Grounded");
				Jump = JumpState.GROUNDED;
			}
			break;
			
		}
	}

	public bool isGrounded()
	{
		return Physics2D.Raycast (groundCheck.position, -Vector2.up, raycastLength, whatIsGround);
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		//Horizontal Movement
		float moveH = Input.GetAxis ("Horizontal");
		//Debug.Log (moveH);
		Flip (moveH);
		rigidbody2D.velocity = new Vector2 (moveH * maxSpeed, rigidbody2D.velocity.y);

	}

	void Flip(float moveH)
	{
		if (moveH > 0)
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		else if (moveH < 0)
			transform.localEulerAngles = new Vector3 (0, 180, 0);
	}
}
