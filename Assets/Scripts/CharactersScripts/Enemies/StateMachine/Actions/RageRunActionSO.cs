using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "RageRunActionSO", menuName = "State Machine/Actions/Rage Run")]
public class RageRunActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new RageRunAction();
}

public class RageRunAction : StateAction
{
    private Vector2 _moveDirection;
    private LandEnemy<StateMachine> _controller;
    private StateMachine _stateMachine;
    
    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
        _stateMachine = stateMachine;
    }

    public override void OnStateEnter()
    {
        _controller.FaceTargetPoint(_stateMachine._targetLastPosition);
        _moveDirection = _stateMachine.transform.localScale.normalized;
    } 

    public override void OnUpdate()
    {
        _controller.Move(_moveDirection, false);
    }
}
