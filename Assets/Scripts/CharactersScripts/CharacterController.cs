using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
	protected Rigidbody2D rigidBody2D;
	protected Animator animator;
	// protected float m_LandingDistance;						// Distance when to play landing animation
	// protected float m_GravityScale; 						// gravity multiplyer on Rb2D
	// protected float m_FallMultiplyer;						// How fast object should fall
	// protected float m_JumpForce;							// Amount of force added when the player jumps.
	// protected float m_MovementSmoothing;					// How much to smooth out the movement
	// protected bool m_AirControl;							// Whether or not a player can steer while jumping;
	// internal bool m_AllowMove;								// May the object move
	// protected float k_GroundedRadius;		 				// Radius of the overlap circle to determine if grounded
	// protected float k_CeilingRadius; 						// Radius of the overlap circle to determine if the player can stand up
	
	public bool IsFacingRight
	{
		get => transform.localScale.x > 0;
	}

    protected virtual void Awake()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

    public abstract void Move(Vector2 move, bool crouch = false);


	public abstract void Attack(int attackNum);
}
