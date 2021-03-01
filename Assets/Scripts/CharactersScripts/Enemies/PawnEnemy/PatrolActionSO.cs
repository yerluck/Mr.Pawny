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
    private Vector2 moveDirection;
    private LandEnemy<StateMachine> controller;
    
    public override void Awake(StateMachine stateMachine) {
        controller = stateMachine.GetComponent<LandEnemy<StateMachine>>();
        moveDirection = new Vector2(Random.Range(-1f, 1f), 0f).normalized;
    }

    // public override void OnStateEnter()
    // {
    // } 

    public override void OnUpdate()
    {
        if(controller.IsOnEdge)
        {
            // TODO: add new idle state
            moveDirection *= -1;
        }
        controller.Move(moveDirection);
    }
}
