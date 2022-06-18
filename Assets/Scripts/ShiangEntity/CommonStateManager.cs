
namespace Shiang
{
    public class CommonStateManager : StateManager
    {
        IdleState _idle;

        public CommonStateManager() : base() { }

        public override void InitStates()
        {
            _idle = new IdleState(Owner);
        }

        public override void InitTransitions() { }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
}