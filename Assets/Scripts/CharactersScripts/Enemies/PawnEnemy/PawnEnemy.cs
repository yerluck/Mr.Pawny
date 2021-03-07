﻿using UnityEngine;
using Pawny.StateMachine;

// TODO: Instead of player input - AI script
public class PawnEnemy: LandEnemy<StateMachine>
{
    #region Fields and Properties
    [SerializeField] private Transform edgeCheckerTransform;
    [SerializeField] private Transform groundCheckerTransform;
    [SerializeField] private Collider2D physicsCollider;
    [SerializeField] private LayerMask whatIsGround;
    private float gravityScale;
    private float fallMultiplyer;
    private float groundCheckerRadius;
    private float edgeCheckDistance;
    private float obstacleCheckSizeDelta;
    private float obstacleCheckDistance;
    private float hitPoints;
    private float movementSmoothing;
    private float jumpForce;
    private float runSpeed;
    private float airSpeed;
    private float speedSlowFactor;
    protected Vector2 velocity = Vector2.zero;

    
    protected override Transform EdgeCheckerTransform { get => edgeCheckerTransform; }
    protected override Transform GroundCheckerTransform { get => groundCheckerTransform; }
    protected override float GravityScale { get => gravityScale; }
    protected override float FallMultiplyer { get => fallMultiplyer; }
    protected override float GroundCheckerRadius { get => groundCheckerRadius; }
    protected override float EdgeCheckDistance { get => edgeCheckDistance; }
    protected override float ObstacleCheckSizeDelta { get => obstacleCheckSizeDelta; }
    protected override float ObstacleCheckDistance { get => obstacleCheckDistance; }
    protected override LayerMask WhatIsGround { get => whatIsGround; } 
    protected override Collider2D PhysicsCollider { get => physicsCollider; } 
    public override float HP { get => hitPoints; set => hitPoints = value; }
    public override Transform Attacker { get; set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region Initialization from manager
        gravityScale            = PawnEnemyManager.Instance.GravityScale;
        fallMultiplyer          = PawnEnemyManager.Instance.FallMultiplyer;
        groundCheckerRadius     = PawnEnemyManager.Instance.GroundCheckerRadius;
        edgeCheckDistance       = PawnEnemyManager.Instance.EdgeCheckDistance;
        obstacleCheckSizeDelta  = PawnEnemyManager.Instance.ObstacleCheckSizeDelta;
        obstacleCheckDistance   = PawnEnemyManager.Instance.ObstacleCheckDistance;
        whatIsGround            = PawnEnemyManager.Instance.WhatIsGround;
        hitPoints               = PawnEnemyManager.Instance.HitPoints;
        movementSmoothing       = PawnEnemyManager.Instance.MovementSmoothing;
        jumpForce               = PawnEnemyManager.Instance.JumpForce;
        runSpeed                = PawnEnemyManager.Instance.RunSpeed;
        airSpeed                = PawnEnemyManager.Instance.AirSpeed;
        speedSlowFactor         = PawnEnemyManager.Instance.SpeedSlowFactor;
        #endregion
    }

    protected override void OnTakeDamage()
    {

    }

    internal override void Jump()
    {
        if( IsGrounded )
        {
            rigidBody2D.velocity = Vector2.zero;
			rigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    // TODO: add actual actions (animations...)
    public override void Die()
    {
        base.Die();
    }

    public override void Move(Vector2 move, bool crouch)
    {
        if (move.x > 0 && !isFacingRight) Flip();
        if (move.x < 0 && isFacingRight) Flip();

		//only control the character if grounded or airControl is turned on
		if (AllowMove)
		{
            float speed = IsGrounded? runSpeed : airSpeed;
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