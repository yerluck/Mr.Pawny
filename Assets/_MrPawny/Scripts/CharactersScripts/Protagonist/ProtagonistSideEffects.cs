using UnityEngine;

internal class ProtagonistSideEffects : Protagonist, IDamageable
{
    [SerializeField]
    private ParticleSystem stepParticles;
    [SerializeField]
    private GameObject jumpDustPrefab;
    private float _hitPoints;
    private Transform _attackerTransform;
    private Animator _animator;
    private RaycastHit2D _landingCheckHit;
    private ParticleSystem.ShapeModule _stepParticlesShapeModule;
    private bool _isLanded;
    private Vector3 SHAPE_ROTATION = new Vector3(0, 270, 0);
    public float HP { get => _hitPoints; set => _hitPoints = value; }
    public Transform Attacker { get => _attackerTransform; set => _attackerTransform = value; }
    

    protected override void Start() {
        base.Start();
        _hitPoints = PlayerManager.Instance.hitPoints;
		_animator = GetComponent<Animator>();
        _isLanded = !isGrounded;

        // TODO: mb that event should be on player manager
        GameEvents.Instance.onPlayerFlip += CharacterFlipAction;
        if (jumpDustPrefab != null)
        {
            GameEvents.Instance.onPlayerJump += EmitJumpDust;
        }

        if (stepParticles != null) 
        {
            _stepParticlesShapeModule = stepParticles.shape;
            if (transform.localScale.x >= 0) _stepParticlesShapeModule.rotation = SHAPE_ROTATION;
            else _stepParticlesShapeModule.rotation = -1 * SHAPE_ROTATION;
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (isGrounded) 
        {
            _animator.SetBool("isGrounded", true);
        } else
        {
            _animator.SetBool("isGrounded", false);
        }

        //Handle landing animations, TODO: still need tests
        if (GetComponent<Rigidbody2D>().velocity.y < -0.01) {
			_landingCheckHit = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, landingCheckDistance, whatIsGround);
			if (_landingCheckHit.collider != null && !_isLanded) {
                _animator.SetBool("isLanding", true);
                _isLanded = true;
            }
		} else {
            _animator.SetBool("isLanding", false);
            _isLanded = false;
        }

        _animator.SetFloat("xVelocity", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		_animator.SetFloat("yVelocity", GetComponent<Rigidbody2D>().velocity.y);
    }

    public void EmitStepDust() {
        stepParticles.Play();
    }

    // TODO: add into pool
    private void EmitJumpDust()
    {
        Instantiate(jumpDustPrefab, groundCheckTransform.position , Quaternion.Euler(PlayerManager.Instance.JUMP_DUST_ROTATION));
    }

    // TODO: mb that should be separated onto fight system
    public void TakeDamage(float dmg){
        HP -= dmg;

        if(HP <= 0) {
            Die();
        }
	}

    //TODO: add actual death action
    public void Die()
    {
        Debug.Log("DEAD!"); 
    }

    private void CharacterFlipAction()
    {
        if (stepParticles != null)
        {
            _stepParticlesShapeModule.rotation *= -1;
        }
    }

    private void OnDestroy() {
        if(GameEvents.Instance != null)
        {
            GameEvents.Instance.onPlayerFlip -= CharacterFlipAction;        
            GameEvents.Instance.onPlayerJump -= EmitJumpDust;
        }
    }
}
