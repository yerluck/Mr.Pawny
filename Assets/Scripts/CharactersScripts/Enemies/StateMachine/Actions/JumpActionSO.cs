using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "JumpActionSO", menuName = "State Machine/Actions/Jump")]
public class JumpActionSO : StateActionSO<JumpAction> { }

public class JumpAction : StateAction
{
    private LandEnemy _controller;

    public override void Awake(StateMachine stateMachine)
    {
        _controller = stateMachine.GetComponent<LandEnemy>();
    }

    public override void OnStateEnter()
    {
        _controller.Jump();
    } 

    public override void OnUpdate() { }
}
