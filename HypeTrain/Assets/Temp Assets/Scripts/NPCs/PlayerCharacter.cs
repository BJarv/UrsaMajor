using UnityEngine;
using System.Collections;

public class PlayerCharacter : baseCharacter
{
    //Jump force variables
    public float plusJumpForce = 300f;
    public float fastFallForce = 50f;
    public float maxFallSpeed = 400f;
    
    private float currentJumpForce = 0f;
    private float forceToAdd = 0f;
    private bool applyJumpForce = false;

    //Horizontal input variable
    private float moveH = 0;

    //Player health variables
    public static float playerHealth;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public float invincibilityCooldown = .5f;
    public float deathDelay = 1.5f;
    [HideInInspector] public bool invincibility;
    public static bool alreadyDying = false;
    public static bool playerDead;
    private bool healed = false;

    //Experimental Jump Variables
    private bool jumpButtonPressed = false;
    private bool jumping = false;
    public float jumpVector = 300f;
    public float minJumpTime = .1f;
    public float maxJumpTime = .3f;

    private Popup popupManager;

    // Use this for initialization
    override protected void Start() {
        base.Start();
        popupManager = Camera.main.transform.GetComponent<Popup>();
        currentHealth = maxHealth;
        alreadyDying = false;
    }

    // Update is called once per frame
    override protected void Update()
    {
        jumpButtonPressed = Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("LTrig") == 1;

        //Grounded movement
        moveH = Input.GetAxisRaw("Horizontal");
        Flip(moveH);

        //Jumpstate handler
        switch (jumpState) {
            case JumpStates.GROUNDED:
                if (jumpButtonPressed && IsGrounded())
                {
                    jumpState = JumpStates.JUMPING;
                    characterAnimator.SetBool("Jump", true); //Switch to jump animation
                    characterAnimator.SetBool("Hit", false);
                }
                break;

            case JumpStates.JUMPING:
                if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("LTrig") > 0.1) && currentJumpForce < maxJumpForce)
                {
                    applyJumpForce = true; //Adds force in fixed update
                }
                else
                {
                    jumpState = JumpStates.FALLING;
                    currentJumpForce = 0;
                    applyJumpForce = false;
                }
                break;

            case JumpStates.FALLING:

                if (IsGrounded() && rb.velocity.y <= 0)
                {
                    //Debug.Log("Grounded"); Use this to debug jump issues
                    jumpState = JumpStates.GROUNDED;
                    characterAnimator.SetBool("Jump", false); //End jump animation
                }
                break;
        }

        //If player runs out of health or falls below
        if((currentHealth <= 0f || transform.position.y < -5f) && !alreadyDying){
            alreadyDying = true;
            StartDeath();
        }
    }

    // Use for actions on rigidbodies
    override protected void FixedUpdate()
    {
        if (jumpButtonPressed && !jumping && IsGrounded())
        {
            jumping = true;
            StartCoroutine(JumpRoutine());
        }
        /*if (applyJumpForce) {
            float timeDiff = Time.deltaTime * 100;
            forceToAdd = plusJumpForce * timeDiff;
            currentJumpForce += forceToAdd;
            rb.AddForce(new Vector2(0, forceToAdd));
        }*/

        if (IsGrounded())
        {
            //Grounded Movement
            if (moveH > 0)
            {
                characterAnimator.SetBool("Run", true);
                if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else if (moveH == 0) characterAnimator.SetBool("Run", false);
            else if (moveH < 0)
            {
                characterAnimator.SetBool("Run", true);
                if (rb.velocity.x > 0) rb.velocity = new Vector2(0, rb.velocity.y);
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        } 
        else
        {
            //Aerial Horizontal Movement
            if (moveH > 0)
            {
                if (rb.velocity.x < moveSpeed)
                    rb.AddForce(new Vector2(moveH * airMoveSpeed, 0));
            }
            else if (moveH < 0)
            {
                if (rb.velocity.x > -moveSpeed)
                    rb.AddForce(new Vector2(moveH * airMoveSpeed, 0));
            }
            //Fast-fall
            if(Input.GetAxisRaw("Vertical") == -1)
            {
                //Check that player is falling and not already exceeding 'maxFallSpeed' before applying more fastFallForce
                if(rb.velocity.y < 0 && rb.velocity.y > -maxFallSpeed) rb.AddForce(new Vector2(0, -fastFallForce));
            }
        }
    }

    IEnumerator JumpRoutine() {
        //Only zero'ing out the y axis so that horizontal momentum is carried into the jump
        rb.velocity = new Vector2(rb.velocity.x, 0);
        float jumpTimer = 0;

        while ((jumpButtonPressed || jumpTimer < minJumpTime) && jumpTimer < maxJumpTime)
        {
            float jumpPercent = jumpTimer / maxJumpTime;
            Vector2 thisFrameJumpVector = Vector2.Lerp(new Vector2 (0, jumpVector), Vector2.zero, jumpPercent);
            rb.AddForce(thisFrameJumpVector);
            jumpTimer += Time.deltaTime;
            yield return null;
        }

        jumping = false;
    }

    //Set animator, disable colliders, invoke game over screen
    public void StartDeath(){
        EventManager.TriggerEvent("PlayerDeath");
        playerDead = true;
        characterAnimator.SetBool("Hit", true);
        characterAnimator.SetBool("Jump", false);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        ScoreKeeper.DisplayScore = 0;
        Invoke("DisplayGameOverScreen", deathDelay);
    }

    private void DisplayGameOverScreen()
    {
        Game.incDeaths();
        Game.addLoot(ScoreKeeper.Score);
        Game.addCarsCleared(ScoreKeeper.CarsCompleted);
    }

    //Expand Hurt function to account for invincibility / update UI
    public override void Hurt(int damage, GameObject dmgObj)
    {
        base.Hurt(damage, dmgObj);
        if (!invincibility)
        {
            invincibility = true;
            AdjustHealthUI(currentHealth);
            Invoke("InvincibiltyReset", invincibilityCooldown);
        }
    }

    void InvincibiltyReset() { invincibility = false; }

    //Activates / deactivates UI hearts based on current health
    private void AdjustHealthUI(float health)
	{
		heart1.SetActive (health >= 10f);
		heart2.SetActive (health >= 20f);
		heart3.SetActive (health >= 30f);
	}
}