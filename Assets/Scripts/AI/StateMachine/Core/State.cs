namespace Pawny.StateMachine
{
    public class State
    {
        internal StateMachine _stateMachine;
        internal StateAction[] _actions;
        internal StateTransition[] _transitions;

        internal State() { }

        public void OnStateEnter()
        {
            void OnStateEnter(IStateComponent[] comps)
            {
                for (int i = 0; i < comps.Length; i++)
                {
                    comps[i].OnStateEnter();
                }
            }
            OnStateEnter(_transitions);
            OnStateEnter(_actions);
        }

        public void OnUpdate()
        {
            for (int i = 0; i < _actions.Length; i++)
            {
                _actions[i].OnUpdate();
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
            OnStateExit(_transitions);
            OnStateExit(_actions);
        }

        public bool TryGetTransition(out State state)
        {
            for (int i = 0; i < _transitions.Length; i++)
            {
                if(_transitions[i].TryGetTransition(out state))
                {
                    return true;
                }
            }

            state = null;
            return false;
        }
    }
}
