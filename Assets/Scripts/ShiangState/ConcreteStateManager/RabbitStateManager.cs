
namespace Shiang
{
    class RabbitStateManager : StateManager
    {
        IdleState _idle;
        FollowState _follow;
        Rabbit _rabbit;

        public override void InitStates()
        {
            if (_rabbit == null)
                _rabbit = (Rabbit)Owner;

            _idle = new IdleState(_rabbit);
            _follow = new FollowState(_rabbit);
        }

        public override void InitTransitions()
        {
            SM.AddTransiton(_idle, _follow, () => _rabbit.MeetFollowCriteria());
            SM.AddTransiton(_follow, _idle, () => !_rabbit.MeetFollowCriteria());
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
}