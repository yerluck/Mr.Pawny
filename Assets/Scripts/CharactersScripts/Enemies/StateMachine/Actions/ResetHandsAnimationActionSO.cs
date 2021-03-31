using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ResetHandsAnimationActionSO", menuName = "State Machine/Actions/Reset Hands Animation")]
public class ResetHandsAnimationActionSO : StateActionSO
{
    [SerializeField] private EnemyAnimationOverrideKeys whatToReset; 

    protected override StateAction CreateAction() => new ResetHandsAnimationAction(whatToReset);
}

public class ResetHandsAnimationAction : StateAction
{
    private Animator _animator;
    private FightSystem _fightSystem;
    private EnemyAnimationOverrideKeys _animationKey;

    public ResetHandsAnimationAction(EnemyAnimationOverrideKeys animationKey)
    {
        _animationKey = animationKey;
    }

    
    public override void Awake(StateMachine stateMachine)
    {
        _animator = stateMachine.GetComponent<Animator>();
        _fightSystem = stateMachine.GetComponent<FightSystem>();
    }

    public override void OnStateEnter()
    {
        _animator.runtimeAnimatorController = _fightSystem.weapon.handsAnimationOverriders[_animationKey];
    }

    public override void OnUpdate() { }
}
