using UnityEngine;
using System.Collections;

public class meleeEnemyCharacter : enemyCharacter
{
    public GameObject deathParticles;

    //new public enum EnemyStates
    //{
    //    IDLE,
    //    ALERT,
    //    ATTACK,
    //    JUMP,
    //    DASH
    //}
    // Use this for initialization
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    override protected void Attack()
    {
        //Only move while grounded
        if (IsGrounded())
        {
            //Set direction
            if (player.transform.position.x < transform.position.x) direction = -1;
            else direction = 1;
            if (JumpCastSuccess())
            {
                enemyState = EnemyStates.JUMP;
            }
            //Otherwise move regularly
            else rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
        }

    }

    override protected void Death()
    {
        base.Death();
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
