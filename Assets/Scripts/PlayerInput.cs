using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterController2D movementController;
    private float runSpeed;
    private float airSpeed;
    private float jumpBufferTime;
    public float jumpBufferCounter;
    private float horizontalMove = 0f;
    private bool crouch = false;
    private bool facingRight;
    private Animator anim;
    [SerializeField] private ParticleSystem stepParticles;
    private ParticleSystem.ShapeModule shape;
    private float jumpCooldownTime;
    private float jumpCooldown;

    // TODO: add new attacks accordingly
    private enum Attacks
    {
        SwordForward,
        SwordUp,
        SwordDown
    }


    private void Awake() {
        if (movementController == null){
            movementController = GetComponent<CharacterController2D>();
        }
        anim = GetComponent<Animator>();
        shape = stepParticles.shape;
        facingRight = transform.localScale.x >= 0 ? true : false;

        #region Initialization
        jumpCooldownTime = PlayerManager.Instance.hangTime + 0.05f;
        jumpBufferTime = PlayerManager.Instance.jumpBufferTime;
        runSpeed = PlayerManager.Instance.runSpeed;
        airSpeed = PlayerManager.Instance.airSpeed;
        #endregion

        jumpCooldown = jumpCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        var speed = movementController.m_Grounded ? runSpeed : airSpeed;
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if(horizontalMove != 0) {
            anim.SetBool("isMoveInput", true);
        } else {
            anim.SetBool("isMoveInput", false);
        };

        if(horizontalMove > 0 && !facingRight){
            Flip();
        }
        if(horizontalMove < 0 && facingRight){
            Flip();
        }

        //handle jump buffer
        if (Input.GetButtonDown("Jump") && jumpBufferCounter < 0 && jumpCooldown <= 0){
            jumpCooldown = jumpCooldownTime;
            jumpBufferCounter = jumpBufferTime;
            anim.SetTrigger("jumpPressed");
        } else {
            jumpBufferCounter -= Time.deltaTime; 
        };

        // handle jump
        if (jumpBufferCounter > 0) {
            movementController.Jump();
        }

        if (Input.GetButtonDown("Crouch")){
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }

        //TODO: Find Better Way for attacks
        if (Input.GetMouseButtonDown(0)) 
        {
            if (Input.GetAxisRaw("Vertical") < 0 && !movementController.m_Grounded)
            {
                movementController.Attack((int)Attacks.SwordDown);
                return;
            }
            movementController.Attack((int)Attacks.SwordForward);
        }

    }

    void FixedUpdate() 
    {
        movementController.Move(horizontalMove * Time.fixedDeltaTime, crouch);
        jumpCooldown -= Time.fixedDeltaTime;
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
        shape.rotation *= -1;

		// Multiply the player's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
