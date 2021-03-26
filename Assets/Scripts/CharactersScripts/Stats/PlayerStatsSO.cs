using UnityEngine;

//TODO: Change all data to final values when game is done
[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Stats/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
#region Fields
    [Header("Movement related")]
    [SerializeField]
    private float  runSpeed = 40f;
    [SerializeField]
    private float  airMovementSpeed = 40f;
	[Range(0f, 1f)]
    [SerializeField]
    private float  crouchSpeed = .36f;
	[Range(0f, .3f)]
    [SerializeField]
    private float  movementSmoothing = .05f;
	[SerializeField]
    private bool   isAirMovementAllowed = true;
    [Space]

    [Header("Jump/Gravity related")]
    [Range(0f, 1000f)]
    [SerializeField]
    private float  jumpForce = 550f;
	[Range(0f, 1000f)]
    [SerializeField]
    private float  airJumpForce = 200f;
    [SerializeField]
    private bool   isAirJumpAllowed = false;
    [Tooltip("How fast player falls down")]
    [Range(1f,20f)]
    [SerializeField]
    private float  fallGravityScale = 3f;
    [Tooltip("Gravity scale for low jump")]
    [Range(1f,20f)]
    [SerializeField]
    private float  lowJumpGravityScale = 5f;
    [Tooltip("Regular gravity scale affecting player")]
    [Range(1f,10f)]
    [SerializeField]
    private float  jumpGravityScale = 2f;
    [Tooltip("Time for koyote jump")]
    [Range(0f, 1f)]
    [SerializeField]
    private float  koyoteJumpTime = 0.2f;
    [Tooltip("Buffer time during which player may jump (before landing)")]
    [Range(0f, 1f)]
    [SerializeField]
    private float  jumpBufferTime = 0.1f;
    [Tooltip("Distance to check if player gonna land (used for landing animation)")]
    [Range(0f, 2f)]
    [SerializeField]
    private float  landingCheckDistance = 0.5f;
    [Tooltip("Rotation of emitted dust")]
    [SerializeField]
    private Vector3 jumpDustRotation = new Vector3(75, 0, 0);
    [Space]

    [Header("Other properties")]
	[SerializeField]
    private int    maxHitPoints = 100;
    [Tooltip("Radius used to check if player is grounded")]
	[SerializeField]
    private float  groundedCheckRadius = .05f;
    [Tooltip("Radius used to check if player under the ceiling")]
    [SerializeField]
    private float  ceilingCheckRadius = .2f;
    [Range(0f, 1f)]
    [SerializeField]
    // TODO: mb that should be related to weapon (in case of multiple weapons)
    private float  attackTimeCooldown = 0.4f;
#endregion

#region Data to move to Manager
//TODO: move that data to manager
    // [SerializeField]
    // private bool   isParalized = false;
    // [SerializeField]
    // private bool   isMovementAllowed = true;
    // public bool     FacingRight         { get; set; }
#endregion

#region Properties
    public float    JumpGravityScale        { get => jumpGravityScale; }
    public float    FallGravityScale        { get => fallGravityScale; }
    public float    LowJumpGravityScale     { get => lowJumpGravityScale; }    
    public int      MaxHitPoints            { get => maxHitPoints; set => maxHitPoints = value; }
    public float    KoyoteJumpTime          { get => koyoteJumpTime; }
    public float    AttackTimeCooldown      { get=> attackTimeCooldown; }
    public float    JumpBufferTime          { get => jumpBufferTime; }
    public float    GroundedCheckRadius     { get => groundedCheckRadius; }
    public float    CeilingCheckRadius      { get => ceilingCheckRadius; }
    public float    JumpForce               { get => jumpForce; }
    public float    AirJumpForce            { get => airJumpForce; }
    public float    CrouchSpeed             { get => crouchSpeed; }
    public float    MovementSmoothing       { get => movementSmoothing; }
    public bool     IsAirJumpAllowed        { get => isAirJumpAllowed; set => isAirJumpAllowed = value; }
    public bool     IsAirMovementAllowed    { get => isAirMovementAllowed; set => isAirMovementAllowed = value; }
    public float    RunSpeed                { get => runSpeed; }
    public float    AirMovementSpeed        { get => airMovementSpeed; }
    public float    LandingCheckDistance    { get => landingCheckDistance; }
    public Vector3  JumpDustRotation        { get => jumpDustRotation; }
#endregion
}
