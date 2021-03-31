using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AttackActionSO", menuName = "State Machine/Actions/Attack")]
public class AttackActionSO: StateActionSO<AttackAction> { }

public class AttackAction : StateAction
{
    private EnemyWeaponSO _weapon;
    private FightSystem _fightSystem;

    public override void Awake(StateMachine stateMachine)
    {
        _fightSystem = stateMachine.GetComponent<FightSystem>();
        _weapon = _fightSystem.weapon;
    }

    public override void OnStateEnter()
    {
        int attackNumber = Random.Range(0, _weapon.weaponAttackAttributesDictionary.Count);
        _fightSystem.InitAttack(attackNumber);
    } 

    public override void OnUpdate() { }
}
