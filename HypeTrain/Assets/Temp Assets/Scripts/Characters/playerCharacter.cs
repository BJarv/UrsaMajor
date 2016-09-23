using UnityEngine;
using System.Collections;

public class playerCharacter : baseCharacter
{
    //Jump force variables
    public float plusJumpForce = 300f; 
    public float airMoveSpeed = 50f;
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

    private Popup popupManager;

    // Use this for initialization
    override protected void Start() {
        base.Start();
        popupManager = GameObject.Find("Main Camera").GetComponent<Popup>();
        currentHealth = maxHealth;
        alreadyDying = false;
    }

    // Update is called once per frame
    override protected void Update()
    {
        //Grounded movement
        moveH = Input.GetAxisRaw("Horizontal");
        Flip(moveH);
        if (IsGrounded()) {
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

        //Jumpstate handler
        switch (jumpState) {
            case JumpStates.GROUNDED:
                if ((Input.GetKey(KeyCode.Space) || Input.GetAxis("LTrig") > 0.1) && IsGrounded())
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
        if (applyJumpForce) {
            float timeDiff = Time.deltaTime * 100;
            forceToAdd = plusJumpForce * timeDiff;
            currentJumpForce += forceToAdd;
            rb.AddForce(new Vector2(0, forceToAdd));
        }
        if(!IsGrounded())
        {
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
            if(Input.GetAxisRaw("Vertical") == -1) {
                Debug.Log("Fastfall!");
                //if (rb.velocity.y > maxFallSpeed) {
                    rb.AddForce(new Vector2(0, -200));
                //}
                
            }

        }
    }

    //Set animator, disable colliders, invoke game over screen
    public void StartDeath(){
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
        
        popupManager.dead = true;
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
            Invoke("invincibiltyReset", invincibilityCooldown);
        }
    }

    void invincibiltyReset() { invincibility = false; }

    //Activates / deactivates UI hearts based on current health
    private void AdjustHealthUI(float health)
	{
		heart1.SetActive (health >= 10f);
		heart2.SetActive (health >= 20f);
		heart3.SetActive (health >= 30f);
	}
}