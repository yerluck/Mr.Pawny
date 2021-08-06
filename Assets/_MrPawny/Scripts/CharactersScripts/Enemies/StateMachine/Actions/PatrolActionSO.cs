using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PatrolActionSO", menuName = "State Machine/Actions/Patrol")]
public class PatrolActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new PatrolAction();
}

public class PatrolAction : StateAction
{
    private Vector2 _moveDirection;
    private LandEnemy<StateMachine> _controller;
    private StateMachine _stateMachine;
    private bool _isStateFirstEnter;
    
    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
        _stateMachine = stateMachine;
        _isStateFirstEnter = true;
    }

    public override void OnStateEnter()
    {
        if (_isStateFirstEnter)
        {
            _controller.FaceTargetPoint(_stateMachine.targetLastPosition);
            _moveDirection = _stateMachine.transform.localScale.normalized;
            _isStateFirstEnter = false;
        }
    } 

    public override void OnUpdate()
    {
        _controller.Move(_moveDirection, true);
    }

    public override void OnStateExit()
    {
        _moveDirection *= -1;
    }
}
