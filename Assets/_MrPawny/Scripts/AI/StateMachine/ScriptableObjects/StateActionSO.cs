using System.Collections.Generic;
using UnityEngine;

namespace Pawny.StateMachine.ScriptableObjects
{
    /// <summary>
    /// Sriptable object class to implement the action. Inherit from that class to create new action.
    /// </summary>
    public abstract class StateActionSO : ScriptableObject
    {
        /// <summary>
        /// Method to get the state action for this StateActionSO
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <param name="createdInstances"></param>
        /// <returns>The state action <see cref="StateAction"/> this SO has (new if has no)</returns>
        internal StateAction GetAction(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if(createdInstances.TryGetValue(this, out var obj))
            {
                return (StateAction)obj;
            }

            var action = CreateAction();
            createdInstances.Add(this, action);
            action.originSO = this;
            action.Awake(stateMachine);
            return action;
        }

        /// <summary>
        /// Called when creating new action SO 
        /// </summary>
        /// <returns>New state action <see cref="StateAction"/></returns>
        protected abstract StateAction CreateAction();
    }

    /// <summary>
    /// Use this for standard behavior, otherwise implement own <see cref="StateActionSO"/>
    /// </summary>
    /// <typeparam name="T"><see cref="StateAction"/> that would be used in this SO</typeparam>
    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
	{
		protected override StateAction CreateAction() => new T();
	}
}
