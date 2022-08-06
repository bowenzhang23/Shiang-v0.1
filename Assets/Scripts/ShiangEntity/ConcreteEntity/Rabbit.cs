
using UnityEngine;

namespace Shiang
{
    #region state manager
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
    #endregion

    public class Rabbit : MonoBehaviour, IFriend, ICreature, IDynamic, IFollower
    {
        // TODO
        private float _speed = 1.0f;

        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;
        IPlayer _player;

        public StateManager StateMgr => _stateMgr;

        public Animator Anim => _anim;
        
        public Orientation Orientation => _orientation;

        public AbilityContainer Abilities => null;

        public float StartFollowDistance => 10f;

        public float StopFollowDistance => 4f;

        public float PositionDiff => transform.position.x - _player.Coordinate.x;

        public void Idle()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][(int)_orientation]);
        }

        public void Move()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = PositionDiff < 0 ? Orientation.Right : Orientation.Left;
            transform.position +=
                Vector3.right * (_orientation == Orientation.Right ? 1f : -1f) * _speed * Time.deltaTime;
        }

        public void UseAbility() { }

        public void FollowPlayer() => Move();

        private void Awake()
        {
            _orientation = Orientation.Left;
            _anim = GetComponent<Animator>();
            _stateMgr = Utils.CreateStateManager<RabbitStateManager, Rabbit>(this);
            _player = FindObjectOfType<RanRan>();
        }

        void Update() => _stateMgr.Tick();

        public bool MeetFollowCriteria()
        {
            float distance = Mathf.Abs(PositionDiff);
            return distance < StartFollowDistance && distance > StopFollowDistance;
        }
    }
}