using UnityEngine;
using System.Collections;

public class enemyCharacter : baseCharacter {

    public enum EnemyStates
    {
        IDLE,
        ATTACK,
        JUMP
    }

    //Movement Variables
    protected EnemyStates enemyState = EnemyStates.IDLE;
    protected int direction = -1;
    [SerializeField]
    protected float strollDistance = 2f;
    protected Vector2 StrollStartLocation;
    

    //Player Variables
    protected GameObject player;
    protected Itemizer itemCreator;
    protected ScoreKeeper HYPECounter;

    ////AirBlast HYPE Variables
    //protected bool isAirBlasted;
    //protected float airBlastedDuration;
    //protected float airBlastedTimer;
    //protected float airBlastedTimerSeconds;

    //Aggro Variables
    protected Transform visionRayLocation;
    [SerializeField]
    protected float visionRayLength = 4f;
    [SerializeField]
    protected LayerMask visionRayMask = 1024; //layermask for player layer

    //Distances to player
    protected float distanceToPlayerX;
    protected float distanceToPlayerY;
    

	// Use this for initialization
	override protected void Start () {
        base.Start();
        player = GameObject.Find("Player");
        itemCreator = GameObject.Find("Main Camera").GetComponent<Itemizer>();
        HYPECounter = player.GetComponent<ScoreKeeper>();
        strollDistance += Random.Range(0, 1f);
        visionRayLocation = transform.Find("raycasts/Vision Ray");
        StrollStartLocation = transform.position;
	}
	
	// Update is called once per frame
	override protected void Update () {
        if (transform.position.y < -5f) Destroy(gameObject);
        Flip(rb.velocity.x);
        Act();
    }

    virtual protected void Act()
    {
        switch (enemyState)
        {
            case EnemyStates.ATTACK: Attack(); break;
            case EnemyStates.IDLE: Idle(); break;
            case EnemyStates.JUMP: Jump(); break;
            default: Idle(); break;

        }
    }

    virtual protected void Idle()
    {
        //NEEDS TO BE ALERTED IF GUNFIRE IN SAME TRAIN CAR
        if (Physics2D.Raycast(visionRayLocation.position, visionRayLocation.right, visionRayLength, visionRayMask)) //alerts enemy if it sees player in front of them
        {
            enemyState = EnemyStates.ATTACK;
        }
        if (StrollStartLocation.x - transform.position.x > strollDistance){ //when moving left, turn around after exceeding stroll distance
            direction = 1;
        }
        if(transform.position.x >= StrollStartLocation.x){ //when moving right, turn around after returning to StrollStartLocation
            direction = -1;
        }
        rb.velocity = new Vector2(moveSpeed/3 * direction, rb.velocity.y); //Strolling movement
    }

    virtual protected void Jump()
    {

    }

    virtual protected void Attack()
    {

    }
}
