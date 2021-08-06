using UnityEngine;
public class PawnEnemyManager: Singleton<PawnEnemyManager>, IEnemyCharacterManager
{
    [SerializeField][Range(0, 1f)]  private float   groundCheckerRadius     = .1f;
    [SerializeField][Range(0, 1f)]  private float   edgeCheckDistance       = .5f;
    [SerializeField][Range(0, 1f)]  private float   obstacleCheckSizeDelta  = .2f;
    [SerializeField][Range(0, 1f)]  private float   obstacleCheckDistance   = .5f;
    [SerializeField][Range(0, .3f)] private float   movementSmoothing       = .05f;
    [SerializeField][Range(0, 20f)] private float   gravityScale            = 2.5f;
    [SerializeField][Range(0, 20f)] private float   fallMultiplyer          = 2.5f;
    [SerializeField][Range(0, 2f)]  private float   detectionRate           = 1.0f;
    [SerializeField][Range(0, 10f)] private float   viewDistance            = 3f;
    [SerializeField][Range(0, 20f)] private float   listenDistance          = 6.2f;
    [SerializeField][Range(0, 360)] private int     fieldOfView             = 45;
    [SerializeField][Range(0, 1)]   private float   speedSlowFactor         = .36f;
    [SerializeField] private float      jumpForce       = 400f;
    [SerializeField] private float      runSpeed        = 40f;
    [SerializeField] private float      airSpeed        = 40f;
    [SerializeField] private float      hitPoints       = 100;
    [SerializeField] private LayerMask  whatIsGround;
    [SerializeField] private bool       isListening;

    public float        FallMultiplyer          { get => fallMultiplyer;            set => fallMultiplyer = value; }
    public float        GravityScale            { get => gravityScale;              set => gravityScale = value; }
    public float        JumpForce               { get => jumpForce;                 set => jumpForce = value; }
    public float        RunSpeed                { get => runSpeed;                  set => runSpeed = value; }
    public float        AirSpeed                { get => airSpeed;                  set => airSpeed = value; }
    public float        HitPoints               { get => hitPoints; }
    public float        GroundCheckerRadius     { get => groundCheckerRadius; }
    public float        EdgeCheckDistance       { get => edgeCheckDistance; }
    public float        ObstacleCheckSizeDelta  { get => obstacleCheckSizeDelta; }
    public float        ObstacleCheckDistance   { get => obstacleCheckDistance; }
    public float        MovementSmoothing       { get => movementSmoothing; }
    public float        DetectionRate           { get => detectionRate; }
    public float        ViewDistance            { get => viewDistance; }
    public float        ListenDistance          { get => listenDistance; }
    public float        SpeedSlowFactor         { get => speedSlowFactor;           set => speedSlowFactor = value; }
    public int          FieldOfView             { get => fieldOfView; }
    public bool         IsListening             { get => isListening;               set => isListening = value; }
    public LayerMask    WhatIsGround            { get => whatIsGround; }

}