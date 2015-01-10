using UnityEngine;
using System.Collections;

public enum DinoState //enemy states dictate what mode enemies are in
{
	IDLE,
	ATTACK,
	DASH,
	STUN
}

public class BigDino : MonoBehaviour {

	public float health = 100f;
	public DinoState State = DinoState.IDLE; //basic state
	public float DinoSpeed = 6f;
	public float AttackDist = 35f;  //distance at which enemy will switch to attacking
	public float StrollDist = 3f;  //distance enemy walks back and forth during idle
	public Animator Animator;
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
	private Transform dashCastTransform;
	private GameObject wallPos;

	private float startTime;
	
	//raycast info
	public float dashCast = 20f;
	public LayerMask dashMask;  
	public Vector2 dashVec; //force vector applied during dash
	public float dashCD = 3f; //time between dashes
	public LayerMask enemyGroundMask;
	[HideInInspector] public bool dashRdy = true;
	public float groundCast = 1f;
	[HideInInspector] public Itemizer money;
	[HideInInspector] public ScoreKeeper HYPECounter;
	
	// Use this for initialization
	void Start () {
		health *= Multiplier.enemyHealth;
		DinoSpeed *= Multiplier.enemySpeed;
		dashCastTransform = gameObject.transform.Find ("dashCast");
		money = GameObject.Find ("Main Camera").GetComponent<Itemizer>();
		Player = GameObject.Find("character");
		HYPECounter = GameObject.Find("character").GetComponent<ScoreKeeper>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -5f) Destroy (gameObject);
		//grounde = isGrounded ();
		distToPlayer = Vector2.Distance (transform.position, Player.transform.position);
		//Debug.Log (distToPlayer);
		Debug.Log (State);
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
		Flip (transform.rigidbody2D.velocity.x); //Uses velocity to determine when to flip
	}
	//Code to flip sprite
	void Flip(float moveH){
		if (!stunned || !predashOnce) {
			if (moveH > 0) {
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else if (moveH < 0) {
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
		if (transform.position.x < Player.transform.position.x) 
		{
			rigidbody2D.velocity = new Vector2 (DinoSpeed, rigidbody2D.velocity.y); 
		} 
		else 
		{
			rigidbody2D.velocity = new Vector2 (DinoSpeed *-1, rigidbody2D.velocity.y); 
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
				rigidbody2D.AddForce (new Vector2(recoil.x, recoil.y)); // Recoil off left wall
			} else if(transform.position.x < wallPos.transform.position.x){
				rigidbody2D.AddForce (new Vector2(-recoil.x, recoil.y)); // Recoil off rightt wall
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
		//Store the dash direction upon pausing
		if(transform.position.x > Player.transform.position.x)dashVec.x = -dashVec.x;
		else if(transform.position.x <= Player.transform.position.x)dashVec.x = dashVec.x;
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

			rigidbody2D.AddForce (new Vector2(dashVec.x, dashVec.y)); //Execute dash
			dashVec.x = Mathf.Abs (dashVec.x);						  //Reset sign (+/-) of dashVec.x

			postDash = true;
			Invoke ("aggro", dashTime);
		}
	}
	
	void OnCollisionEnter2D(Collision2D colObj){
		if (colObj.collider.tag == "Player") {
			colObj.gameObject.GetComponent<PlayerHealth>().Hurt(10);
			if(transform.position.x > colObj.transform.position.x)
			{
				Player.rigidbody2D.AddForce(new Vector2(-200, 375));
			}
			else if(transform.position.x < colObj.transform.position.x)
			{
				Player.rigidbody2D.AddForce(new Vector2(200, 375));
			}
			Player.GetComponent<CharControl>().hitAnim();
		}
		if(State == DinoState.DASH && colObj.collider.tag == "wall") {
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
			money.At (transform.position, Random.Range ((int)(10 * Multiplier.moneyDrop),(int)(50 * Multiplier.moneyDrop)));
			HYPECounter.incrementHype(true); //Increment HYPE twice for big kill
			HYPECounter.incrementHype(true);
			Destroy (gameObject);
		}
	}
	
	public bool isDash() //raycast in front of enemy, if it hits the player, true
	{
		return Physics2D.Raycast (dashCastTransform.position, transform.right, dashCast, dashMask) && dashRdy;
	}

	public void dashOn() 
	{
		dashRdy = true;
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

}
