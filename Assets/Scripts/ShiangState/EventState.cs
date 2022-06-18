using System;

namespace Shiang
{
    public class EventState : IState
    {
        public event Action OnStateEnter;
        public event Action OnStateTick;
        public event Action OnStateExit;

        public void Enter() => OnStateEnter?.Invoke();

        public void Exit() => OnStateExit?.Invoke();

        public void Tick() => OnStateTick?.Invoke();
    }
}