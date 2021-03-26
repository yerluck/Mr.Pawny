using Pawny.StateMachine.ScriptableObjects;

namespace Pawny.StateMachine
{
    public class State
    {
        internal StateSO originSO;
        internal StateMachine stateMachine;
        internal StateAction[] actions;
        internal StateTransition[] transitions;

        internal State() { }

        public State(
			StateSO originSO,
			StateMachine stateMachine,
			StateTransition[] transitions,
			StateAction[] actions)
		{
			this.originSO = originSO;
			this.stateMachine = stateMachine;
			this.transitions = transitions;
			this.actions = actions;
		}

        public void OnStateEnter()
        {
            void OnStateEnter(IStateComponent[] comps)
            {
                for (int i = 0; i < comps.Length; i++)
                {
                    comps[i].OnStateEnter();
                }
            }
            OnStateEnter(transitions);
            OnStateEnter(actions);
        }

        public void OnUpdate()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].OnUpdate();
            }
        }

        public void OnStateExit()
        {
            void OnStateExit(IStateComponent[] comps)
            {
                for (int i = 0; i < comps.Length; i++)
                {
                    comps[i].OnStateExit();
                }
            }
            OnStateExit(transitions);
            OnStateExit(actions);
        }

        public bool TryGetTransition(out State state)
        {
            state = null;

            for (int i = 0; i < transitions.Length; i++)
            {
                if(transitions[i].TryGetTransition(out state))
                {
                    break;
                }
            }

            for (int i = 0; i < transitions.Length; i++)
            {
                transitions[i].ClearConditionsCache();
            }

            return state != null;
        }
    }
}
