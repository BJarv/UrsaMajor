using UnityEngine;
using System.Collections;

public class enemyCharacter : baseCharacter
{

    public enum EnemyStates
    {
        IDLE,
        DELAY,
        ATTACK,
        JUMP
    }

    //Movement Variables
    protected EnemyStates enemyState = EnemyStates.IDLE;
    protected int direction = -1;
    [SerializeField]
    protected float strollDistance = 2f;
    protected Vector2 StrollStartLocation;

    //Jump variables
    protected bool startJump = false;
    [SerializeField] protected float jumpDistance = 400f;
    [SerializeField] protected float jumpHeight = 800f;


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
    protected bool alerted = false;
    protected float delayTime = 1f;
    protected Transform visionRayLocation;
    protected Transform jumpCheckLocation;
    protected Transform obstacleHeightCheckLocation;
    [SerializeField]
    protected float visionRayLength = 4f;
    [SerializeField]
    protected float jumpCheckRayLength = 4f;
    [SerializeField]
    protected float obstacleCheckRayLength = 4f;
    [SerializeField]
    protected LayerMask visionRayMask = 1024; //layermask for player layer
    [SerializeField]
    protected LayerMask jumpCheckRayMask = 1024; //layermask for ???
    [SerializeField]
    protected LayerMask obstacleCheckRayMask = 1024; //layermask for ???

    //Distances to player
    protected float distanceToPlayerX;
    protected float distanceToPlayerY;


    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        itemCreator = GameObject.Find("Main Camera").GetComponent<Itemizer>();
        HYPECounter = player.GetComponent<ScoreKeeper>();
        strollDistance += Random.Range(0, 1f);
        visionRayLocation = transform.Find("raycasts/Vision Ray");
        jumpCheckLocation = transform.Find("raycasts/JumpCheck"); //checks to see if you need to jump
        obstacleHeightCheckLocation = transform.Find("raycasts/ObstacleCheck"); //checks to see if you can clear the obstacle
        StrollStartLocation = transform.position;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (transform.position.y < -5f) Destroy(gameObject);
        Flip(rb.velocity.x);
        Act();
    }

    // Use for physics calculations
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if(enemyState == EnemyStates.JUMP)
        {
            rb.AddForce(new Vector2(airMoveSpeed * direction, 0));
        }
    }

    virtual protected void Act()
    {
        switch (enemyState)
        {
            case EnemyStates.ATTACK: Attack(); break;
            case EnemyStates.IDLE: Idle(); break;
            case EnemyStates.DELAY: Delay(); break;
            case EnemyStates.JUMP: Jump(); break;
            default: Idle(); break;

        }
    }

    virtual protected void Idle()
    {
        //NEEDS TO BE ALERTED IF GUNFIRE IN SAME TRAIN CAR
        if (Physics2D.Raycast(visionRayLocation.position, visionRayLocation.right, visionRayLength, visionRayMask)) //alerts enemy if it sees player in front of them
        {
            enemyState = EnemyStates.DELAY;
        }
        if (StrollStartLocation.x - transform.position.x > strollDistance)
        { //when moving left, turn around after exceeding stroll distance
            direction = 1;
        }
        if (transform.position.x >= StrollStartLocation.x)
        { //when moving right, turn around after returning to StrollStartLocation
            direction = -1;
        }
        rb.velocity = new Vector2(moveSpeed / 3 * direction, rb.velocity.y); //Strolling movement
    }

    virtual protected void Delay()
    {
        if (!alerted)
        {
            //characterAnimator.SetBool("alerted", true); // NEED ANIM STATE
            Invoke("SwitchToAttack", delayTime);
            alerted = true;
        }
    }

    protected void SwitchToAttack()
    {
        enemyState = EnemyStates.ATTACK;
        alerted = false;
    }

    //Initiate a jump then return to attack state
    virtual protected void Jump()
    {
        if (IsGrounded() && !startJump)
        {
            startJump = true;
            rb.AddForce(new Vector2(0, jumpHeight));
        } else if(IsGrounded()) {
            startJump = false;
            enemyState = EnemyStates.ATTACK;
        }
        
    }

    virtual protected void Attack()
    {
        
    }

    public bool JumpCastSuccess()
    {
        return Physics2D.Raycast(jumpCheckLocation.position, Vector2.right * direction, jumpCheckRayLength, jumpCheckRayMask);
            //&& Physics2D.Raycast(obstacleCheckLocation.position, Vector2.right, obstacleCheckRayLength, obstacleCheckRayMask);
    }
}
