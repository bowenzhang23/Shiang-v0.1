using UnityEngine;

namespace Shiang
{
    public class RanRan : MonoBehaviour, IPlayer, ICreature, IDynamic, IControllable, ITreasure
    {
        // TODO
        private const float _speed = 2.4f;

        [SerializeField] GameObject _flipObjects;
        InputController _inputController;
        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;
        ItemContainer _inventory;
        AbilityContainer _abilityContainer;

        private Weapon _currentWeapon;
        private Ability _currentAbility;

        public StateManager StateMgr => _stateMgr;
        
        public Animator Anim => _anim;

        public Orientation Orientation => _orientation;

        public ItemContainer Items => _inventory;

        public AbilityContainer Abilities => _abilityContainer;

        public Weapon CurrentWeapon => _currentWeapon;

        public Ability CurrentAbility => _currentAbility;

        public Vector3 Coordinate => transform.position;

        public void Idle()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][(int)_orientation]);
        }

        public void Move()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = _inputController.ChangeX > 0 ? Orientation.Right : Orientation.Left;
            transform.position += Vector3.right * _inputController.ChangeX * _speed * Time.deltaTime;
        }

        public void Cool()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(CoolState)][(int)_orientation]);
        }

        public void TakeDamage()
        {
            Debug.Log("TakeDamage");
        }

        public void UseWeapon()
        {
            if (_currentWeapon == null) 
                return;
            Anim.Play(_currentWeapon.ClipNames[(int)_orientation]);
            StartCoroutine(_currentWeapon.Cd.CountdownCo());
        }

        public void UseAbility()
        {
            if (_currentAbility == null)
                return;
            Anim.Play(_currentAbility.ClipNames[(int)_orientation]);
            StartCoroutine(_currentAbility.Cd.CountdownCo());
        }

        private void Awake()
        {
            _orientation = Orientation.Right;
            _anim = GetComponent<Animator>();
            _inputController = FindObjectOfType<InputController>();
            _stateMgr = Utils.CreateStateManagerIC<PlayerStateManager, RanRan>(this, _inputController);
            _inventory = new ItemContainer(GameMechanism.INVENTORY_CAPACITY);
            _abilityContainer = new AbilityContainer(GameMechanism.ABILITY_CAPACITY);

            // TODO
            if (_inventory.Weapons().Count == 0)
            {
                var fist = Utils.ItemClonedFromPoolOfType<Whip>();
                _inventory.Receive(ref fist);
                _currentWeapon = (Weapon)_inventory.Weapons()[0];
            }

            // TODO
            if (_abilityContainer.IsEmpty())
            {
                var ability = Utils.AbilityRefFromPoolOfType<GoldenScepter>();
                _abilityContainer.Receive(ability);
                _currentAbility = _abilityContainer.Abilities()[0];
            }
        }

        void Update() => _stateMgr.Tick();
    }
}