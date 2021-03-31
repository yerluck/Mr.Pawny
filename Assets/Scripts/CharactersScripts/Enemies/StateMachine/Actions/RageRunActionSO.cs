using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "RageRunActionSO", menuName = "State Machine/Actions/Rage Run")]
public class RageRunActionSO : StateActionSO<RageRunAction> { }

public class RageRunAction : StateAction
{
    private Vector2 _moveDirection;
    private LandEnemy _controller;
    private StateMachine _stateMachine;
    
    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy>();
        _stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {
        _controller.FaceTargetPoint(_stateMachine.targetLastPosition);
        _moveDirection = _stateMachine.transform.localScale.normalized;
    } 

    public override void OnUpdate()
    {
        _controller.Move(_moveDirection, false);
    }

    public override void OnStateExit()
    {
        _controller.Move(Vector2.zero, true);
    }
}
