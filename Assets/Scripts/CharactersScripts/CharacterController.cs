using UnityEngine;

public abstract class CharacterController<T> : MonoBehaviour
{
	[SerializeField] protected T inputSource;
	protected Rigidbody2D rigidbody2D;
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
	

    protected virtual void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		if(inputSource == null) inputSource = GetComponent<T>();
	}

    public abstract void Move(float move, bool crouch);


	public abstract void Attack(int attackNum);
}
