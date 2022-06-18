
using System;

namespace Shiang
{
    public class Transition
    {
        public Transition(IState to, Func<bool> cond)
        {
            To = to;
            Condition = cond;
        }

        public IState To { get; }

        public Func<bool> Condition { get; }
    }
}