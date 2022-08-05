
using UnityEngine;

namespace Shiang
{
    public class Bahu : MonoBehaviour, IEnemy, ICreature, IDynamic, IFollower
    {
        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;
        IPlayer _player;

        public StateManager StateMgr => _stateMgr;

        public Animator Anim => _anim;

        public Orientation Orientation => _orientation;

        public AbilityContainer Abilities => null;

        public float StartFollowDistance => 10f;

        public float StopFollowDistance => 0.1f;

        public float PositionDiff => transform.position.x - _player.Coordinate.x;

        public void FollowPlayer()
        {
        }

        public void Idle()
        {
        }

        public bool MeetFollowCriteria() => Mathf.Abs(PositionDiff) < StartFollowDistance;

        public void Move()
        {
        }

        public void TakeDamage()
        {
        }

        public void UseAbility()
        {
        }

        public void UseWeapon()
        {
        }

        private void Awake()
        {
            _orientation = Orientation.Left;
            _anim = GetComponent<Animator>();
        }

        void Update() => _stateMgr.Tick();
    }
}