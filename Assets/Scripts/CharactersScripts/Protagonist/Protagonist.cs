using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

internal class Protagonist : CharacterController<PlayerInput>
{
	protected float m_FallMultiplyer;
	protected float m_LowJumpMultiplyer;
	protected float m_JumpForce;												// Amount of force added when the player jumps.
	protected float m_AirJumpForce;
	protected float m_CrouchSpeed;												// Amount of maxSpeed applied to crouching movement. 1 = 100%
	protected float m_MovementSmoothing;										// How much to smooth out the movement
	protected bool m_AirControl;												// Whether or not a player can steer while jumping;
	internal bool m_AllowMove;
	protected bool m_AirJump;
	private float m_RunSpeed;
	private float m_AirSpeed;
	protected float m_LandingDistance;											// Distance when to play landing animation
	protected float m_GravityScale; 											// gravity multiplyer on Rb2D
	protected float k_GroundedRadius = .07f; 									// Radius of the overlap circle to determine if grounded
	protected float k_CeilingRadius = .2f; 										// Radius of the overlap circle to determine if the player can stand up
	[SerializeField] protected LayerMask m_WhatIsGround;						// A mask determining what is ground to the character
	[SerializeField] protected ContactFilter2D m_WhatIsPlatform;
	[SerializeField] internal Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] internal Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] GameObject[] attackEffectPrefabs = {};
	protected bool m_wasCrouching = false;
	public bool m_Grounded;            											// Whether or not the player is grounded.
	protected Vector2 m_Velocity = Vector2.zero;
	protected bool m_AirJumped;
	protected Collider2D[] colliders = new Collider2D[1];
	protected float m_HangTime; 													// Koyote time
	protected float hangCounter;

	protected Dictionary<int, Vector3> attackPoints = new Dictionary<int, Vector3>()
	{
		{0, new Vector3(0.6f, 0, 0)},				// SwordForward
		{1, new Vector3(0.25f, 1, 0)},				// SwordUp
		{2, new Vector3(-0.07f, -0.6f, 0)}			// SwordDown
	};



	protected override void Awake()
	{
		base.Awake();

		m_LandingDistance 	= PlayerManager.Instance.landingDistance;
		m_HangTime 			= PlayerManager.Instance.hangTime;
		m_GravityScale 		= PlayerManager.Instance.gravityScale;
		m_FallMultiplyer 	= PlayerManager.Instance.fallMultiplyer;
		m_LowJumpMultiplyer = PlayerManager.Instance.lowJumpMultiplyer;
		m_JumpForce 		= PlayerManager.Instance.jumpForce;
		m_AirJumpForce 		= PlayerManager.Instance.airJumpForce;
		m_CrouchSpeed 		= PlayerManager.Instance.crouchSpeed;
		m_MovementSmoothing = PlayerManager.Instance.movementSmoothing;
		m_AirControl 		= PlayerManager.Instance.airControl;
		m_AllowMove 		= PlayerManager.Instance.allowMove;
		m_AirJump 			= PlayerManager.Instance.airJump;
		m_RunSpeed			= PlayerManager.Instance.runSpeed;
		m_AirSpeed			= PlayerManager.Instance.airSpeed;
		k_GroundedRadius 	= PlayerManager.Instance.groundedRadius;
		k_CeilingRadius 	= PlayerManager.Instance.ceilingRadius;
	}

	protected virtual void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				m_AirJumped = false;
			}
		}

		//Koyote time setting
		if (m_Grounded) 
		{
			hangCounter = m_HangTime;
		} else
		{
			hangCounter -= Time.deltaTime;
		}

		#region Controversial // Better Jump Effect
		if (rigidBody2D.velocity.y < 0) {
			rigidBody2D.gravityScale = m_FallMultiplyer;
		} else if (rigidBody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
			rigidBody2D.gravityScale = m_LowJumpMultiplyer;
		} else {
			rigidBody2D.gravityScale = m_GravityScale;
		}
		#endregion
	}

	public override void Move(Vector2 move, bool crouch)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch && m_Grounded)
		{

			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsPlatform, colliders) > 0)
			{
				if(!colliders[0].CompareTag("Passform")){
					crouch = true;
				}
			}
		}

		float speed = m_Grounded ? m_RunSpeed : m_AirSpeed;

		//only control the player if grounded or airControl is turned on
		if ((m_Grounded || m_AirControl) && PlayerManager.Instance.allowMove)
		{
			move *= speed * Time.fixedDeltaTime;

			// If crouching
			if (crouch && m_Grounded)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				//TODO: add crouching effect = reduce collider height
			} else
			{
				// Enable the collider when not crouching
				//TODO: add crouching effect = reduce collider height
				if (m_wasCrouching)
				{
					m_wasCrouching = false;
				}
			}

			// Move the character by finding the target velocity
			Vector2 targetVelocity = new Vector2(move.x, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector2.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
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
		if (hangCounter >= 0)
		{
			rigidBody2D.velocity = Vector2.zero;
			// Add a vertical force to the player.
			// m_Grounded = false;
			rigidBody2D.AddForce(new Vector2(0f, m_JumpForce));
			inputSource.jumpBufferCounter = 0;
			GameEvents.Instance.PlayerJump();
		} else if (m_AirJump && !m_AirJumped) {
			rigidBody2D.velocity = Vector2.zero;
			m_AirJumped = true;
			rigidBody2D.AddForce(new Vector2(0f, m_AirJumpForce));
			inputSource.jumpBufferCounter = 0;
		}
	}

	protected void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
		Gizmos.DrawWireSphere(m_CeilingCheck.position, k_CeilingRadius);
	}
}