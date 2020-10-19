using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    #region Properties
    [SerializeField] private float m_RunSpeed = 40f;
    [SerializeField] private float m_AirSpeed = 40f;
    [Range(0, 1000)][SerializeField] private float m_JumpForce = 400f;
	[Range(0, 1000)][SerializeField] private float m_AirJumpForce = 200f;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private bool m_AllowMove = true;
    [SerializeField] private bool m_AirJump = false;
	[SerializeField] private int m_HitPoints = 100;
	[SerializeField] private float k_GroundedRadius = .05f;
    [SerializeField] private float k_CeilingRadius = .2f;
    [SerializeField][Range(1f,20f)] private float m_FallMultiplyer = 2.5f;
    [SerializeField][Range(1f,20f)] private float m_LowJumpMultiplyer = 2f;
    [SerializeField][Range(1f,10f)] private float m_GravityScale = 2.5f;
    [SerializeField][Range(0f, 1f)] private float m_HangTime = 0.2f;
    [SerializeField][Range(0f, 1f)] private float m_JumpBufferTime = 0.1f;
    [SerializeField][Range(0, 2f)] private float m_LandingDistance = 0.5f;
    [SerializeField] private bool m_paralized = false;
    [SerializeField] private Vector3 m_JUMP_DUST_ROTATION = new Vector3(75, 0, 0);

    #endregion

    #region Fields
    public float gravityScale { get => m_GravityScale; }
    public float fallMultiplyer { get => m_FallMultiplyer; }
    public float lowJumpMultiplyer { get => m_LowJumpMultiplyer; }    
    public int hitPoints { get => m_HitPoints; set => m_HitPoints = value; }
    public float hangTime { get => m_HangTime; }
    public float jumpBufferTime { get => m_JumpBufferTime; }
    public float groundedRadius { get => k_GroundedRadius; set => k_GroundedRadius = value; }
    public float ceilingRadius { get => k_CeilingRadius; }
    public float jumpForce { get => m_JumpForce; }
    public float airJumpForce { get => m_AirJumpForce; }
    public float crouchSpeed { get => m_CrouchSpeed; }
    public float movementSmoothing { get => m_MovementSmoothing; }
    public bool airJump { get => m_AirJump; set => m_AirJump = value; }
    public bool airControl { get => m_AirControl; set => m_AirControl = value; }
    public bool allowMove { get => m_AllowMove; set => m_AllowMove = value; }
    public float runSpeed { get => m_RunSpeed; set => m_RunSpeed = value; }
    public float airSpeed { get => m_AirSpeed; set => m_AirSpeed = value; }
    public float landingDistance { get => m_LandingDistance; }
    public bool paralized { get => m_paralized; set => m_paralized = value; }
    public bool facingRight { get; set; }
    public Vector3 JUMP_DUST_ROTATION { get => m_JUMP_DUST_ROTATION; }
    #endregion

   // (Optional) Prevent non-singleton constructor use.
    protected PlayerManager() { }

}
