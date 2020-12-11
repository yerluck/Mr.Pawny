using System;
using UnityEngine;
public class PawnEnemyManager: Singleton<PawnEnemyManager>
{
    [SerializeField][Range(0, 1f)] private float groundCheckerRadius = .1f;
    [SerializeField][Range(0, 1f)] private float edgeCheckDistance = .5f;
    [SerializeField][Range(0, 1f)] private float obstacleCheckSizeDelta = .2f;
    [SerializeField][Range(0, 1f)] private float obstacleCheckDistance = .5f;
    [SerializeField][Range(0, .3f)] private float movementSmoothing = .05f;
    [SerializeField][Range(0, 20f)] private float gravityScale = 2.5f;
    [SerializeField][Range(0, 20f)] private float fallMultiplyer = 2.5f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private float airSpeed = 40f;
    [SerializeField] private float hitPoints = 100;
    [SerializeField] private LayerMask whatIsGround;

    public float FallMultiplyer { get => fallMultiplyer; set => fallMultiplyer = value; }
    public float GravityScale { get => gravityScale; set => gravityScale = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float RunSpeed { get => runSpeed; set => runSpeed = value; }
    public float AirSpeed { get => airSpeed; set => airSpeed = value; }
    public float HitPoints { get => hitPoints; }
    public float GroundCheckerRadius { get => groundCheckerRadius; }
    public float EdgeCheckDistance { get => edgeCheckDistance; }
    public float ObstacleCheckSizeDelta { get => obstacleCheckSizeDelta; }
    public float ObstacleCheckDistance { get => obstacleCheckDistance; }
    public float MovementSmoothing { get => movementSmoothing; }
    public LayerMask WhatIsGround { get => whatIsGround; }
}