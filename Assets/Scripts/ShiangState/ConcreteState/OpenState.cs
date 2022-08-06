
namespace Shiang
{
    public class OpenState : IState
    {
        IBinary _box;

        public OpenState(IBinary box) => _box = box;

        public void Enter() => _box.Open();

        public void Exit() => _box.Close();

        public void Tick() { }
    }
}