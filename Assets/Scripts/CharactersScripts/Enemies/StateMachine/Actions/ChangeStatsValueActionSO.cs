using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;
using Moment = Pawny.StateMachine.StateAction.SpecificMoment;
using System.Reflection;
using System;

[CreateAssetMenu(fileName = "ChangeStatsValueActionSO", menuName = "State Machine/Actions/Change Stats Value")]
public class ChangeStatsValueActionSO : StateActionSO
{
    public PropertyType propertyType = default;
    public string propertyName = default;

    public bool boolValue = default;
	public int intValue = default;
	public float floatValue = default;

    public Moment whenToRun = default;

    protected override StateAction CreateAction() => new ChangeStatsValueAction(propertyName, this);

    public enum PropertyType
    {
        Bool, Float, Int,
    }
}

public class ChangeStatsValueAction : StateAction
{
    private string _propertyName;
    private ChangeStatsValueActionSO _originSO;
    private PropertyInfo _propertyInfo;
    private IEnemyCommonStats _statsObject;

	public ChangeStatsValueAction(string propertyName, ChangeStatsValueActionSO originSO)
	{
        _propertyName = propertyName;
		_originSO = originSO;
	}

	public override void Awake(StateMachine stateMachine)
	{
        _statsObject = stateMachine.statsSO;
        Type statsType = typeof(IEnemyCommonStats);
        _propertyInfo = statsType.GetProperty(_propertyName);
	}

	public override void OnStateEnter()
	{
		if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
			SetPropertyValue();
	}

	public override void OnStateExit()
	{
		if (_originSO.whenToRun == SpecificMoment.OnStateExit)
			SetPropertyValue();
	}

	private void SetPropertyValue()
	{
		switch (_originSO.propertyType)
		{
			case ChangeStatsValueActionSO.PropertyType.Bool:
				_propertyInfo.SetValue(_statsObject, _originSO.boolValue);
				break;
			case ChangeStatsValueActionSO.PropertyType.Int:
				_propertyInfo.SetValue(_statsObject, _originSO.intValue);
				break;
			case ChangeStatsValueActionSO.PropertyType.Float:
				_propertyInfo.SetValue(_statsObject, _originSO.floatValue);
				break;
		}
	}

	public override void OnUpdate() { }
}
