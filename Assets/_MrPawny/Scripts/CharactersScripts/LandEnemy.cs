using UnityEngine;

public abstract class LandEnemy<T>: CharacterController<T> // T is input system - specific AI for each enemy type
{
    public bool allowMove = true;
    protected bool isFacingRight = true;
    private bool _grounded = false;

    // In case if need to update get or  set - update accessors
    #region abstract properties
    protected abstract Transform EdgeCheckTransform { get; }
    protected abstract Transform GroundCheckTransform { get; }
    protected abstract float GroundCheckRadius { get; }
    protected abstract float JumpGravityScale { get; }
    protected abstract float FallGravityScale { get; }
    protected abstract float EdgeCheckDistance { get; }
    protected abstract float ObstacleCheckSizeDelta { get; }
    protected abstract float ObstacleCheckDistance { get; }
    protected abstract LayerMask WhatIsGround { get; } 
    protected abstract Collider2D PhysicsCollider { get; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        isFacingRight = transform.localScale.x >= 0;
    }

    protected virtual void FixedUpdate()
    {
        if (rigidBody2D.velocity.y < 0) {
			rigidBody2D.gravityScale = FallGravityScale;
		} else {
			rigidBody2D.gravityScale = JumpGravityScale;
		}
    }

    // Method that could be called before instance dies
    internal abstract void Jump();

    // getter to check if character is grounded
    internal bool IsGrounded
    {
        get
        {
            _grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckTransform.position, GroundCheckRadius, WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    _grounded = true;
                }
            }

            return _grounded;
        }
    }
    
    // getter to check if character is on the edge of platform
    internal bool IsOnEdge
    {
        get
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(EdgeCheckTransform.position, Vector2.down, EdgeCheckDistance, WhatIsGround);
            if (!groundInfo.collider) return true;

            return false;
        }
    }

    // getter to check if character is in the front of Obstacle
    internal bool IsFacingObstacle
    {
        get
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                PhysicsCollider.bounds.center, 
                new Vector2(PhysicsCollider.bounds.size.x, PhysicsCollider.bounds.size.y - ObstacleCheckSizeDelta),
                0f,
                isFacingRight ? Vector2.right : Vector2.left,
                ObstacleCheckDistance,
                WhatIsGround
		    );

            if(hit) return true;

            return false;
        }
    }

    // Method to turn character to face specific point in space
    internal void FaceTargetPoint(Vector2 targetPoint)
    {
        bool facingTarget;
		if(targetPoint.x - transform.position.x > 0) {
			facingTarget = isFacingRight;
		} else {
			facingTarget = !isFacingRight;
		}
		if(!facingTarget) Flip();
    }

    // Method to change facing direction
    protected void Flip()
	{
		// Switch the way the character is labelled as facing.
		isFacingRight = !isFacingRight;

		// Multiply the character's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}