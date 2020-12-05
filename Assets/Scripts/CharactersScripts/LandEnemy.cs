using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LandEnemy<T>: CharacterController<T>, IDamageable // T is input system - specific AI for each enemy type
{
    protected bool facingRight = true;
    protected bool grounded = false;

    #region abstract properties
    protected abstract Transform EdgeCheckerTransform { get; }
    protected abstract Transform GroundCheckerTransform { get; }
    protected abstract float GroundCheckerRadius { get; }
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

        facingRight = transform.localScale.x >= 0 ? true : false;
    }


    public void TakeDamage(float damage)
    {
        HP -= damage;
        OnTakeDamage();

        if (HP <= 0) Die();
    }

    // Method that could be called before instance dies
    protected abstract void OnTakeDamage();
    internal abstract void Jump(float force);

    public abstract void Die();

    // getter to check if character is grounded
    internal bool isGrounded
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
    
    // getter to check if character on edge of platform
    internal bool isOnEdge
    {
        get
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(EdgeCheckerTransform.position, Vector2.down, EdgeCheckDistance, WhatIsGround);
            if (!groundInfo.collider) return true;

            return false;
        }
    }

    // getter to check if character in front of Obstacle
    internal bool isFacingObstacle
    {
        get
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                PhysicsCollider.bounds.center, 
                new Vector2(PhysicsCollider.bounds.size.x, PhysicsCollider.bounds.size.y - ObstacleCheckSizeDelta),
                0f,
                facingRight ? Vector2.right : Vector2.left,
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
			facingTarget = facingRight;
		} else {
			facingTarget = !facingRight;
		}
		if(!facingTarget) Flip();
    }

    // Method to change facing direction
    protected void Flip()
	{
		// Switch the way the character is labelled as facing.
		facingRight = !facingRight;

		// Multiply the character's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}