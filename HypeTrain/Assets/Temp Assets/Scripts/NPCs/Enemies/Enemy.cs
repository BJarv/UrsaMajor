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

public class Enemy : LogController {

	public float health = 20f;
	public EnemyState State = EnemyState.IDLE; //basic state
	public float EnemySpeed = 2f;
	public float AttackDist = 45f;  //distance at which enemy will switch to attacking
	public float StrollDist = 3f;  //distance enemy walks back and forth during idle

	[HideInInspector] public bool airBlasted = false; //Am I being hit by an air blast??
	private float airBlastedTime = 0;
	private float timer = 0;
	private float timeSecs = 0;

	[HideInInspector] public GameObject Player;
	[HideInInspector] public int direction = -1; //direction enemy is facing, 1 for right, -1 for left
	[HideInInspector] public float distToPlayer;	
	[HideInInspector] public float verticalDistToPlayer;
	
	private Vector2 StrollStart = new Vector2(0, 0);
	
	private bool strolling = false;


	//raycast info
	public float dashCast = 20f; //wall distance
	public float jumpCast = 5f; //jump distance
	public LayerMask dashMask;  
	public LayerMask jumpMask; 
	public Vector2 dashVec; //force vector applied during dash
	public Vector2 jumpVec; //force vector applied during jump
	public float dashCD = 2f; //time between dashes
	public float jumpCD = 2f; //time between jumps
	public LayerMask enemyGroundMask;
	[HideInInspector] public bool dashRdy = true;
	[HideInInspector] public bool jumpRdy = true;
	[HideInInspector] public float groundCast = 1f;
	[HideInInspector] public Itemizer money;
	[HideInInspector] public ScoreKeeper HYPECounter;

	public float initialDelay;
	public bool delayDone = false;

	// Use this for initialization
	virtual protected void Start () {
		health *= Multiplier.enemyHealth;
		EnemySpeed *= Multiplier.enemySpeed;
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
		Player = GameObject.Find("Player");
		HYPECounter = GameObject.Find("Player").GetComponent<ScoreKeeper>();
		initialDelay = Random.Range (.1f, .5f);
		Invoke ("endDelay", initialDelay);
	}
	
	// Update is called once per frame
	virtual protected void Update () {
		//Update timer:
		timer += Time.deltaTime;
		if (timer >= 1) {
			timeSecs += 1;
			timer = 0;
		}
		//See if done being blasted by air:
		if (timeSecs - airBlastedTime >= 1) {
			airBlasted = false;
		}
		if (airBlasted && GetComponent<Rigidbody2D> ().velocity.y == 0) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0,5));
			Debug.Log ("VELOCITY ADJUSTED");
		}
		if(delayDone) {
			if(transform.position.y < -5f) Destroy (gameObject);
			//grounde = isGrounded ();
			distToPlayer = Vector2.Distance (transform.position, Player.transform.position);
			//Debug.Log (distToPlayer);
			if ((distToPlayer < AttackDist) && (State != EnemyState.ATTACK) && (State != EnemyState.DASH) && (State != EnemyState.JUMP)) 
			{
				State = EnemyState.ATTACK;
			} 
			if (State == EnemyState.ATTACK && isJump ()) //enemy is about to jump!
			{
				jumpRdy = false;           //turn off jumping
				Invoke ("jumpOn", jumpCD); //turn it back on again after a cooldown
				State = EnemyState.JUMP;
			}
			if (State == EnemyState.ATTACK && isDash ()) //enemy is about to dash!
			{
				dashRdy = false;           //turn off dashing
				Invoke ("dashOn", dashCD); //turn it back on again after a cooldown
				State = EnemyState.DASH; 
			}
			Act();
		}
	}

	void FixedUpdate () {
		Flip (transform.GetComponent<Rigidbody2D>().velocity.x); //Uses velocity to determine when to flip
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
		if (isJump ()) return; //prevents enemy from moving when he should be jumping
		if (isDash ()) return;
		if (!airBlasted) {
			if (transform.position.x < Player.transform.position.x) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (EnemySpeed, GetComponent<Rigidbody2D> ().velocity.y); 
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-EnemySpeed, GetComponent<Rigidbody2D> ().velocity.y); 
			}
		}
	}

	virtual public void Idle(){
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
		
		GetComponent<Rigidbody2D>().velocity = new Vector2 (EnemySpeed *direction, GetComponent<Rigidbody2D>().velocity.y); 	
	}

	void Dash()
	{
		GetComponent<Rigidbody2D>().AddForce (new Vector2(dashVec.x*direction, dashVec.y));
		State = EnemyState.ATTACK;
	}
	void Jump()
	{
		GetComponent<Rigidbody2D>().AddForce (new Vector2(jumpVec.x*direction, jumpVec.y));
		State = EnemyState.IDLE;
	}

	void Chase()
	{
		//move until reach edge or near enough to player
		
	}

	void OnCollisionEnter2D(Collision2D colObj){
		if (colObj.collider.tag == "Player" && colObj.collider.transform.name != "Tucker") {
            colObj.gameObject.GetComponent<PlayerCharacter>().Hurt(10, gameObject);
        }
		//If enemy collides with something at a high speed, hurt it
		if (colObj.collider.tag != "Player" && colObj.collider.transform.name != "Tucker") {
			if (airBlasted){
				// If the enemy hits a wall at high speed, 20 damage
				// Medium speed, 10 damage
				// Low speed, 5 damage
				float xVel = Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.x);
				float yVel = Mathf.Abs(GetComponent<Rigidbody2D> ().velocity.y);
				float hurtVel = (xVel > yVel) ? xVel : yVel;
				if (hurtVel > 10f && hurtVel < 20f) Hurt(10f);
				if (hurtVel >= 20f) Hurt (20f);
				else Hurt (5f);
			}
		}
	}

	virtual public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		if (health <= 0) {
			Debug.Log ("ENEMY DOWN");
			Game.incEnemiesKilled();
			Game.incBounty(1);
			money.At (transform.position, Random.Range ((int)(1 * Multiplier.moneyDrop),(int)(6 * Multiplier.moneyDrop)));
			HYPECounter.incrementHype(true); //Increment HYPE on kill
			ScoreKeeper.EnemiesKilled += 1; //Increment # of kills in current run
			Destroy (gameObject);
		}
	}

	public bool isDash() //raycast in front of enemy, if it hits the player, true
	{
		return Physics2D.Raycast (transform.position, transform.right, dashCast, dashMask) && dashRdy;
	}
	public bool isJump() //raycast in front of enemy, if it hits a wall, and it looks small enough to jump over, true
	{
		//return Physics2D.Raycast (transform.position, (transform.up + transform.right).normalized, jumpCast, jumpMask) && 
		//if (Physics2D.Raycast (transform.position, transform.right, jumpCast, jumpMask)) {
		//	return true;
		//}
		return Physics2D.Raycast (transform.position, transform.right, jumpCast, jumpMask) && jumpRdy && isGrounded ();
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

	public bool verticalWithPlayer()
	{
		return Mathf.Abs(transform.position.x - Player.transform.position.x) < 0.05;
	}

	public void aggro()
	{
		State = EnemyState.ATTACK;
	}

	public void shortenAttack()
	{
		AttackDist = AttackDist/2;
	}
	public void endDelay() {
		delayDone = true;
	}

	public void blastedByAir() {
		Debug.Log ("BLASTED BY AIR CALLED");
		airBlasted = true;
		airBlastedTime = timeSecs;
	}

}
