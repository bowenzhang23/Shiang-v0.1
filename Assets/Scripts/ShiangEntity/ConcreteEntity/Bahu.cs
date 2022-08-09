
using UnityEngine;

namespace Shiang
{
    #region state manager
    public class BahuStateManager : StateManager
    {
        IdleState _idle;
        FollowState _follow;
        UseWeaponState _useWeapon;
        Bahu _bahu;
        const float _HOLDTIME = 1f;

        public override void InitStates()
        {
            if (_bahu == null)
                _bahu  = (Bahu)Owner;

            _idle = new IdleState(_bahu);
            _follow = new FollowState(_bahu);
            _useWeapon = new UseWeaponState();

            _useWeapon.OnStateEnter += _bahu.UseWeapon;
            _useWeapon.OnStateExit += _bahu.ResetStopFollowDistance;
        }

        public override void InitTransitions()
        {
            SM.AddTransiton(_idle, _follow, () => _bahu.MeetFollowCriteria());
            SM.AddTransiton(_follow, _idle, () => !_bahu.MeetFollowCriteria());
            // TODO replace by real weapon attack range
            SM.AddAnyTransition(_useWeapon, () 
                => _bahu.CurrentWeapon != null && _bahu.CurrentWeapon.Cd.IsCooldown 
                && Mathf.Abs(_bahu.PositionDiffToTarget) < 2f); 
            SM.AddTransiton(_useWeapon, _follow, () 
                => SM.TimeInState > _bahu.CurrentWeapon.ClipLength + _HOLDTIME
                || Mathf.Abs(_bahu.PositionDiffToTarget) > 2f);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }

    #endregion

    public class Bahu : MonoBehaviour, IEnemy, ICreature, IDynamic, IFollower
    {
        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;
        IPlayer _player;
        private float _speed = 2.0f;
        private Weapon _currentWeapon;

        private float _stopDistance;

        public StateManager StateMgr => _stateMgr;

        public Animator Anim => _anim;

        public Orientation Orientation => _orientation;

        public AbilityContainer Abilities => null;

        public float StartFollowDistance => 10f;

        public float StopFollowDistance => _stopDistance;

        public float PositionDiffToTarget => transform.position.x - _player.Coordinate.x;

        public Weapon CurrentWeapon => _currentWeapon;

        public void Follow() => Move();

        public void Idle() => Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][(int)_orientation]);

        public bool MeetFollowCriteria()
        {
            float distance = Mathf.Abs(PositionDiffToTarget);
            return distance < StartFollowDistance && distance > StopFollowDistance;
        }

        public void Move()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = PositionDiffToTarget < 0 ? Orientation.Right : Orientation.Left;
            transform.position +=
                Vector3.right * (_orientation == Orientation.Right ? 1f : -1f) * _speed * Time.deltaTime;
        }

        public void TakeDamage()
        {
        }

        public void UseAbility()
        {
        }

        public void ResetStopFollowDistance() => _stopDistance = 2f * Random.Range(0.75f, 1.5f);

        public void UseWeapon()
        {
            if (_currentWeapon == null)
                return;
            Anim.Play(_currentWeapon.ClipNames[(int)_orientation]);
            StartCoroutine(_currentWeapon.Cd.CountdownCo());
        }

        private void Awake()
        {
            _currentWeapon = (Weapon)Utils.ItemClonedFromPoolOfType<Broadsword>();
            _orientation = Orientation.Left;
            _anim = GetComponent<Animator>();
            _player = FindObjectOfType<RanRan>();
            _stateMgr = Utils.CreateStateManager<BahuStateManager, Bahu>(this);
            ResetStopFollowDistance();
        }

        void Update() => _stateMgr.Tick();
    }
}