using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    #region Properties
    [SerializeField] private float RunSpeed = 40f;
    [SerializeField] private float AirSpeed = 40f;
    [Range(0, 1000)][SerializeField] private float JumpForce = 550f;
	[Range(0, 1000)][SerializeField] private float AirJumpForce = 200f;
	[Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;
	[SerializeField] private bool AirControl = true;
	[SerializeField] private bool AllowMove = true;
    [SerializeField] private bool AirJump = false;
	[SerializeField] private int HitPoints = 100;
	[SerializeField] private float k_GroundedRadius = .05f;
    [SerializeField] private float k_CeilingRadius = .2f;
    [SerializeField][Range(1f,20f)] private float FallMultiplyer = 3f;
    [SerializeField][Range(1f,20f)] private float LowJumpMultiplyer = 5f;
    [SerializeField][Range(1f,10f)] private float GravityScale = 2f;
    [SerializeField][Range(0f, 1f)] private float HangTime = 0.2f;
    [SerializeField][Range(0f, 1f)] private float AttackTimeCooldown = 0.4f;
    [SerializeField][Range(0f, 1f)] private float JumpBufferTime = 0.1f;
    [SerializeField][Range(0, 2f)] private float LandingDistance = 0.5f;
    [SerializeField] private bool Paralized = false;
    [SerializeField] private Vector3 _JUMP_DUST_ROTATION = new Vector3(75, 0, 0);

    #endregion

    #region Fields
    public float gravityScale      { get => GravityScale; }
    public float fallMultiplyer    { get => FallMultiplyer; }
    public float lowJumpMultiplyer { get => LowJumpMultiplyer; }    
    public int hitPoints           { get => HitPoints; set => HitPoints = value; }
    public float hangTime          { get => HangTime; }
    public float attackTimeCooldown{get=>AttackTimeCooldown;}
    public float jumpBufferTime    { get => JumpBufferTime; }
    public float groundedRadius    { get => k_GroundedRadius; set => k_GroundedRadius = value; }
    public float ceilingRadius     { get => k_CeilingRadius; }
    public float jumpForce { get => JumpForce; }
    public float airJumpForce { get => AirJumpForce; }
    public float crouchSpeed { get => CrouchSpeed; }
    public float movementSmoothing { get => MovementSmoothing; }
    public bool airJump { get => AirJump; set => AirJump = value; }
    public bool airControl { get => AirControl; set => AirControl = value; }
    public bool allowMove { get => AllowMove; set => AllowMove = value; }
    public float runSpeed { get => RunSpeed; set => RunSpeed = value; }
    public float airSpeed { get => AirSpeed; set => AirSpeed = value; }
    public float landingDistance { get => LandingDistance; }
    public bool paralized { get => Paralized; set => Paralized = value; }
    public bool facingRight { get; set; }
    public Vector3 JUMP_DUST_ROTATION { get => _JUMP_DUST_ROTATION; }
    #endregion

   // (Optional) Prevent non-singleton constructor use.
    protected PlayerManager() { }

}
