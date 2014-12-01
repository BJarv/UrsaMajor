using UnityEngine;
using System.Collections;
using System;


public class ShootingEnemy : Enemy {

	private EnemyGun gun;
	private EnemyShotgun shotgun;
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
				Debug.Log ("No gun attached to a shooting enemy");
			}
		}
	}

	override protected void Attack() //overrides attack function of enemy.cs
	{
		base.Attack ();
		if(gun != null){ //shoot the correct gun type
			gun.isShooting(true, direction);
		} else if(shotgun != null) {
			shotgun.isShooting(true, direction);
		}
	}

	override public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		if (health <= 0) {
			if(spawnKey) {
				money.At (transform.position, 1); //1 for key
			}
			int repeat = (int)UnityEngine.Random.Range (1, 5); //spawn coins between 1 and 5
			while(repeat > 0){
				money.At (transform.position, 0); //0 for coin
				repeat -= 1;
			}
			HYPECounter.incrementHype(true); //Increment HYPE on kill
			Destroy (gameObject);
		}
	}

	override protected void Update () {
		if(transform.position.y < -5f) Destroy (gameObject);
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
		Act();
	}
}
