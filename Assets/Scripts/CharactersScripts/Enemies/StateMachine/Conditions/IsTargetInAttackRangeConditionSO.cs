using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName="IsTargetInAttackRangeConditionSO", menuName="State Machine/Conditions/Is Target In Attack Range")]
public class IsTargetInAttackRangeConditionSO : StateConditionSO
{
    [SerializeField]
    [Range(0f, 2f)]
    private float detectionRate;
    [SerializeField]
    private LayerMask whatCanAttack; 

    protected override Condition CreateCondition() => new IsTargetInAttackRangeCondition(detectionRate, whatCanAttack);
}

public class IsTargetInAttackRangeCondition : Condition
{
    private float _attackCheckDistance;
    private float _attackCheckHeight;
    private float _detectionRate;
    private float _elapsedTime = 0f;
    private StateMachine _stateMachine;
    private LayerMask _whatCanAttack;
    private Collider2D _collider;


    public IsTargetInAttackRangeCondition(float detectionRate, LayerMask whatCanAttack)
    {
        _detectionRate = detectionRate;
        _whatCanAttack = whatCanAttack;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _collider = stateMachine.GetComponent<Collider2D>();
        EnemyWeaponSO weapon = stateMachine.GetComponent<FightSystem>().weapon;
        _attackCheckDistance = weapon.attackCheckDistance;
        _attackCheckHeight = weapon.attackCheckHeight;
    }

    protected override bool Statement() => IsTargetInAttackRange();

    private bool IsTargetInAttackRange()
    {
        _elapsedTime += Time.deltaTime;
        if ( _elapsedTime < _detectionRate)
        {
            return false;
        }

        _elapsedTime = 0f;

        var hit = Physics2D.BoxCast(
            // TODO: calculate depending on detectionHeight
            _stateMachine.transform.position,
            new Vector2(0.01f, _attackCheckHeight),
            0f,
            _stateMachine.transform.right * _stateMachine.transform.localScale.normalized.x,
            _attackCheckDistance,
            _whatCanAttack
        );

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            Aspect aspect = hit.collider.GetComponent<Aspect>();

            if(aspect != null && aspect.aspectType != _stateMachine.aspectName)
            {
                return true;
            }
        }

        return false;
    }
}
