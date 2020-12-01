using UnityEngine;
public class PawnEnemyManager: Singleton<PawnEnemyManager>
{
    [SerializeField][Range(0, 1f)] private float groundCheckerRadius = .1f;
    [SerializeField][Range(0, 1f)] private float edgeCheckDistance = .5f;
    [SerializeField][Range(0, 1f)] private float obstacleCheckSizeDelta = .2f;
    [SerializeField][Range(0, 1f)] private float obstacleCheckDistance = .5f;
    [SerializeField] private LayerMask whatIsGround;


    public float GroundCheckerRadius { get => groundCheckerRadius; }
    public float EdgeCheckDistance { get => edgeCheckDistance; }
    public float ObstacleCheckSizeDelta { get => obstacleCheckSizeDelta; }
    public float ObstacleCheckDistance { get => obstacleCheckDistance; }
    public LayerMask WhatIsGround { get => whatIsGround; }
}