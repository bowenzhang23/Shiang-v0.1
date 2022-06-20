using UnityEngine;

namespace Shiang
{
    public class RanRan : MonoBehaviour, IPlayer, ICreature, IDynamic, IControllable
    {
        [SerializeField] GameObject _flipObjects;
        InputController _inputController;
        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;

        // TODO
        public Weapon weapon;
        public Ability ability;

        public StateManager StateMgr => _stateMgr;
        
        public Animator Anim => _anim;

        public Orientation Orientation => _orientation;

        public void Idle()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][(int)_orientation]);
        }

        public void Move()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = _inputController.ChangeX > 0 ? Orientation.Right : Orientation.Left;
            transform.position += Vector3.right * _inputController.ChangeX * 2.4f * Time.deltaTime;
        }

        public void TakeDamage()
        {
            Debug.Log("TakeDamage");
        }

        public void UseWeapon()
        {
            Anim.Play(weapon.ClipNames[(int)_orientation]);
            StartCoroutine(weapon.Cd.CountdownCo());
        }

        public void UseAbility()
        {
            Anim.Play(ability.ClipNames[(int)_orientation]);
            StartCoroutine(ability.Cd.CountdownCo());
        }

        private void Awake()
        {
            _orientation = Orientation.Right;
            _anim = GetComponent<Animator>();
            _inputController = FindObjectOfType<InputController>();
            _stateMgr = Utils.CreateStateManagerIC<PlayerStateManager, RanRan>(this, _inputController);

            Axe w = Utils.ItemClonedFromPoolOfType<Axe>();

            var itemContainer = Utils.CreateItemContainer(3);
            itemContainer.Receive(ref w);
            weapon = (Weapon)itemContainer.Weapons()[0];

            ability = Utils.AbilityRefFromPoolOfType<GoldenScepter>();
            var abilityContainer = Utils.CreateAbilityContainer(3);
            abilityContainer.Receive((GoldenScepter)ability);

        }

        void Update() => _stateMgr.Tick();
    }
}