using System.Collections.Generic;
using UnityEngine;

namespace Pawny.StateMachine.ScriptableObjects
{
    public abstract class StateConditionSO: ScriptableObject
    {
        protected abstract Condition CreateCondition();

        /// <summary>
        /// Gets the <see cref="StateCondition"/> of this SO
        /// </summary>
        /// <returns></returns>
        internal StateCondition GetCondition(StateMachine stateMachine, bool expectedResult, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (!createdInstances.TryGetValue(this, out var obj))
			{
				var condition = CreateCondition();
				condition._originSO = this;
				createdInstances.Add(this, condition);
				condition.Awake(stateMachine);

				obj = condition;
			}

			return new StateCondition(stateMachine, (Condition)obj, expectedResult);
        }
    }

    /// <summary>
    /// Use this for standard behavior, otherwise implement own <see cref="StateConditionSO"/>
    /// </summary>
    /// <typeparam name="T"><see cref="Condition"/> that would be used in this SO</typeparam>
    public abstract class StateConditionSO<T> : StateConditionSO where T: Condition, new()
    {
        protected override Condition CreateCondition() => new T();
    }
}
