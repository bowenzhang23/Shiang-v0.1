
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class StateMachine
    {
        // caches
        IState _currentState;
        List<Transition> _currentTransition;

        // transitions
        Dictionary<Type, List<Transition>> _transitions;
        List<Transition> _anyTransition;
        List<Transition> _emptyTransition;

        public StateMachine()
        {
            _currentTransition = new List<Transition>();
            _transitions = new Dictionary<Type, List<Transition>>();
            _anyTransition = new List<Transition>();
            _emptyTransition = new List<Transition>();
        }

        public float TimeInState { get; private set; }

        public void Tick()
        {
            var transition = GetTransition();

            if (transition != null)
                ChangeState(transition.To);

            _currentState?.Tick();
            TimeInState += Time.deltaTime;
        }

        public void ChangeState(IState newState)
        {
            if (newState == _currentState)
                return;

            _currentState?.Exit();
            _currentState = newState;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransition);
            if (_currentTransition == null)
                _currentTransition = _emptyTransition;

            _currentState?.Enter();
            TimeInState = 0f;
        }

        public void AddTransition(IState from, IState to, Func<bool> cond)
        {
            if (!_transitions.TryGetValue(from.GetType(), out var tmpTransition))
            {
                tmpTransition = new List<Transition>();
                _transitions[from.GetType()] = tmpTransition;
            } 
            
            tmpTransition.Add(new Transition(to, cond));
        }

        public void AddAnyTransition(IState to, Func<bool> cond)
        {
            _anyTransition.Add(new Transition(to, cond));
        }

        // Assuming order implicitly
        private Transition GetTransition()
        {
            foreach (var transition in _anyTransition)
                if (transition.Condition()) 
                    return transition;

            foreach (var transition in _currentTransition)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}