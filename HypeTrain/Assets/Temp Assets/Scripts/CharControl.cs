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

	//Movement variables
	public float maxSpeed = 2f;
	public float addSpeed = 25f;
	private JumpState Jump = JumpState.GROUNDED;

	//Ground stuff
	[HideInInspector] public Transform midGroundCheck;
	[HideInInspector] public Transform leftGroundCheck;
	[HideInInspector] public Transform rightGroundCheck;
	[HideInInspector] public Transform wallCheck;
	float raycastLength = 0.15f;
	public LayerMask whatIsGround;

	//Wall collision variables
	private bool leftWalled;
	private bool rightWalled;
	public LayerMask whatIsWall;

	//Jumpforce variables
	public float PlusJumpForce = 300f;
	public float CurrJumpForce = 0f;
	public float MaxJumpForce = 1100f;
	
	public int horizDirection = 1;

	public static bool dead;

	int IDofTrigs = 8;
	int IDofProjs = 13;
	int IDofEnes = 11;

	void Awake() {
		//animator = GetComponent<Animator>();
	}

	void Start() {
		//SaveLoad.Load (); //LOADS SAVE GAME
		//SaveLoad.Load (); //LOADS SAVE GAME
		//switch(Game.skin) {
		//case 0:
		//  //default char skin, do nothing
		//	break;
		//case 1:
		//  GetComponent<SpriteRenderer>().sprite = skin2; //or whatever itll be called
		// 	break;
		//}
		animator = GetComponent<Animator>();
		midGroundCheck = GameObject.Find("character/midGroundCheck").transform;
		leftGroundCheck = GameObject.Find("character/leftGroundCheck").transform;
		rightGroundCheck = GameObject.Find("character/rightGroundCheck").transform;
		wallCheck = GameObject.Find("character/wallCheck").transform;
		Physics2D.IgnoreLayerCollision (IDofTrigs, IDofProjs, true); //make triggers and projectiles play nice but causes bullets to not go through one-ways
		Physics2D.IgnoreLayerCollision (IDofTrigs, IDofEnes, true); //make triggers and enemies play nice
	}

	void Update () {

		//Raycast to see if a wall is in front of the player
		leftWalled = Physics2D.Raycast (wallCheck.position, -Vector2.right, .75f, whatIsWall);
		rightWalled = Physics2D.Raycast (wallCheck.position, Vector2.right, .75f, whatIsWall);

		switch (Jump) {

		case JumpState.GROUNDED: 
			if((Input.GetKey(KeyCode.Space) || Input.GetAxis ("LTrig") > 0.1) && isGrounded()) {
				Jump = JumpState.JUMPING;
				animator.SetBool ("Jump", true); //Switch to jump animation
				animator.SetBool ("Hit", false);
			}
			break;

		case JumpState.JUMPING: 
			if((Input.GetKey(KeyCode.Space) || Input.GetAxis ("LTrig") > 0.1) && CurrJumpForce < MaxJumpForce) {
				var timeDiff = Time.deltaTime * 100;
				var forceToAdd = PlusJumpForce*timeDiff;
				CurrJumpForce += forceToAdd;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceToAdd));
			}
			else {
				Jump = JumpState.FALLING;
				CurrJumpForce = 0;
			}
			break;

		case JumpState.FALLING: 
			if (isGrounded() && GetComponent<Rigidbody2D>().velocity.y <= 0) {
				//Debug.Log("Grounded"); Use this to debug jump issues
				Jump = JumpState.GROUNDED;
				animator.SetBool ("Jump", false); //End jump animation
			}
			break;
			
		}
	}

	public void hitAnim () {
		animator.SetBool ("Hit",true); //LOOK HERE HAYDEN Switch character to hit animation
		animator.SetBool ("Jump", false);
		Invoke ("hitToIdle", .25f);
	}

	void hitToIdle (){
		animator.SetBool ("Hit", false);
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
	
		if(moveH > 0 && !rightWalled) //Add && !rightWalled
		{
			animator.SetBool ("Run",true); //Begin run animation
			if(GetComponent<Rigidbody2D>().velocity.x <= maxSpeed)
				GetComponent<Rigidbody2D>().AddForce(new Vector2 (moveH * addSpeed, 0));
		}
		else if (moveH == 0) animator.SetBool ("Run",false); //End run animation
		else if(moveH < 0 && !leftWalled)  //Add if(!leftWalled)
		{
			animator.SetBool ("Run",true); //Begin run animation
			if(GetComponent<Rigidbody2D>().velocity.x > -maxSpeed)
				GetComponent<Rigidbody2D>().AddForce(new Vector2 (moveH * addSpeed, 0));
		}
	}

	public void StartDeath() //turns on hit animation, and makes character drop through floor.
	{
		animator.SetBool ("Hit", true);
		animator.SetBool ("Jump", false);
		gameObject.GetComponent<Collider2D>().enabled = false;
		dead = true;
		Destroy (gameObject, 3f);
	}

	void Flip(float moveH)
	{
		if (moveH > 0)
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		else if (moveH < 0)
			transform.localEulerAngles = new Vector3 (0, 180, 0);
	}

	public void skinChange(Sprite skin){
		GetComponent<SpriteRenderer>().sprite = skin;
	}
}
