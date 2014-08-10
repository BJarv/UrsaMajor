using UnityEngine;
using System.Collections;

public class ShootingEnemy : Enemy {

	private EnemyGun gun;


	void Start () {
		gun = transform.Find ("enemyGun").GetComponent<EnemyGun>();
	}

	override public void Attack()
	{
		base.Attack ();
		gun.isShooting(true, direction);
	}

	//override public void Chase()
}
