using UnityEngine;
using System.Collections;

public enum JumpState
{
	GROUNDED,
	JUMPING,
	FALLING
}


public class CharControl : MonoBehaviour {

	private Animator animator; //Store a ref to the animator so we can use it later
	public float maxSpeed = 2f;
	public float addSpeed = 25f;
	private JumpState Jump = JumpState.GROUNDED;
	//Ground stuff
	public Transform midGroundCheck;
	public Transform leftGroundCheck;
	public Transform rightGroundCheck;
	float raycastLength = 0.3f;
	public LayerMask whatIsGround;
	//Jumpforce variables
	public float PlusJumpForce = 300f;
	public float CurrJumpForce = 0f;
	public float MaxJumpForce = 1100f;

	public int horizDirection = 1;


	void Awake() {
		animator = GetComponent<Animator>();
		}
	void Start() {

	}

	void Update () {
		switch (Jump) {

		case JumpState.GROUNDED: 
			if(Input.GetKey(KeyCode.Space) && isGrounded()) {
				Jump = JumpState.JUMPING;
				animator.SetBool ("Jump",true); //Switch to jump animation
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
				//Debug.Log("Grounded"); Use this to debug jump issues
				Jump = JumpState.GROUNDED;
				animator.SetBool ("Jump", false); //End jump animation
			}
			break;
			
		}
	}

	public bool isGrounded()
	{
		return Physics2D.Raycast (midGroundCheck.position, -Vector2.up, raycastLength, whatIsGround) || 
			Physics2D.Raycast (leftGroundCheck.position, -Vector2.up, raycastLength, whatIsGround) ||
			Physics2D.Raycast (rightGroundCheck.position, -Vector2.up, raycastLength, whatIsGround);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Horizontal Movement
		float moveH = Input.GetAxis ("Horizontal");
		//Debug.Log (moveH);
		Flip (moveH);
	
		if(moveH > 0)
		{
			animator.SetBool ("Run",true); //Begin run animation
			if(rigidbody2D.velocity.x <= maxSpeed)
				rigidbody2D.AddForce(new Vector2 (moveH * addSpeed, 0));
		}
		else if (moveH == 0) animator.SetBool ("Run",false); //End run animation
		else
		{
			animator.SetBool ("Run",true); //Begin run animation
			if(rigidbody2D.velocity.x > -maxSpeed)
				rigidbody2D.AddForce(new Vector2 (moveH * addSpeed, 0));
		}
	}

	void Flip(float moveH)
	{
		if (moveH > 0)
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		else if (moveH < 0)
			transform.localEulerAngles = new Vector3 (0, 180, 0);
	}
}
