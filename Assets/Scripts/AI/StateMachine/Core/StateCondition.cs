using Pawny.StateMachine.ScriptableObjects;

namespace Pawny.StateMachine
{
    public abstract class Condition : IStateComponent
    {
        private bool _isCached = false;
        private bool _cashedStatement = default;
        internal StateConditionSO originSO;

		/// <summary>
		/// Use this property to access shared data from the <see cref="StateConditionSO"/> that corresponds to this <see cref="Condition"/>
		/// </summary>
		protected StateConditionSO OriginSO => originSO;

        /// <summary>
        /// Statement to evaluate, called from <see cref="StateCondition"/>
        /// </summary>
        protected abstract bool Statement();

        /// <summary>
		/// Wrap the <see cref="Statement"/> so it can be cached.
		/// </summary>
        internal bool GetStatement()
        {
            if (!_isCached)
            {
                _isCached = true;
                _cashedStatement = Statement();
            }

            return _cashedStatement;
        }

        internal void ClearStatementCache()
        {
            _isCached = false;
        }

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
        internal readonly StateMachine _stateMachine;
        internal readonly Condition _condition;
        internal readonly bool _expectedResult;

        public StateCondition(StateMachine stateMachine, Condition condition, bool expectedResult)
        {
            _stateMachine = stateMachine;
            _condition = condition;
            _expectedResult = expectedResult;
        }

        /// <summary>
        /// Called from <see cref="StateTransition"/> that contains <see cref="StateConditionSO"/>
        /// </summary>
        /// <returns>True if expected result is equal the condition result</returns>
        public bool IsMet
        { 
            get
            {
                bool statement = _condition.GetStatement();
                bool isMet = statement == _expectedResult;

#if UNITY_EDITOR
                _stateMachine.debugger.TransitionConditionResult(_condition.originSO.name, statement, isMet);
#endif
                return isMet;
            }
        }
    }
}
