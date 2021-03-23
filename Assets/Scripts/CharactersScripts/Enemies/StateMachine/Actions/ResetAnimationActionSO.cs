using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ResetAnimationActionSO", menuName = "State Machine/Actions/Reset Animation")]
public class ResetAnimationActionSO : StateActionSO
{
    [SerializeField] private EnemyAnimationOverrideKeys whatToReset; 

    protected override StateAction CreateAction() => new ResetAnimationAction(whatToReset);
}

public class ResetAnimationAction : StateAction
{
    private Animator _animator;
    private FightSystem _fightSystem;
    private EnemyAnimationOverrideKeys _animationKey;

    public ResetAnimationAction(EnemyAnimationOverrideKeys animationKey)
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
        _animator.runtimeAnimatorController = _fightSystem.weaponClips[_animationKey];
    }

    public override void OnUpdate() { }
}
