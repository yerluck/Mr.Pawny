namespace Pawny.StateMachine
{
    public abstract class Condition : IStateComponent
    {
        /// <summary>
        /// Statement to evaluate, called from <see cref="StateCondition"/>
        /// </summary>
        public abstract bool Statement();

        /// <summary>
		/// Awake is called when creating a new instance. Use this method to cache the components needed for the condition.
		/// </summary>
		/// <param name="stateMachine">The <see cref="StateMachine"/> this instance belongs to.</param>
        public virtual void Awake(StateMachine stateMachine) { }

        public virtual void OnStateEnter() { }

        public virtual void OnStateExit() { }
    }

    public readonly struct StateCondition
    {
        internal readonly Condition _condition;
        internal readonly bool _expectedResult;

        public StateCondition(Condition condition, bool expectedResult)
        {
            _condition = condition;
            _expectedResult = expectedResult;
        }

        /// <summary>
        /// Called from <see cref="StateTransition"/> that contains <see cref="StateConditionSO"/>
        /// </summary>
        /// <returns>True if expected result is equal the condition result</returns>
        public bool IsMet => _condition.Statement() == _expectedResult;
    }
}
