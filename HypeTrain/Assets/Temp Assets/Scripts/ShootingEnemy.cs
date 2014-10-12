using UnityEngine;
using System.Collections;

public class ShootingEnemy : Enemy {

	private EnemyGun gun;
	public bool spawnKey = false;

	override protected void Start () {  //overrides start function of enemy.cs
		base.Start ();
		health = 10f;
		gun = transform.Find ("enemyGun").GetComponent<EnemyGun>();
	}

	override protected void Attack() //overrides attack function of enemy.cs
	{
		base.Attack ();
		gun.isShooting(true, direction);
	}

	override public void Hurt(float damage){
		State = EnemyState.ATTACK;
		health -= damage;
		if (health <= 0) {
			if(spawnKey) {
				money.At (transform.position, 1); //1 for key
			}
			int repeat = (int)Random.Range (1, 5); //spawn coins between 1 and 5
			while(repeat > 0){
				money.At (transform.position, 0); //0 for coin
				repeat -= 1;
			}
			Destroy (gameObject);
		}
	}

	override protected void Update () {
		
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
