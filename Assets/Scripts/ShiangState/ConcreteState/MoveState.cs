
namespace Shiang
{
    public class MoveState : IState
    {
        readonly IDynamic _owner;

        public MoveState(IDynamic entity) => _owner = entity;

        public void Enter() { }

        public void Exit() { }

        public void Tick() => _owner.Move();
    }
}