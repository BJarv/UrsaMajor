using UnityEngine;
using System.Collections;

public class baseCharacter : MonoBehaviour
{

    protected enum JumpStates
    {
        GROUNDED,
        JUMPING,
        FALLING
    }

    //Movement variables
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float maxJumpForce;
    protected JumpStates jumpState;
    [SerializeField]
    protected Vector2 hurtKnockback;

    //Health variables
    [SerializeField]
    protected int maxHealth;
    protected int currentHealth;

    //Ground check variables
    protected Transform groundCheckTransform;
    [SerializeField]
    protected float groundCheckRayLength = 0.35f; //0.27 for player(maybe for standard enemies as well)
    [SerializeField]
    public LayerMask groundMask;

    //Component references
    protected Rigidbody2D rb;
    protected Animator characterAnimator;

    // Use this for initialization
    virtual protected void Start()
    {
        //Set component references
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        groundCheckTransform = transform.FindChild("raycasts/groundCheck").transform;

        currentHealth = maxHealth;

    }

    // Update is called once per frame
    virtual protected void Update() { }

    virtual protected void FixedUpdate() {

    }

    //Decrement health, play animation, apply knockback
    virtual protected void Hurt(int damage, GameObject dmgObj) {
        //Decrement character health
        currentHealth -= damage;
        if (currentHealth <= 0) Death();

        //Play hit animation
        characterAnimator.SetBool("Hit", true);
        characterAnimator.SetBool("Jump", false);
        Invoke("hitToIdle", .25f);

        //Knock back player in a direction depending on their position relative to the damaging object
        if (dmgObj.transform.position.x - transform.position.x > 0)
        {
            rb.AddForce(new Vector2(-hurtKnockback.x, hurtKnockback.y)); //Knock up and to the left
        }
        else if (dmgObj.transform.position.x - transform.position.x < 0)
        {
            rb.AddForce(hurtKnockback); //Knock up and to the right
        }
    }

    virtual protected void Death() { }

    //Flip sprite depending on direction / velocity
    public void Flip(float moveH)
    {
        if (moveH > 0)
            transform.localEulerAngles = new Vector3(0, 0, 0);
        else if (moveH < 0)
            transform.localEulerAngles = new Vector3(0, 180, 0);
    }

    //Raycast to check if the character is on the ground
    public bool IsGrounded() { return Physics2D.OverlapArea(new Vector2(groundCheckTransform.position.x, groundCheckTransform.position.y), new Vector2(groundCheckTransform.position.x + groundCheckRayLength, groundCheckTransform.position.y - .01f), groundMask) != null; }

    //Line to be invoked after delay in hurt
    public void HitToIdle() { characterAnimator.SetBool("Hit", false); }

    /*
    public delegate void DelayedFunction();
    public void Delay(float delayTime, DelayedFunction df) {
        float timer = 0;
        if (timer < delayTime)
        {
            timer += Time.deltaTime;
        }
        else {
            df();
            timer = 0;
        }
    }*/
}
