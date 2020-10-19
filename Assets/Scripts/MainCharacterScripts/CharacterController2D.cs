using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
	private float m_FallMultiplyer;
	private float m_LowJumpMultiplyer;
	private float m_JumpForce;							// Amount of force added when the player jumps.
	private float m_AirJumpForce;
	private float m_CrouchSpeed;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	private float m_MovementSmoothing;	// How much to smooth out the movement
	private bool m_AirControl;							// Whether or not a player can steer while jumping;
	internal bool m_AllowMove;
	private bool m_AirJump;
	protected float m_LandingDistance;
	private float m_GravityScale; 												// gravity multiplyer on Rb2D
	private float k_GroundedRadius = .07f; 										// Radius of the overlap circle to determine if grounded
	private float k_CeilingRadius = .2f; 										// Radius of the overlap circle to determine if the player can stand up
	[SerializeField] protected LayerMask m_WhatIsGround;						// A mask determining what is ground to the character
	[SerializeField] private ContactFilter2D m_WhatIsPlatform;
	[SerializeField] internal Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] internal Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] GameObject[] attackEffectPrefabs = {};
	private bool m_wasCrouching = false;
	public bool m_Grounded;            											// Whether or not the player is grounded.
	protected Rigidbody2D m_Rigidbody2D;
	private Vector2 m_Velocity = Vector2.zero;
	private bool m_AirJumped;
	private Collider2D[] colliders = new Collider2D[1];
	private float m_HangTime; 													// Koyote time
	private float hangCounter;
	private PlayerInput playerInput;
	private Animator m_anim;

	private Dictionary<int, Vector3> attackPoints = new Dictionary<int, Vector3>()
	{
		{0, new Vector3(0.6f, 0, 0)},				// SwordForward
		{1, new Vector3(0.25f, 1, 0)},				// SwordUp
		{2, new Vector3(-0.07f, -0.6f, 0)}			// SwordDown
	};



	protected virtual void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();
		m_anim = GetComponent<Animator>();

		m_LandingDistance = PlayerManager.Instance.landingDistance;
		m_HangTime = PlayerManager.Instance.hangTime;
		m_GravityScale = PlayerManager.Instance.gravityScale;
		m_FallMultiplyer = PlayerManager.Instance.fallMultiplyer;
		m_LowJumpMultiplyer = PlayerManager.Instance.lowJumpMultiplyer;
		m_JumpForce = PlayerManager.Instance.jumpForce;
		m_AirJumpForce = PlayerManager.Instance.airJumpForce;
		m_CrouchSpeed = PlayerManager.Instance.crouchSpeed;
		m_MovementSmoothing = PlayerManager.Instance.movementSmoothing;
		m_AirControl = PlayerManager.Instance.airControl;
		m_AllowMove = PlayerManager.Instance.allowMove;
		m_AirJump = PlayerManager.Instance.airJump;
		k_GroundedRadius = PlayerManager.Instance.groundedRadius;
		k_CeilingRadius = PlayerManager.Instance.ceilingRadius;
	}

	protected virtual void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
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
		if (m_Rigidbody2D.velocity.y < 0) {
			m_Rigidbody2D.gravityScale = m_FallMultiplyer;
		} else if (m_Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump")) {
			m_Rigidbody2D.gravityScale = m_LowJumpMultiplyer;
		} else {
			m_Rigidbody2D.gravityScale = m_GravityScale;
		}
		#endregion
	}

	public void Move(float move, bool crouch)
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

		//only control the player if grounded or airControl is turned on
		if ((m_Grounded || m_AirControl) && PlayerManager.Instance.allowMove)
		{

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
			Vector2 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
	}

	// TODO: Think about multi-weapon attack => dealing damage and so on
	public void Attack(int attackNum)
	{
		GameObject go = Instantiate(attackEffectPrefabs[0], transform.TransformPoint(attackPoints[attackNum]), gameObject.transform.rotation, gameObject.transform) as GameObject;
		// GameObject go = Instantiate(attackEffectPrefabs[0], attackPoint) as GameObject;
		IAttacker attackScript = go.GetComponent<IAttacker>();
		if (attackScript != null) {
			attackScript.InitAttack(new object[] {attackNum, PlayerManager.Instance.facingRight});
			m_anim.ResetTrigger("attacked");
			m_anim.SetTrigger("attacked");
			m_anim.SetInteger("attackNum", attackNum);
			attackScript.PerformAttack();
		}
	}

	public void Jump()
	{
		//Jump according koyote time
		if (hangCounter >= 0)
		{
			m_Rigidbody2D.velocity = Vector2.zero;
			// Add a vertical force to the player.
			// m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			playerInput.jumpBufferCounter = 0;
			GameEvents.Instance.PlayerJump();
		} else if (m_AirJump && !m_AirJumped) {
			m_Rigidbody2D.velocity = Vector2.zero;
			m_AirJumped = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_AirJumpForce));
			playerInput.jumpBufferCounter = 0;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
		Gizmos.DrawWireSphere(m_CeilingCheck.position, k_CeilingRadius);
	}
}