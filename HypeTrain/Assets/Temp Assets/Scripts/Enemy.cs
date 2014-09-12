using UnityEngine;
using System.Collections;

public enum EnemyState
{
	IDLE,
	ATTACK,
	CHASE,
	DASH
}

public class Enemy : MonoBehaviour {

	public float health = 20f;

	public EnemyState State = EnemyState.IDLE;
	public float EnemySpeed = 2f;
	public GameObject Player = null;
	private float AttackDist = 10f;
	public int direction = 1;
	public float StrollDist = 3f;
	public float distToPlayer;
	public Vector3 posOfTrans1;
	public Vector3 posOfTrans2;
	
	private Vector2 StrollStart = new Vector2(0, 0);
	
	private bool strolling = false;

	private float startTime;

	//raycast info
	public float dashCast = 1f;
	public float jumpCast = 2f;
	public LayerMask dashMask;
	public LayerMask jumpMask; 
	public Vector2 dashVec;
	public Vector2 jumpVec;
	public float dashCD = 2f;
	private bool dashRdy = true;

	public bool dashable;

	// Use this for initialization
	virtual protected void Start () {
		Player = GameObject.Find("character");
	}
	
	// Update is called once per frame
	void Update () {
	
		posOfTrans1 = transform.position;
		posOfTrans1 = Player.transform.position;
		distToPlayer = Vector2.Distance (transform.position, Player.transform.position);
		//Debug.Log (distToPlayer);
		if (distToPlayer < AttackDist) 
		{
			State = EnemyState.ATTACK;
		} 
		if (isDash ()) 
		{
			dashRdy = false;
			Invoke ("dashOn", dashCD);
			State = EnemyState.DASH;
		}
		Act();
	}

	void Act()
	{
		switch(State)
		{
			case EnemyState.ATTACK: Attack(); break;
			case EnemyState.IDLE: Idle(); break;
			case EnemyState.CHASE: Chase(); break;
			case EnemyState.DASH: Dash (); break;
			default: Idle();break;
			
		}
	}

	virtual protected void Attack()
	{
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
		health -= damage;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public bool isDash()
	{
		return Physics2D.Raycast (transform.position, Vector2.right*direction, dashCast, dashMask) && dashRdy;
	}
	public bool isJump()
	{
		return Physics2D.Raycast (transform.position, (Vector2.up + (Vector2.right*direction)).normalized, jumpCast, jumpMask) && 
			Physics2D.Raycast (transform.position, Vector2.right*direction, dashCast, jumpMask);
	}

	public void dashOn()
	{
		dashRdy = true;
	}
}
