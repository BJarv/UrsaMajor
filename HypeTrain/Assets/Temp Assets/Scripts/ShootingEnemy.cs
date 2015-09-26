using UnityEngine;
using System.Collections;
using System;

public class ShootingEnemy : Enemy {

	private EnemyGun gun;
	private EnemyShotgun shotgun;
	private EnemyLauncher launcher;
	private EnemyLobber lobber;
	public float stopAndShootRange = 10f;
	public bool spawnKey = false;

	override protected void Start () {  //overrides start function of enemy.cs
		base.Start ();
		try {
			gun = transform.Find ("enemyGun").GetComponent<EnemyGun>();
		} catch {
			//Debug.Log ("No 'enemyGun' found, looking for shotgun");
			try {
				shotgun = transform.Find ("enemyShotgun").GetComponent<EnemyShotgun>();
			} catch {
				//Debug.Log ("No 'enemyShotgun' found, looking for launcher");
				try {
					launcher = transform.Find ("missilePoint").GetComponent<EnemyLauncher>();
				} catch {
					//Debug.Log ("No 'enemyLauncher' found, looking for lobber");
					try {
						lobber = transform.Find ("lobPoint").GetComponent<EnemyLobber>();
					} catch {
						Debug.Log ("No gun attached to a shooting enemy");
					}
				}
			}
		}
	}

	override protected void Attack() //overrides attack function of enemy.cs
	{
		//base.Attack ();
		if (isJump ()) return; //prevents enemy from moving when he should be jumping
		if (isDash ()) return;
		if (!airBlasted && distToPlayer > stopAndShootRange) {
			if (transform.position.x < Player.transform.position.x) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (EnemySpeed, GetComponent<Rigidbody2D> ().velocity.y); 
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-EnemySpeed, GetComponent<Rigidbody2D> ().velocity.y); 
			}
		} else {
			GetComponent<Rigidbody2D> ().velocity = new Vector2(0, GetComponent<Rigidbody2D> ().velocity.y);
		}
		if(gun != null){ //shoot the correct gun type
			gun.isShooting(true);
		} else if(shotgun != null) {
			shotgun.isShooting(true);
		} else if(launcher != null) {
			launcher.isShooting(true);
		} else if(lobber != null) {
			lobber.isShooting(true);
		} 
	}

	override public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		if (health <= 0) {
			if(spawnKey) {
				money.keyAt (transform.position); 
			}
			money.At (transform.position, (int)UnityEngine.Random.Range ((int)(5 * Multiplier.moneyDrop),(int)(11 * Multiplier.moneyDrop))); 	
			HYPECounter.incrementHype(true); //Increment HYPE on kill
			ScoreKeeper.DisplayEnemiesKilled += 1; //Increment # of kills in current run
			Destroy (gameObject);
		}
	}

	override protected void Update () {
		if(transform.position.y < -5f) Destroy (gameObject);
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
		Act();
	}
}
 