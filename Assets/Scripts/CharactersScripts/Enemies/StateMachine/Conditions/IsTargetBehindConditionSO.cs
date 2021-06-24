using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName="IsTargetBehindConditionSO", menuName="State Machine/Conditions/Is Target Behind Condition")]
public class IsTargetBehindConditionSO : StateConditionSO<IsTargetBehindCondition> { }

public class IsTargetBehindCondition : Condition
{
    private CharacterController _controller;
    private StateMachine _stateMachine;

    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<CharacterController>();
        _stateMachine = stateMachine;
    }

    protected override bool Statement() => IsTargetBehind();

    private bool IsTargetBehind()
    {
        bool isTargetOnTheRight = (_stateMachine.target.position - _stateMachine.transform.position).x > 0;
        bool isFacingRight = _controller.IsFacingRight;

        return (isTargetOnTheRight && !isFacingRight) || (!isTargetOnTheRight && isFacingRight);
    }
}
