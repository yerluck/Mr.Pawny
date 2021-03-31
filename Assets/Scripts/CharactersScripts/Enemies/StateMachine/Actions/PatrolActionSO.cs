using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "PatrolActionSO", menuName = "State Machine/Actions/Patrol")]
public class PatrolActionSO : StateActionSO<PatrolAction> { }

public class PatrolAction : StateAction
{
    private Vector2 _moveDirection;
    private LandEnemy _controller;
    private StateMachine _stateMachine;
    private bool _isStateFirstEnter;
    
    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy>();
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
        _controller.Move(Vector2.zero, true);
        _moveDirection *= -1;
    }
}
