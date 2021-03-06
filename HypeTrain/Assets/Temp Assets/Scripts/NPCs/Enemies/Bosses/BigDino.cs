using UnityEngine;
using System.Collections;

public enum DinoState //enemy states dictate what mode enemies are in
{
	IDLE,
	ATTACK,
	DASH,
	STUN
}

public class BigDino : LogController {
	
	public float health = 100f;
	public DinoState State = DinoState.IDLE; //basic state
	public float DinoSpeed = 6f;
	public float AttackDist = 35f;  //distance at which enemy will switch to attacking
	public float StrollDist = 3f;  //distance enemy walks back and forth during idle
	public Animator Animator;
	public Animator ledgeAnimator;
	[HideInInspector] public GameObject Player;
	[HideInInspector] public int direction = -1; //direction enemy is facing, 1 for right, -1 for left
	[HideInInspector] public float distToPlayer;	
	
	public float stunTime = 3f;
	public Vector2 recoil;
	private bool stunned = false;
	private bool predash = true;
	private bool predashOnce = true;
	private float predashTime = 1f;
	private bool postDash = false;
	private float dashTime = .3f;
	private GameObject wallPos;
	public Vector2 throwPlayer;
	public bool inNotStunRange = false;
	public bool inThrowRange = false;
	public bool throwing = false;
	public float throwCD = 1f;
	
	private float startTime;
	
	//raycast info
	public float dashCast = 20f;
	public LayerMask dashMask;  
	public Vector2 dashVec; //force vector applied during dash
	public float dashCD = 6f; //time between dashes
	public LayerMask enemyGroundMask;
	[HideInInspector] public bool dashRdy = true;
	public float groundCast = 1f;
	[HideInInspector] public Itemizer money;
	[HideInInspector] public ScoreKeeper HYPECounter;
	
