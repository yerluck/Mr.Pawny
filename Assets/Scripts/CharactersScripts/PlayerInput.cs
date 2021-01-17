using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Protagonist movementController;
    private float runSpeed;
    private float airSpeed;
    private float jumpBufferTime;
    [HideInInspector] public float jumpBufferCounter;
    private float horizontalMove = 0f;
    private bool crouch = false;
    // public bool PlayerManager.Instance.facingRight;
    private Animator anim;
    private float jumpCooldownTime;
    private float jumpCooldown;
    private float attackCooldownTime;
    private float attackCooldown;

    // TODO: add new attacks accordingly
    private enum Attacks
    {
        SwordForward,
        SwordUp,
        SwordDown
    }


    private void Awake() {
        if (movementController == null){
            movementController = GetComponent<Protagonist>();
        }
        anim = GetComponent<Animator>();

        #region Initialization
        PlayerManager.Instance.facingRight = transform.localScale.x >= 0 ? true : false;
        jumpCooldownTime = PlayerManager.Instance.hangTime; // TODO: test this out, mb just chanhge at ManagerInstance
        attackCooldownTime = PlayerManager.Instance.attackTimeCooldown;
        jumpBufferTime = PlayerManager.Instance.jumpBufferTime;
        runSpeed = PlayerManager.Instance.runSpeed;
        airSpeed = PlayerManager.Instance.airSpeed;
        #endregion
        jumpCooldown = jumpCooldownTime;
        attackCooldown = attackCooldownTime;
    }

    void Update()
    {
        // if (PlayerManager.Instance.paralized == true)
        // {
        //     horizontalMove = 0f;
        //     return;
        // }

        var speed = movementController.m_Grounded ? runSpeed : airSpeed;
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if(horizontalMove != 0) {
            anim.SetBool("isMoveInput", true);
        } else {
            anim.SetBool("isMoveInput", false);
        };

        if(horizontalMove > 0 && !PlayerManager.Instance.facingRight){
            Flip();
        }
        if(horizontalMove < 0 && PlayerManager.Instance.facingRight){
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
        if (Input.GetMouseButtonDown(0) && attackCooldown <= 0) 
        {
            attackCooldown = attackCooldownTime;
            if (Input.GetAxisRaw("Vertical") < 0 && !movementController.m_Grounded)
            {
                movementController.Attack((int)Attacks.SwordDown);
                return;
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                movementController.Attack((int)Attacks.SwordUp);
                return;
            }

            movementController.Attack((int)Attacks.SwordForward);
        }
        else {
            attackCooldown -= Time.deltaTime; 
        };
    }

    void FixedUpdate() 
    {
        movementController.Move(horizontalMove * Time.fixedDeltaTime, crouch);
        jumpCooldown -= Time.fixedDeltaTime;
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		PlayerManager.Instance.facingRight = !PlayerManager.Instance.facingRight;

		// Multiply the player's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

        GameEvents.Instance.PlayerFlip();
	}
}
