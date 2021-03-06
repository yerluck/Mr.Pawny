using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LandEnemy<T>: CharacterController<T>, IDamageable // T is input system - specific AI for each enemy type
{
    protected bool isFacingRight = true;
    protected bool grounded = false;

    // In case if need to update get or  set - update accessors
    #region abstract properties
    protected abstract Transform EdgeCheckerTransform { get; }
    protected abstract Transform GroundCheckerTransform { get; }
    protected abstract float GroundCheckerRadius { get; }
    protected abstract float GravityScale { get; }
    protected abstract float FallMultiplyer { get; }
    protected abstract float EdgeCheckDistance { get; }
    protected abstract float ObstacleCheckSizeDelta { get; }
    protected abstract float ObstacleCheckDistance { get; }
    protected abstract LayerMask WhatIsGround { get; } 
    protected abstract Collider2D PhysicsCollider { get; }
    public abstract float HP { get; set; }
    public abstract Transform Attacker { get; set; }
    public bool AllowMove { get; set; } = true;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        isFacingRight = transform.localScale.x >= 0;
    }

    protected virtual void FixedUpdate()
    {
        if (rigidBody2D.velocity.y < 0) {
			rigidBody2D.gravityScale = FallMultiplyer;
		} else {
			rigidBody2D.gravityScale = GravityScale;
		}
    }


    public void TakeDamage(float damage)
    {
        HP -= damage;
        OnTakeDamage();

        if (HP <= 0)
        {
            Die();
        }
    }

    // Method that could be called before instance dies
    protected abstract void OnTakeDamage();
    internal abstract void Jump();

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    // getter to check if character is grounded
    internal bool IsGrounded
    {
        get
        {
            grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckerTransform.position, GroundCheckerRadius, WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                }
            }

            return grounded;
        }
    }
    
    // getter to check if character is on the edge of platform
    internal bool IsOnEdge
    {
        get
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(EdgeCheckerTransform.position, Vector2.down, EdgeCheckDistance, WhatIsGround);
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

    // Method to turn character to face player
    internal void FaceTarget(Transform target)
    {
        bool facingTarget;
		if(target.position.x - transform.position.x > 0) {
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