	// Use this for initialization
	void Start () {
		health *= Multiplier.enemyHealth;
		DinoSpeed *= Multiplier.enemySpeed;
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
		Player = GameObject.Find("Player");
		HYPECounter = GameObject.Find("Player").GetComponent<ScoreKeeper>();

		transform.localEulerAngles = new Vector3 (0, 180, 0);
		ledgeAnimator.Play ("dinoPlat_idle");
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -5f) Destroy (gameObject);
		//grounde = isGrounded ();
		distToPlayer = Vector2.Distance (transform.position, Player.transform.position);
		//Debug.Log (distToPlayer);
		//Debug.Log (State);
		//if (postDash && inThrowRange) { //for throw during dash
		//	postDash = false;
		//	playerThrow();
		//}
		if ((distToPlayer < AttackDist) && (State == DinoState.IDLE)) 
		{
			State = DinoState.ATTACK;
		} 
		if (State == DinoState.ATTACK && isDash ()) //enemy is about to dash!
		{
			dashRdy = false;           //turn off dashing
			Invoke ("dashOn", dashCD); //turn it back on again after a cooldown
			State = DinoState.DASH; 
		}
		Act();
	}
	
	void FixedUpdate () {
		Flip (transform.GetComponent<Rigidbody2D>().velocity.x); //Uses velocity to determine when to flip
	}
	//Code to flip sprite
	void Flip(float moveH){
		if (!stunned || !predashOnce || State != DinoState.STUN) {
			if (transform.position.x < Player.transform.position.x) {
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else if (transform.position.x > Player.transform.position.x) {
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			}
		}
		
	}
	
	public void Act()
	{
		switch(State)
		{
		case DinoState.ATTACK: Attack(); break;
		case DinoState.IDLE: Idle(); break;
		case DinoState.DASH: Dash (); break;
		case DinoState.STUN: Stun (); break;
		default: Idle();break;
			
		}
	}
	
	private void Attack()
	{
		if (isDash ()) return; //prevents enemy from moving when he should be jumping
		if (inThrowRange && !throwing) {
			throwing = true;
			Invoke ("throwingDone", throwCD);
			playerThrow ();
		}
		if (transform.position.x < Player.transform.position.x) 
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2 (DinoSpeed, GetComponent<Rigidbody2D>().velocity.y); 
		} 
		else 
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2 (DinoSpeed *-1, GetComponent<Rigidbody2D>().velocity.y); 
		}
		
	}
	
	private void Idle()
	{
		
	}
	
	private void Stun()
	{
		Animator.Play ("dino_bite");
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		if(!stunned){
			stunned = true;
			if(transform.position.x > wallPos.transform.position.x){
				GetComponent<Rigidbody2D>().AddForce (new Vector2(recoil.x, recoil.y)); // Recoil off left wall
			} else if(transform.position.x < wallPos.transform.position.x){
				GetComponent<Rigidbody2D>().AddForce (new Vector2(-recoil.x, recoil.y)); // Recoil off rightt wall
			}
			Invoke ("endStun", stunTime);
		}
	}
	
	private void endStun() {
		Animator.Play ("dino_walk");
		gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		postDash = false;	//resets dashCD
		State = DinoState.ATTACK;
		stunned = false;
	}
	
	private void pause() { //pre dash pause to give player time to dodge
		Animator.Play ("dino_run");
		if(transform.position.x > Player.transform.position.x) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (1f, GetComponent<Rigidbody2D>().velocity.y);
			dashVec.x = -dashVec.x;//Store the dash direction upon pausing
		} else {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (-1f, GetComponent<Rigidbody2D>().velocity.y);
		}
		Invoke ("unpause", predashTime);
	}
	private void unpause() {
		predash = false;
	}
	private void Dash()
	{
		if(postDash){ 
			Animator.Play ("dino_walk");
			return;
		}
		if(predashOnce) {
			predashOnce = false;
			pause ();
		}
		if(!predash) {
			predash = true;
			predashOnce = true;
			
			GetComponent<Rigidbody2D>().AddForce (new Vector2(dashVec.x, dashVec.y)); //Execute dash
			dashVec.x = Mathf.Abs (dashVec.x);						  //Reset sign (+/-) of dashVec.x
			
			postDash = true;
			Invoke ("aggro", dashTime);
		}
	}
	
	private void playerCollideOn() {
		Physics2D.IgnoreCollision (Player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
	}
	
	public void playerThrow() {
		Player.GetComponent<PlayerHealth>().HurtPlus(10, gameObject);
		Invoke ("playerCollideOn", 1.5f);
		Physics2D.IgnoreCollision (Player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
	}
	
	void OnCollisionEnter2D(Collision2D colObj){
		if (colObj.collider.tag == "Player") {
            colObj.gameObject.GetComponent<PlayerCharacter>().Hurt(10, gameObject);
        }
		if(State == DinoState.DASH && colObj.collider.tag == "wall" && !inNotStunRange) {
			wallPos = colObj.gameObject;
			State = DinoState.STUN;
		}
		
	}
	
	virtual public void Hurt(float damage){
		if(State == DinoState.IDLE) {
			State = DinoState.ATTACK;
		}
		health -= damage;
		if (health <= 0) {
			Game.incBossesKilled();
			//Game.incBounty(8);
			money.At (transform.position, Random.Range ((int)(10 * Multiplier.moneyDrop),(int)(50 * Multiplier.moneyDrop)));
			HYPECounter.incrementHype(true); //Increment HYPE twice for big kill
			HYPECounter.incrementHype(true);
			ScoreKeeper.EnemiesKilled += 1; //Increment # of kills in current run
			ledgeAnimator.SetBool ("fall", true);
			Destroy (gameObject);
		}
	}
	
	public bool isDash() //raycast in front of enemy, if it hits the player, true
	{
		if(distToPlayer < AttackDist - (AttackDist/10) && dashRdy) {
			return true;
		} else {
			return false;
		}
		//return Physics2D.Raycast (dashCastTransform.position, transform.right, dashCast, dashMask) && dashRdy;
	}
	
	public void dashOn() 
	{
		dashRdy = true;
	}
	
	private void throwingDone() {
		throwing = false;
	}
	
	public bool isGrounded()
	{
		return Physics2D.Raycast (transform.position, -Vector2.up, groundCast, enemyGroundMask);
	}
	
	public void aggro()
	{
		if(State != DinoState.STUN) {
			postDash = false;
			State = DinoState.ATTACK;
		}
	}

	private void fallenState(){
		ledgeAnimator.Play ("dinoPlat_fallen");
		Destroy (gameObject);
	}
	
}
