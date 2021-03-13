using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TimeElapsed", menuName = "State Machine/Conditions/Time elapsed")]
public class TimeElapsedConditionSO : StateConditionSO
{
	[SerializeField] float _timerLength = .5f;
	protected override Condition CreateCondition() => new TimeElapsedCondition(_timerLength);
}

public class TimeElapsedCondition : Condition
{
	private float _timerLength;
	private float _startTime;

	public override void OnStateEnter()
	{
		_startTime = Time.time;
	}

	public TimeElapsedCondition(float timerLength)
	{
		_timerLength = timerLength;
	}

	public override bool Statement() => Time.time >= _startTime + _timerLength;
}
