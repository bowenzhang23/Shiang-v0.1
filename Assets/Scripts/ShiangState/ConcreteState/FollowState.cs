
namespace Shiang
{
    class FollowState : IState
    {
        IFollower _follower;

        public FollowState(IFollower follower) => _follower = follower;

        public void Enter() { }

        public void Exit() { }

        public void Tick() => _follower.FollowPlayer();
    }
}
