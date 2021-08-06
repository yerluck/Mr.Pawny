using UnityEngine;

[CreateAssetMenu(fileName = "PawnEnemyStatsSO",  menuName = "Stats/Pawn Enemy Stats")]
public class PawnEnemyStatsSO : ScriptableObject, IEnemyCommonStats
{
#region Fields
    [Header("Movement related")]
    [SerializeField]
    private float       runSpeed = 40f;
    [SerializeField]
    private float       airMovementSpeed = 40f;
    [SerializeField]
    [Range(0, .3f)] 
    private float       movementSmoothing = .05f;
    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Run speed slow factor (for regular movement)")]
    private float       speedSlowFactor = .36f;
    [SerializeField]
    private LayerMask   whatIsGround;
    [Space]

    [Header("Jump/Gravity related")]
    [SerializeField]
    private float       jumpForce = 400f;
    [SerializeField]
    [Range(0, 20f)] 
    private float       jumpGravityScale = 2.5f;
    [SerializeField]
    [Range(0, 20f)] 
    private float       fallGravityScale = 2.5f;
    [Space]

    [Header("Senses related")]
    [SerializeField]
    [Range(0, 2f)]  
    private float       detectionRate = 1.0f;
    [SerializeField]
    [Range(0, 360)] 
    private int         fieldOfView = 45;
    [SerializeField]
    [Range(0, 10f)] 
    private float       viewDistance = 3f;
    [SerializeField]
    [Range(0, 20f)] 
    private float       listenDistance = 6.2f;
    [SerializeField]
    private bool        isListening;
    [Space]

    [Header("Other properties")]
    [SerializeField]
    [Range(0, 1f)]  
    private float       groundCheckRadius = .1f;
    [SerializeField]
    [Range(0, 1f)]  
    private float       edgeCheckDistance = .5f;
    [SerializeField]
    [Range(0, 1f)]  
    private float       obstacleCheckSizeDelta = .2f;
    [SerializeField]
    [Range(0, 1f)]  
    private float       obstacleCheckDistance = .5f;
    [SerializeField]
    private float       maxHitPoints = 100;
#endregion

#region Properties
    public float        FallGravityScale        { get => fallGravityScale; }
    public float        JumpGravityScale        { get => jumpGravityScale; }
    public float        JumpForce               { get => jumpForce; }
    public float        RunSpeed                { get => runSpeed; }
    public float        AirMovementSpeed        { get => airMovementSpeed; }
    public float        MaxHitPoints            { get => maxHitPoints; set => maxHitPoints = value; }
    public float        GroundCheckRadius       { get => groundCheckRadius; }
    public float        EdgeCheckDistance       { get => edgeCheckDistance; }
    public float        ObstacleCheckSizeDelta  { get => obstacleCheckSizeDelta; }
    public float        ObstacleCheckDistance   { get => obstacleCheckDistance; }
    public float        MovementSmoothing       { get => movementSmoothing; }
    public float        DetectionRate           { get => detectionRate; }
    public float        ViewDistance            { get => viewDistance; }
    public float        ListenDistance          { get => listenDistance; }
    public float        SpeedSlowFactor         { get => speedSlowFactor; }
    public int          FieldOfView             { get => fieldOfView; }
    public bool         IsListening             { get => isListening; set => isListening = value; }
    public LayerMask    WhatIsGround            { get => whatIsGround; }
#endregion
}