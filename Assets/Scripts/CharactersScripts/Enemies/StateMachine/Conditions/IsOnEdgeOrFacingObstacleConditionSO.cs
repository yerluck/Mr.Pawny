﻿using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName="IsOnEdgeOrFacingObstacle", menuName="State Machine/Conditions/Is On Edge Or Facing Obstacle")]
public class IsOnEdgeOrFacingObstacleConditionSO : StateConditionSO<IsOnEdgeOrFacingObstacleCondition> { }

public class IsOnEdgeOrFacingObstacleCondition : Condition
{
    private LandEnemy<StateMachine> _controller;

    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
    }

    protected override bool Statement() => _controller.IsOnEdge || _controller.IsFacingObstacle;
}
