using UnityEngine;
using System.Collections;

public enum JumpState
{
	GROUNDED,
	JUMPING,
	FALLING
}


public class CharControl : LogController {

	private Animator animator; //Store a ref to the animator so we can use it later
	private Rigidbody2D r;

	public bool controllable = true;

	//Movement variables
	public float maxSpeed = 2f;
	public float addSpeed = 25f;
	private JumpState Jump = JumpState.GROUNDED;

	//Ground stuff
	[HideInInspector] public Transform midGroundCheck;
	[HideInInspector] public Transform leftGroundCheck;
	[HideInInspector] public Transform rightGroundCheck;

	//Raycast points to make sure player doesn't get stuck on walls
	[HideInInspector] public Transform topWallCheck;
	[HideInInspector] public Transform midTopWallCheck;
	[HideInInspector] public Transform midWallCheck;
	[HideInInspector] public Transform midBotWallCheck;
	[HideInInspector] public Transform botWallCheck;
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
	int IDofNodes = 17;
	int IDofPlayer = 10;
	int IDofTucker = 18;
	int IDofRet = 19;
	int IDofColl = 9;

	void Start() {
		dead = false;
		r = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		midGroundCheck = GameObject.Find("character/raycasts/midGroundCheck").transform;
		leftGroundCheck = GameObject.Find("character/raycasts/leftGroundCheck").transform;
		rightGroundCheck = GameObject.Find("character/raycasts/rightGroundCheck").transform;
		topWallCheck = GameObject.Find("character/raycasts/topWallCheck").transform;
		midTopWallCheck = GameObject.Find("character/raycasts/midTopWallCheck").transform;
		midWallCheck = GameObject.Find("character/raycasts/midWallCheck").transform;
		midBotWallCheck = GameObject.Find("character/raycasts/midBotWallCheck").transform;
		botWallCheck = GameObject.Find("character/raycasts/botWallCheck").transform;
		Physics2D.IgnoreLayerCollision (IDofTrigs, IDofProjs, true); //make triggers and projectiles play nice but causes bullets to not go through one-ways
		Physics2D.IgnoreLayerCollision (IDofTrigs, IDofEnes, true); //make triggers and enemies play nice
		Physics2D.IgnoreLayerCollision (IDofNodes, IDofEnes, true); //make nodes and everything else play nice
		Physics2D.IgnoreLayerCollision (IDofNodes, IDofPlayer, true);
		Physics2D.IgnoreLayerCollision (IDofNodes, IDofProjs, true);
		Physics2D.IgnoreLayerCollision (IDofTucker, IDofPlayer, true); //ignore tucker collision
		Physics2D.IgnoreLayerCollision (IDofTucker, IDofProjs, true);
		Physics2D.IgnoreLayerCollision (IDofTucker, IDofNodes, true);
		Physics2D.IgnoreLayerCollision (IDofRet, IDofPlayer, true); //ignore collision on Reticle
		//Physics2D.IgnoreLayerCollision (IDofRet, IDofTucker, true);
		//Physics2D.IgnoreLayerCollision (IDofRet, IDofEnes, true);
		Physics2D.IgnoreLayerCollision (IDofRet, IDofProjs, true);
		Physics2D.IgnoreLayerCollision (IDofRet, IDofNodes, true);
		Physics2D.IgnoreLayerCollision (IDofTrigs, IDofNodes, true);
		Physics2D.IgnoreLayerCollision (IDofColl, IDofNodes, true);

	}

	void Update () {

		//Raycast in front of the player from three different heights to see if a wall is in front of the player
		leftWalled = (Physics2D.Raycast (midWallCheck.position, -Vector2.right, .75f, whatIsWall)
		              || Physics2D.Raycast (midTopWallCheck.position, -Vector2.right, .75f, whatIsWall) 
		              || Physics2D.Raycast (midBotWallCheck.position, -Vector2.right, .75f, whatIsWall)
		              || Physics2D.Raycast (topWallCheck.position, -Vector2.right, .75f, whatIsWall)
		              || Physics2D.Raycast (botWallCheck.position, -Vector2.right, .75f, whatIsWall));
		rightWalled = (Physics2D.Raycast (midWallCheck.position, Vector2.right, .75f, whatIsWall)
		               || Physics2D.Raycast (midTopWallCheck.position, Vector2.right, .75f, whatIsWall)
		               || Physics2D.Raycast (midBotWallCheck.position, Vector2.right, .75f, whatIsWall)
		               || Physics2D.Raycast (topWallCheck.position, Vector2.right, .75f, whatIsWall)
					   || Physics2D.Raycast (botWallCheck.position, Vector2.right, .75f, whatIsWall));

		//Debug.Log ("WALLED: " + leftWalled + rightWalled);

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
				r.AddForce(new Vector2(0, forceToAdd));
			}
			else {
				Jump = JumpState.FALLING;
				CurrJumpForce = 0;
			}
			break;

		case JumpState.FALLING: 
			if (isGrounded() && r.velocity.y <= 0) {
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
		//******OLD METHOD**********
		if(controllable){
			if(moveH > 0 && !rightWalled){
				animator.SetBool ("Run",true); //Begin run animation
				if(r.velocity.x <= maxSpeed)
					r.AddForce(new Vector2 (moveH * addSpeed, 0));
			}
			else if (moveH == 0) animator.SetBool ("Run",false); //End run animation
			else if(moveH < 0 && !leftWalled){
				animator.SetBool ("Run",true); //Begin run animation
				if(r.velocity.x > -maxSpeed)
					r.AddForce(new Vector2 (moveH * addSpeed, 0));
			}
		}
	}

	public void StartDeath(){ //turns on hit animation, and makes character drop through floor.
		animator.SetBool ("Hit", true);
		animator.SetBool ("Jump", false);
		gameObject.GetComponent<Collider2D>().enabled = false;
		Game.incDeaths();
		Gun.keyLoaded = false; //So that player does not shoot key on respawning\
		ScoreKeeper.DisplayScore = 0;
		Invoke ("setDead", 1f);
	}

	public void setDead(){
		dead = true;
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
