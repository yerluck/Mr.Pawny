using UnityEngine;

namespace Pawny.StateMachine
{
    /// <summary>
    /// Main FSM controller object (add this to gameObject)
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        [Tooltip("Set the initial state")]
        [SerializeField] private ScriptableObjects.StateSO _initialStateSO = null; 
        private State _currentState;


        private void Awake() {
            _currentState = _initialStateSO.GetState(this);
            _currentState.OnStateEnter();
        }

        private void Update() {
            _currentState.OnUpdate();
        }
    }
}