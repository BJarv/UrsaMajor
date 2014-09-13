using UnityEngine;
using System.Collections;

public enum EnemyState //enemy states dictate what mode enemies are in
{
	IDLE,
	ATTACK,
	CHASE,
	DASH,
	JUMP
}

public class Enemy : MonoBehaviour {

	public float health = 20f;

	public EnemyState State = EnemyState.IDLE; //basic state
	public float EnemySpeed = 2f;
	public GameObject Player = null;
	public float AttackDist = 10f;  //distance at which enemy will switch to attacking
	public int direction = 1;      //direction enemy is facing, 1 for right, -1 for left
	public float StrollDist = 3f;  //distance enemy walks back and forth during idle
	public float distToPlayer;	
	public Vector3 posOfTrans1; 
	public Vector3 posOfTrans2;
	
	private Vector2 StrollStart = new Vector2(0, 0);
	
	private bool strolling = false;

	private float startTime;

	//raycast info
	public float dashCast = 20f; //wall distance
	public float jumpCast = 5f; //jump distance
	public LayerMask dashMask;  
	public LayerMask jumpMask; 
	public Vector2 dashVec; //force vector applied during dash
	public Vector2 jumpVec; //force vector applied during jump
	public float dashCD = 2f; //time between dashes
	public bool dashRdy = true;
	public float jumpCD = 2f; //time between dashes
	public bool jumpRdy = true;
	public float groundCast = 1f;
	public LayerMask enemyGroundMask;

	// Use this for initialization
	virtual protected void Start () {
		Player = GameObject.Find("character");
	}
	
	// Update is called once per frame
	virtual protected void Update () {
	
		posOfTrans1 = transform.position;
		posOfTrans1 = Player.transform.position;
		distToPlayer = Vector2.Distance (transform.position, Player.transform.position);
		//Debug.Log (distToPlayer);
		if (distToPlayer < AttackDist) 
		{
			State = EnemyState.ATTACK;
		} 
		if (State == EnemyState.ATTACK && isJump ()) //enemy is about to jump!
		{
			jumpRdy = false;           //turn off jumping
			Invoke ("jumpOn", jumpCD); //turn it back on again after a cooldown
			State = EnemyState.JUMP;
		}
		if (isDash ()) //enemy is about to dash!
		{
			dashRdy = false;           //turn off dashing
			Invoke ("dashOn", dashCD); //turn it back on again after a cooldown
			State = EnemyState.DASH; 
		}
		Act();
	}

	void FixedUpdate () {
		Flip (transform.rigidbody2D.velocity.x); //Uses velocity to determine when to flip
	}
	//Code to flip sprite
	void Flip(float moveH){
		if (moveH > 0) {
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		} else if (moveH < 0) {
			transform.localEulerAngles = new Vector3 (0, 180, 0);
		}
	}

	public void Act()
	{
		switch(State)
		{
			case EnemyState.ATTACK: Attack(); break;
			case EnemyState.IDLE: Idle(); break;
			case EnemyState.CHASE: Chase(); break;
			case EnemyState.DASH: Dash (); break;
			case EnemyState.JUMP: Jump (); break;
			default: Idle();break;
			
		}
	}

	virtual protected void Attack()
	{
		if (isJump ()) return;
		if (transform.position.x < Player.transform.position.x) 
		{
			rigidbody2D.velocity = new Vector2 (EnemySpeed, rigidbody2D.velocity.y); 
		} 
		else 
		{
			rigidbody2D.velocity = new Vector2 (EnemySpeed *-1, rigidbody2D.velocity.y); 
		}

	}

	virtual public void Idle()
	{
		if (!strolling)
		{
			StrollStart = transform.position;
			strolling = true;
		}
		
		if (Vector2.Distance (transform.position, StrollStart) > StrollDist) 
		{
			direction *= -1;
			strolling = false;
		}
		
		rigidbody2D.velocity = new Vector2 (EnemySpeed *direction, rigidbody2D.velocity.y); 
		
	}

	void Dash()
	{
		rigidbody2D.AddForce (new Vector2(dashVec.x*direction, dashVec.y));
		State = EnemyState.ATTACK;
	}
	void Jump()
	{
		rigidbody2D.AddForce (new Vector2(jumpVec.x*direction, jumpVec.y));
		State = EnemyState.IDLE;
	}

	void Chase()
	{
		//move until reach edge or near enough to player
		
	}

	void OnCollisionEnter2D(Collision2D colObj){
		if (colObj.collider.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().Hurt(10);
		}
	}

	public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public bool isDash() //raycast in front of enemy, if it hits the player, true
	{
		return Physics2D.Raycast (transform.position, transform.right*direction, dashCast, dashMask) && dashRdy;
	}
	public bool isJump() //raycast in front of enemy, if it hits a wall, and it looks small enough to jump over, true
	{
		//return Physics2D.Raycast (transform.position, (transform.up + transform.right).normalized, jumpCast, jumpMask) && 

			return Physics2D.Raycast (transform.position, transform.right, jumpCast, jumpMask) && jumpRdy;
	}

	public void dashOn() 
	{
		dashRdy = true;
	}
	public void jumpOn() 
	{
		jumpRdy = true;
	}

	public bool isGrounded()
	{
		return Physics2D.Raycast (transform.position, -Vector2.up, groundCast, enemyGroundMask);
	}
}
