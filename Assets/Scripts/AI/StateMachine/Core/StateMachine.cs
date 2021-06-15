using UnityEngine;
using System;
using System.Collections.Generic;

namespace Pawny.StateMachine
{
    /// <summary>
    /// Main FSM controller object (add this to gameObject)
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        [Tooltip("Set the initial state of this StateMachine")]
        [SerializeField] private ScriptableObjects.TransitionTableSO _transitionTableSO = default;

#if UNITY_EDITOR
		[Space]
		[SerializeField]
		internal Debugging.StateMachineDebugger debugger = default;
#endif

        private readonly Dictionary<Type, Component> _cashedComponents = new Dictionary<Type, Component>();
        [SerializeField] internal State currentState;
        [SerializeField] private ScriptableObject _statsSO;
        [HideInInspector] public IEnemyCommonStats statsSO;
        [HideInInspector] public Aspect.AspectTypes aspectName;
        [HideInInspector] public Transform target;
        [HideInInspector] public Vector3 targetLastPosition;

        private void Awake() {
            statsSO = (IEnemyCommonStats)Instantiate(_statsSO);
            aspectName = GetComponent<Aspect>().aspectType;
            currentState = _transitionTableSO.GetInitialState(this);
#if UNITY_EDITOR
			debugger.Awake(this);
#endif
        }

        private void Start()
        {
            currentState.OnStateEnter();    
        }

        private void Update() {
            if(currentState.TryGetTransition(out State targetState))
            {
                TransitToState(targetState);
            }

            currentState.OnUpdate();
        }

        private void TransitToState(State targetState)
        {
            currentState.OnStateExit();
            currentState = targetState;
            currentState.OnStateEnter();
        }

        public new T GetComponent<T>() where T: Component
        {
            return TryGetComponent(out T component)
                ? component : throw new InvalidOperationException($"{typeof(T).Name} not found on {name} object.");
        }

        public new bool TryGetComponent<T>(out T component) where T: Component
        {
            var type = typeof(T);
            if (!_cashedComponents.TryGetValue(type, out var value))
            {
                if(base.TryGetComponent<T>(out component))
                {
                    _cashedComponents.Add(type, component);
                }

                return component != null;
            }

            component = (T)value;
            return true;
        }

        public T GetOrAddComponent<T>() where T: Component
        {
            if (!TryGetComponent<T>(out var component))
            {
                component = gameObject.AddComponent<T>();
                _cashedComponents.Add(typeof(T), component);
            }

            return component;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetLastPosition, 0.1f);
        }
    }
}