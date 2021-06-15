using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName="IsAttackCompleteCondition", menuName="State Machine/Conditions/Is Attack Complete")]
public class IsAttackCompleteConditionSO : StateConditionSO<IsAttackCompleteCondition> { }

public class IsAttackCompleteCondition : Condition
{
    private FightSystem _fightSystem;

    public override void Awake(StateMachine stateMachine)
    {
        _fightSystem = stateMachine.GetComponent<FightSystem>();
    }

    protected override bool Statement() => _fightSystem.isAttackComplete;
}
