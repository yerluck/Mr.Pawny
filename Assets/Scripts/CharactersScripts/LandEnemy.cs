using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LandEnemy<T>: CharacterController<T> // T is input system - specific AI for each enemy type
{
    protected bool facingRight = true;
    protected bool grounded = false;

    #region abstract properties
    protected abstract Transform edgeCheckerTransform { get; }     
    protected abstract Transform groundCheckerTransform { get; }
    protected abstract float groundCheckerRadius { get; }
    protected abstract float edgeCheckDistance { get; }
    protected abstract float obstacleCheckSizeDelta { get; }
    protected abstract float obstacleCheckDistance { get; }
    protected abstract LayerMask whatIsGround { get; } 
    protected abstract Collider2D physicsCollider { get; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        facingRight = transform.localScale.x >= 0 ? true : false;
    }

    // getter to check if character is grounded
    protected bool isGrounded
    {
        get
        {
            grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckerTransform.position, groundCheckerRadius, whatIsGround);
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
    protected bool isOnEdge
    {
        get
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(edgeCheckerTransform.position, Vector2.down, edgeCheckDistance, whatIsGround);
            if (!groundInfo.collider) return true;

            return false;
        }
    }

    protected bool isFacingObstacle
    {
        get
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                physicsCollider.bounds.center, 
                new Vector2(physicsCollider.bounds.size.x, physicsCollider.bounds.size.y - obstacleCheckSizeDelta),
                0f,
                facingRight ? Vector2.right : Vector2.left,
                obstacleCheckDistance,
                whatIsGround
		    );

            if(hit) return true;

            return false;
        }
    }

    protected void FaceTarget(Transform target)
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
    private void Flip()
	{
		// Switch the way the character is labelled as facing.
		facingRight = !facingRight;

		// Multiply the character's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}