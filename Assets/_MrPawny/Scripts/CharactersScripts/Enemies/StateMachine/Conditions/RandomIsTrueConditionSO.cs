using UnityEngine;
using Pawny.StateMachine;
using Pawny.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "RandomIsTrueConditionSO", menuName = "State Machine/Conditions/Probablity Of Positive Outcome")]
public class RandomIsTrueConditionSO : StateConditionSO
{
    [Tooltip("Probability of a positive outcome (0% - 100%)")]
    [SerializeField][Range(0f, 100f)] private float probability;
    [SerializeField][Range(0f, 2f)] private float detectionRate;

    protected override Condition CreateCondition() => new RandomIsTrueCondition(probability, detectionRate);
}

public class RandomIsTrueCondition : Condition
{
    private readonly float probability = 0f;
    private readonly float detectionRate;
    private float _elapsedTime = 0f;

    public RandomIsTrueCondition(float probability, float detectionRate)
    {
        this.probability = probability;
        this.detectionRate = detectionRate;
    }

	protected override bool Statement() => GetRandomOutput();

    private bool GetRandomOutput()
    {
        _elapsedTime += Time.deltaTime;
        if ( _elapsedTime < detectionRate)
        {
            return false;
        }

        _elapsedTime = 0f;
        return probability >= Random.Range(0f, 100f);
    }
}
