using UnityEngine;
using Pawny.AdditionalAction;
using Moment = Pawny.AdditionalAction.AdditionalActionSO.SpecificMoment;

[CreateAssetMenu(fileName="JumpAdditionalAction", menuName="Additional Actions/Jump")]
public class JumpAdditionalActionSO : AdditionalActionSO
{
    [SerializeField] private Moment whenToPerformAction;

    public override Moment ActionPerformMoment => whenToPerformAction;

    public override AdditionalActionBase CreateAdditionalAction() => new JumpAdditionalAction();
}

public class JumpAdditionalAction : AdditionalActionBase
{
    private LandEnemy _controller;
    public override void Initialize(Transform transform)
    {
        transform.TryGetComponent(out _controller);
    }

    public override void PerformAction()
    {
        if(_controller != null)
        {
            _controller.Jump();
        }
    }
}
