using UnityEngine;
using Pawny.StateMachine;

// TODO: Instead of player input - AI script
public class PawnEnemy: LandEnemy<StateMachine>
{
#region Fields and Properties
    [SerializeField]
    public  PawnEnemyStatsSO statsSO;
    [SerializeField]
    private Transform   edgeCheckTransform;
    [SerializeField]
    private Transform   groundCheckTransform;
    [SerializeField]
    private Collider2D  physicsCollider;
    [SerializeField]
    private LayerMask   whatIsGround;
    private float       jumpGravityScale;
    private float       fallGravityScale;
    private float       groundCheckRadius;
    private float       edgeCheckDistance;
    private float       obstacleCheckSizeDelta;
    private float       obstacleCheckDistance;
    private float       movementSmoothing;
    private float       jumpForce;
    private float       runSpeed;
    private float       airMovementSpeed;
    private float       speedSlowFactor;
    private Vector2     velocity = Vector2.zero;


    protected   override float      JumpGravityScale { get => jumpGravityScale; }
    protected   override float      FallGravityScale { get => fallGravityScale; }
    protected   override float      GroundCheckRadius { get => groundCheckRadius; }
    protected   override float      EdgeCheckDistance { get => edgeCheckDistance; }
    protected   override float      ObstacleCheckSizeDelta { get => obstacleCheckSizeDelta; }
    protected   override float      ObstacleCheckDistance { get => obstacleCheckDistance; }
    protected   override Transform  EdgeCheckTransform { get => edgeCheckTransform; }
    protected   override Transform  GroundCheckTransform { get => groundCheckTransform; }
    protected   override Collider2D PhysicsCollider { get => physicsCollider; } 
    protected   override LayerMask  WhatIsGround { get => whatIsGround; } 
#endregion

    protected override void Start()
    {
        base.Start();

        #region Initialization from manager
        jumpGravityScale        = statsSO.JumpGravityScale;
        fallGravityScale        = statsSO.FallGravityScale;
        groundCheckRadius       = statsSO.GroundCheckRadius;
        edgeCheckDistance       = statsSO.EdgeCheckDistance;
        obstacleCheckSizeDelta  = statsSO.ObstacleCheckSizeDelta;
        obstacleCheckDistance   = statsSO.ObstacleCheckDistance;
        whatIsGround            = statsSO.WhatIsGround;
        movementSmoothing       = statsSO.MovementSmoothing;
        jumpForce               = statsSO.JumpForce;
        runSpeed                = statsSO.RunSpeed;
        airMovementSpeed        = statsSO.AirMovementSpeed;
        speedSlowFactor         = statsSO.SpeedSlowFactor;
        #endregion
    }

    internal override void Jump()
    {
        if( IsGrounded )
        {
            rigidBody2D.velocity = Vector2.zero;
			rigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    public override void Move(Vector2 move, bool crouch)
    {
        if (move.x > 0 && !isFacingRight) Flip();
        if (move.x < 0 && isFacingRight) Flip();

		//only control the character if grounded or airControl is turned on
		if (allowMove)
		{
            float speed = IsGrounded? runSpeed : airMovementSpeed;
            speed *= crouch ? speedSlowFactor : 1f;
			// Move the character by finding the target velocity

			Vector2 targetVelocity = new Vector2(move.x * speed * Time.deltaTime, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector2.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);
		}
    }

    public override void Attack(int attackNum)
    {

    }
}