

namespace Shiang
{
    public class IdleState : IState
    {
        readonly IGameEntity _owner;

        public IdleState(IGameEntity entity) => _owner = entity;
        
        public void Enter() => _owner.Idle();

        public void Exit() { }

        public void Tick() { }
    }
}