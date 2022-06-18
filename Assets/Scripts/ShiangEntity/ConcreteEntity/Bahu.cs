
using UnityEngine;

namespace Shiang
{
    public class Bahu : MonoBehaviour, IEnemy, ICreature, IDynamic
    {
        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;

        public StateManager StateMgr => _stateMgr;

        public Animator Anim => _anim;

        public Orientation Orientation => _orientation;

        public void Idle()
        {
        }

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