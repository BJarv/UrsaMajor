using UnityEngine;
using System.Collections;

public class playerCharacter : baseCharacter
{

    public float plusJumpForce = 300f;
    private float currentJumpForce = 0f;
    public float airMoveSpeed = 0f;

    // Use this for initialization
    override protected void Start() {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        float moveH = Input.GetAxisRaw("Horizontal");
        Flip(moveH);
        Debug.Log(IsGrounded());
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
        } else {
            if (moveH > 0)
            {
                if (rb.velocity.x < moveSpeed)
                    rb.AddForce(new Vector2(moveH * moveSpeed, 0));
            }
            else if (moveH < 0)
            {
                if (rb.velocity.x > -moveSpeed)
                    rb.AddForce(new Vector2(moveH * moveSpeed, 0));
            }
        }

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
                    var timeDiff = Time.deltaTime * 100;
                    var forceToAdd = plusJumpForce * timeDiff;
                    currentJumpForce += forceToAdd;
                    rb.AddForce(new Vector2(0, forceToAdd));
                }
                else
                {
                    jumpState = JumpStates.FALLING;
                    currentJumpForce = 0;
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
    }

    // Use for actions on rigidbodies
    override protected void FixedUpdate()
    {

    }

}
