using UnityEngine;
using System.Collections;

public class ShootingEnemy : Enemy {

	private EnemyGun gun;


	override protected void Start () {
		base.Start ();
		gun = transform.Find ("enemyGun").GetComponent<EnemyGun>();
	}

	override protected void Attack()
	{
		base.Attack ();
		gun.isShooting(true, direction);
	}

	//override public void Chase()
}
