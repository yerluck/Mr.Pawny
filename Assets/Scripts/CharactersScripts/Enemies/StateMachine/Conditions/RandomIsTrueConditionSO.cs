using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "RandomIsTrueConditionSO", menuName = "State Machine/Conditions/Probablity Of Positive Outcome")]
public class RandomIsTrueConditionSO : StateConditionSO
{
    [Tooltip("Probability of a positive outcome (0% - 100%)")]
    [SerializeField][Range(0f, 1f)] private float probability;

    protected override Condition CreateCondition() => new RandomIsTrueCondition(probability);
}

public class RandomIsTrueCondition : Condition
{
    private readonly float probability;

    public RandomIsTrueCondition(float probability)
    {
        this.probability = probability;
    }

	protected override bool Statement() => probability >= Random.Range(0f, 100f);
}
