using UnityEngine;

// TODO: Instead of player input - AI script
public class PawnEnemy<PlayerInput>: LandEnemy<PlayerInput>
{
    #region Fields and Properties
    [SerializeField] private Transform edgeCheckerTransform;
    [SerializeField] private Transform groundCheckerTransform;
    [SerializeField] private Collider2D physicsCollider;
    private float groundCheckerRadius;
    private float edgeCheckDistance;
    private float obstacleCheckSizeDelta;
    private float obstacleCheckDistance;
    private LayerMask whatIsGround;
    private float hitPoints;

    
    protected  override Transform EdgeCheckerTransform { get => edgeCheckerTransform; }
    protected  override Transform GroundCheckerTransform { get => groundCheckerTransform; }
    protected  override float GroundCheckerRadius { get => groundCheckerRadius; }
    protected  override float EdgeCheckDistance { get => edgeCheckDistance; }
    protected  override float ObstacleCheckSizeDelta { get => obstacleCheckSizeDelta; }
    protected  override float ObstacleCheckDistance { get => obstacleCheckDistance; }
    protected  override LayerMask WhatIsGround { get => whatIsGround; } 
    protected  override Collider2D PhysicsCollider { get => physicsCollider; } 
    public override float HP { get => hitPoints; set => hitPoints = value; }
    public override Transform Attacker { get; set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region Initialization from manager
        groundCheckerRadius = PawnEnemyManager.Instance.GroundCheckerRadius;
        edgeCheckDistance = PawnEnemyManager.Instance.EdgeCheckDistance;
        obstacleCheckSizeDelta = PawnEnemyManager.Instance.ObstacleCheckSizeDelta;
        obstacleCheckDistance = PawnEnemyManager.Instance.ObstacleCheckDistance;
        whatIsGround = PawnEnemyManager.Instance.WhatIsGround;
        hitPoints = PawnEnemyManager.Instance.HitPoints;
        #endregion
    }

    protected override void OnTakeDamage()
    {

    }

    public override void Die()
    {

    }

    public override void Move(float move, bool crouch)
    {

    }

    public override void Attack(int attackNum)
    {
        
    }
}