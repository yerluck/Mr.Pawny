using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StayIdleActionSO", menuName = "State Machine/Actions/StayIdle")]
public class StayIdleActionSO : StateActionSO
{
    protected override StateAction CreateAction() => new StayIdleAction();
}

public class StayIdleAction : StateAction
{
    private LandEnemy<StateMachine> controller;
    
    public override void Awake(StateMachine stateMachine)
    {
        controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
    }

    public override void OnUpdate()
    {
        controller.Move(Vector2.zero);
    }
}
