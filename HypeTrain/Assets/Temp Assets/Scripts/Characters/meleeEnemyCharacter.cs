using UnityEngine;
using System.Collections;

public class meleeEnemyCharacter : enemyCharacter {
    public GameObject deathParticles;

	// Use this for initialization
	override protected void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    override protected void Jump()
    {

    }

    override protected void Attack()
    {
        if (player.transform.position.x < transform.position.x) {
            direction = -1;
        } else {
            direction = 1;
        }
        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
    }

    protected override void Death()
    {
        base.Death();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }
}
