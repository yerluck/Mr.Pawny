using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName="IsOnEdgeOrFacingObstacle", menuName="State Machine/Conditions/Is On Edge Or Facing Obstacle")]
public class IsOnEdgeOrFacingObstacleConditionSO : StateConditionSO<IsOnEdgeOrFacingObstacleCondition> { }

public class IsOnEdgeOrFacingObstacleCondition : Condition
{
    private LandEnemy<StateMachine> controller;

    public override void Awake(StateMachine stateMachine)
    {
        controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
    }

    public override bool Statement() => controller.IsOnEdge || controller.IsFacingObstacle;
}
