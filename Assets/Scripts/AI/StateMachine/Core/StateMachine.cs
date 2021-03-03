﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace Pawny.StateMachine
{
    /// <summary>
    /// Main FSM controller object (add this to gameObject)
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        [Tooltip("Set the initial state")]
        [SerializeField] private ScriptableObjects.StateSO _initialStateSO = null; 
        private readonly Dictionary<Type, Component> _cashedComponents = new Dictionary<Type, Component>();
        private State _currentState;

        private void Awake() {
            _currentState = _initialStateSO.GetState(this);
            _currentState.OnStateEnter();
        }

        private void Update() {
            if(_currentState.TryGetTransition(out State targetState))
            {
                TransitToState(targetState);
            }

            _currentState.OnUpdate();
        }

        private void TransitToState(State targetState)
        {
            _currentState.OnStateExit();
            _currentState = targetState;
            _currentState.OnStateEnter();
        }

        public new T GetComponent<T>() where T: Component
        {
            return TryGetComponent(out T component)
                ? component : throw new InvalidOperationException($"{typeof(T).Name} not found on {name} object.");
        }

        public new bool TryGetComponent<T>(out T component) where T: Component
        {
            var type = typeof(T);
            if(!_cashedComponents.TryGetValue(type, out var value))
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
    }
}