using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pawny.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State", menuName = "State Machine/State")]
    public class StateSO : ScriptableObject {
        [SerializeField] private StateActionSO[] _actions = null;
        [SerializeField] private StateTransitionSO[] _transitions = null;

        /// <summary>
        /// Method to get state for the initial state in <see cref="StateMachine"/>
        /// </summary>
        /// <returns>The <see cref="State"/> this SO treats</returns>
        public State GetState(StateMachine stateMachine)
        {
            return GetState(stateMachine, new Dictionary<ScriptableObject, object>());
        }

        internal State GetState(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if(createdInstances.TryGetValue(this, out var obj))
            {
                return (State)obj;
            }

            var state = new State();
            createdInstances.Add(this, state);

            state._stateMachine = stateMachine;
            state._transitions = GetTransitions(_transitions, stateMachine, createdInstances);
            state._actions = GetActions(_actions, stateMachine, createdInstances);

            return state;
        }

        private StateTransition[] GetTransitions(StateTransitionSO[] scriptableTransitions,
            StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            int count = scriptableTransitions.Length;
            var transitions = new StateTransition[count];
            for (int i = 0; i < count; i++)
            {
                transitions[i] = scriptableTransitions[i].GetTransition(stateMachine, createdInstances);
            }

            return transitions;
        }

        private StateAction[] GetActions(StateActionSO[] scriptableActions,
            StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            int count = scriptableActions.Length;
            var actions = new StateAction[count];
            for (int i = 0; i < count; i++)
            {
                actions[i] = scriptableActions[i].GetAction(stateMachine, createdInstances);
            }

            return actions;
        }
    }
}
