using UnityEngine;
using System.Collections;

public class BirdKeeper : ShootingEnemy {

	private EnemyGun gun;
	private EnemyShotgun shotgun;
	private EnemyLauncher launcher;
	private EnemyLobber lobber;
	private Animator anim;
	private bool dying;
	private ParticleSystem sparks;

	override protected void Start () {  //overrides start function of enemy.cs
		anim = GetComponent<Animator> ();
		base.Start ();
		sparks = transform.Find ("sparks").GetComponent<ParticleSystem> ();
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
			launcher.setAnimator(anim);
			//anim.SetBool ("throwing", true);
		} else if(lobber != null) {
			lobber.isShooting(true);
		} 
	}


	//HE DOESNT DIE AT 0???
	override public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		if (health <= 30 && dying == false) {
			dying = true;
			sparks.Play();
			anim.SetTrigger ("dying");
		}
		if (health <= 0) {
			if(spawnKey) {
				money.keyAt (transform.position); 
			}
			money.At (transform.position, (int)UnityEngine.Random.Range ((int)(5 * Multiplier.moneyDrop),(int)(11 * Multiplier.moneyDrop))); 	
			HYPECounter.incrementHype(true); //Increment HYPE on kill
			ScoreKeeper.EnemiesKilled += 1; //Increment # of kills in current run
			Destroy (gameObject);
		}
	}
}
