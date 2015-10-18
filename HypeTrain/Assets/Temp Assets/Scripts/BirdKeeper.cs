using UnityEngine;
using System.Collections;

public class BirdKeeper : ShootingEnemy {
	
	private EnemyLauncher launcher;
	private Animator anim;
	private bool dying;
	private ParticleSystem sparks;

	override protected void Start () {  //overrides start function of enemy.cs
		anim = GetComponent<Animator> ();
		base.Start ();
		sparks = transform.Find ("sparks").GetComponent<ParticleSystem> ();
		try {
			launcher = transform.Find ("missilePoint").GetComponent<EnemyLauncher>();
		} catch {
					//Debug.Log ("No 'enemyLauncher' found, looking for lobber");
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
		if(launcher != null) {
			launcher.isShooting(true);
			launcher.setAnimator(anim);
			//anim.SetBool ("throwing", true);
		}
	}

	
	override public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		//This only happens once when BirdKeeper starts having low health.
		if (health <= 30 && dying == false) {
			dying = true;
			sparks.Play();
			anim.SetTrigger ("dying");
			//The launcher timers are changed and reset
			launcher.shootCD = 2f;
			launcher.shootAnimCD = 1.75f;
			launcher.shotTimer = launcher.shootCD;
			launcher.animTimer = launcher.shootAnimCD;
			launcher.justDidAnim = false;
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
