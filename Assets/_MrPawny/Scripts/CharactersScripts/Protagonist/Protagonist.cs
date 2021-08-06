using UnityEngine;
using System.Collections.Generic;

internal class Protagonist : CharacterController<PlayerInput>
{
	public 		PlayerStatsSO 	playerStats;
	public 		bool 			isGrounded;            											// Whether or not the player is grounded.
	protected 	float 			landingCheckDistance;											// Distance when to play landing animation
	[SerializeField]
	protected 	LayerMask 		whatIsGround;													// A mask determining what is ground to the character
	[SerializeField]
	protected 	Transform 		groundCheckTransform;											// A position marking where to check if the player is grounded.
	private 	float 			fallGravityScale;
	private 	float 			lowJumpGravityScale;
	private 	float 			jumpForce;														// Amount of force added when the player jumps.
	private 	float 			airJumpForce;
	private 	float 			crouchSpeed;													// Amount of maxSpeed applied to crouching movement. 1 = 100%
	private 	float 			movementSmoothing;												// How much to smooth out the movement
	private 	float 			runSpeed;
	private 	float 			airMovementSpeed;
	private 	float 			jumpGravityScale; 												// gravity multiplyer on Rb2D
	private 	float 			groundedCheckRadius; 											// Radius of the overlap circle to determine if grounded
	private 	float 			ceilingCheckRadius; 											// Radius of the overlap circle to determine if the player can stand up
	private 	float 			koyoteJumpTime; 												// Koyote time
	private 	bool 			isAirMovementAllowed;											// Whether or not a player can steer while jumping;
	private 	bool 			isAirJumpAllowed;
	private 	bool 			airJumped;
	[SerializeField]
	private 	ContactFilter2D whatIsPlatform;
	[SerializeField]
	private 	Transform 		ceilingCheckTransform;											// A position marking where to check for ceilings
	[SerializeField]
	private 	GameObject[] 	attackEffectPrefabs = {};
	private 	Vector2 		velocity = Vector2.zero;
	private 	Collider2D[] 	_colliders = new Collider2D[1];
	private 	bool 			_wasCrouching = false;
	private 	float 			_koyoteJumpBuffer;

	// TODO: mb that should be separated onto fight system
	protected Dictionary<int, Vector3> attackPoints = new Dictionary<int, Vector3>()
	{
		{0, new Vector3(0.6f, 0, 0)},				// SwordForward
		{1, new Vector3(0.25f, 1, 0)},				// SwordUp
		{2, new Vector3(-0.07f, -0.6f, 0)}			// SwordDown
	};



	protected override void Start()
	{
		base.Start();

		landingCheckDistance	= playerStats.LandingCheckDistance;
		koyoteJumpTime 			= playerStats.KoyoteJumpTime;
		jumpGravityScale 		= playerStats.JumpGravityScale;
		fallGravityScale 		= playerStats.FallGravityScale;
		lowJumpGravityScale 	= playerStats.LowJumpGravityScale;
		jumpForce 				= playerStats.JumpForce;
		airJumpForce 			= playerStats.AirJumpForce;
		crouchSpeed 			= playerStats.CrouchSpeed;
		movementSmoothing 		= playerStats.MovementSmoothing;
		isAirMovementAllowed 	= playerStats.IsAirMovementAllowed;
		isAirJumpAllowed 		= playerStats.IsAirJumpAllowed;
		runSpeed				= playerStats.RunSpeed;
		airMovementSpeed		= playerStats.AirMovementSpeed;
		groundedCheckRadius 	= playerStats.GroundedCheckRadius;
		ceilingCheckRadius 		= playerStats.CeilingCheckRadius;
	}

	protected virtual void FixedUpdate()
	{
		isGrounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundedCheckRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				airJumped = false;
			}
		}

		//Koyote time setting
		if (isGrounded) 
		{
			_koyoteJumpBuffer = koyoteJumpTime;
		} else
		{
			_koyoteJumpBuffer -= Time.deltaTime;
		}

		#region Controversial // Better Jump Effect
		if (rigidBody2D.velocity.y < 0) {
			rigidBody2D.gravityScale = fallGravityScale;
		} else if (rigidBody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
			rigidBody2D.gravityScale = lowJumpGravityScale;
		} else {
			rigidBody2D.gravityScale = jumpGravityScale;
		}
		#endregion
	}

	public override void Move(Vector2 move, bool crouch)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch && isGrounded)
		{

			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(ceilingCheckTransform.position, ceilingCheckRadius, whatIsPlatform, _colliders) > 0)
			{
				if(!_colliders[0].CompareTag("Passform")){
					crouch = true;
				}
			}
		}

		float speed = isGrounded ? runSpeed : airMovementSpeed;

		//only control the player if grounded or airControl is turned on
		if ((isGrounded || isAirMovementAllowed) && PlayerManager.Instance.allowMove)
		{
			move *= speed * Time.fixedDeltaTime;

			// If crouching
			if (crouch && isGrounded)
			{
				if (!_wasCrouching)
				{
					_wasCrouching = true;
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= crouchSpeed;

				// Disable one of the colliders when crouching
				//TODO: add crouching effect = reduce collider height
			} else
			{
				// Enable the collider when not crouching
				//TODO: add crouching effect = reduce collider height
				if (_wasCrouching)
				{
					_wasCrouching = false;
				}
			}

			// Move the character by finding the target velocity
			Vector2 targetVelocity = new Vector2(move.x, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector2.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
		}
	}

	// TODO: Think about multi-weapon attack => dealing damage and so on
	public override void Attack(int attackNum)
	{
		GameObject go = Instantiate(attackEffectPrefabs[0], transform.TransformPoint(attackPoints[attackNum]), gameObject.transform.rotation, gameObject.transform) as GameObject;
		// GameObject go = Instantiate(attackEffectPrefabs[0], attackPoint) as GameObject;
		IAttacker attackScript = go.GetComponent<IAttacker>();
		if (attackScript != null) {
			attackScript.InitAttack(new object[] {attackNum, PlayerManager.Instance.facingRight});
			animator.ResetTrigger("attacked");
			animator.SetTrigger("attacked");
			animator.SetInteger("attackNum", attackNum);
			attackScript.PerformAttack();
		}
	}

	public void Jump()
	{
		//Jump according koyote time
		if (_koyoteJumpBuffer >= 0)
		{
			rigidBody2D.velocity = Vector2.zero;
			// Add a vertical force to the player.
			// m_Grounded = false;
			rigidBody2D.AddForce(new Vector2(0f, jumpForce));
			inputSource.jumpBufferCounter = 0;
			GameEvents.Instance.PlayerJump();
		} else if (isAirJumpAllowed && !airJumped) {
			rigidBody2D.velocity = Vector2.zero;
			airJumped = true;
			rigidBody2D.AddForce(new Vector2(0f, airJumpForce));
			inputSource.jumpBufferCounter = 0;
		}
	}

	protected void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheckTransform.position, groundedCheckRadius);
		Gizmos.DrawWireSphere(ceilingCheckTransform.position, ceilingCheckRadius);
	}
}