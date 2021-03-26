using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StayIdleActionSO", menuName = "State Machine/Actions/Stay Idle")]
public class StayIdleActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new StayIdleAction();
}

public class StayIdleAction : StateAction
{
    private LandEnemy<StateMachine> _controller;
    
    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
    }

    public override void OnUpdate()
    {
        _controller.Move(Vector2.zero);
    }
}
