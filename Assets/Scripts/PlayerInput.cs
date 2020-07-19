using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterController2D movementController;
    private float runSpeed;
    private float airSpeed;
    private float horizontalMove = 0f;
    private bool crouch = false;
    private bool facingRight = true;

    private void Awake() {
        if (movementController == null){
            movementController = GetComponent<CharacterController2D>();
        }

        #region Initialization
        runSpeed = PlayerManager.Instance.runSpeed;
        airSpeed = PlayerManager.Instance.airSpeed;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        var speed = movementController.m_Grounded ? runSpeed : airSpeed;
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if(horizontalMove > 0 && !facingRight){
            Flip();
        }
        if(horizontalMove < 0 && facingRight){
            Flip();
        }

        if (Input.GetButtonDown("Jump")){
            movementController.Jump();
        };
        if (Input.GetButtonDown("Crouch")){
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }

    }

    void FixedUpdate() 
    {

        movementController.Move(horizontalMove * Time.fixedDeltaTime, crouch);

    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
