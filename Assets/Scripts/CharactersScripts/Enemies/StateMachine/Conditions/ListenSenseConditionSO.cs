using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;
using System;

[CreateAssetMenu(fileName = "ListenSenseConditionSO", menuName = "State Machine/Conditions/Is Player Heard")]
public class ListenSenseConditionSO : StateConditionSO
{
    [SerializeField] private LayerMask _whatCanBeHeard;
    protected override Condition CreateCondition() => new ListenSenseCondition(_whatCanBeHeard);
}

public class ListenSenseCondition : Condition
{
    private LayerMask _whatCanBeHeard;
    private Transform _transform;
    private float _detectionRate;
    private float _elapsedTime = 0f;
    private float _listenDistance;
    private StateMachine _stateMachine;

    public ListenSenseCondition(LayerMask whatCanBeHeard)
    {
        _whatCanBeHeard = whatCanBeHeard;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _stateMachine   = stateMachine;
        _transform      = stateMachine.transform;
        _listenDistance = stateMachine._manager.ListenDistance;
        _detectionRate  = stateMachine._manager.DetectionRate;
    }

    protected override bool Statement() => DetectAspect();

    private bool DetectAspect()
    {
        _elapsedTime += Time.deltaTime;
#if UNITY_EDITOR
        _listenDistance = _stateMachine._manager.ListenDistance;
#endif
        if (_elapsedTime < _detectionRate)
        {
            return false;
        } else 
        {
            _elapsedTime = 0;

            var hitCollider = Physics2D.OverlapCircle(_transform.position, _listenDistance, _whatCanBeHeard);
            if(!hitCollider || !hitCollider.GetComponent<Protagonist>().m_Grounded || hitCollider.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                return false;
            }

            Aspect aspect = hitCollider.GetComponent<Aspect>();

            if (aspect && aspect.aspectType != _stateMachine._aspectName)
            {
                _stateMachine._targetLastPosition = hitCollider.transform.position;
                return true;
            }

            return false;
        }
    }
}
