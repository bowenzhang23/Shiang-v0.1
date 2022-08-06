using System;
using UnityEngine;

namespace Shiang
{
    #region state manager
    public class RanRanStateManager : StateManagerIC
    {
        IdleState _idle;
        MoveState _move;
        CoolState _cool;
        UseWeaponState _useWeapon;
        UseAbilityState _useAbility;
        RanRan _ranran;

        float _coolStayTime = 2f;
        float _useWeaponStayTime = 0f;
        float _useAbilityStayTime = 0f;
        readonly float _EXTITIME = 0.01f;

        public RanRanStateManager() : base() { }

        public override void InitStates()
        {
            if (_ranran == null)
                _ranran = (RanRan)Owner;

            _idle = new IdleState(_ranran);
            _move = new MoveState(_ranran);
            _cool = new CoolState(_ranran);
            _useWeapon = new UseWeaponState();
            _useAbility = new UseAbilityState();

            _useWeapon.OnStateEnter += _ranran.UseWeapon;
            _useWeapon.OnStateEnter += ()
                => _useWeaponStayTime = _ranran.CurrentWeapon.ClipLength + _EXTITIME;

            _useAbility.OnStateEnter += _ranran.UseAbility;
            _useAbility.OnStateEnter += ()
                => _useAbilityStayTime = _ranran.CurrentAbility.ClipLength + _EXTITIME;
        }

        public override void InitTransitions()
        {
            SM.AddTransiton(_idle, _move, () => IC.ChangeX != 0);
            SM.AddTransiton(_cool, _move,
                () => IC.ChangeX != 0 && SM.TimeInState > _EXTITIME);
            SM.AddAnyTransition(_useWeapon,
                () => _ranran.CurrentWeapon.Cd.IsCooldown && IC.UseWeapon);
            SM.AddAnyTransition(_useAbility,
                () => _ranran.CurrentAbility.Cd.IsCooldown && IC.UseAbility);

            SM.AddTransiton(_move, _idle, () => IC.ChangeX == 0);
            SM.AddTransiton(_useWeapon, _cool, () => SM.TimeInState > _useWeaponStayTime);
            SM.AddTransiton(_useAbility, _cool, () => SM.TimeInState > _useAbilityStayTime);
            SM.AddTransiton(_cool, _idle, () => SM.TimeInState > _coolStayTime);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
    #endregion
    
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
        EntityDB _database;

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
            _stateMgr = Utils.CreateStateManagerIC<RanRanStateManager, RanRan>(this, _inputController);
            
            _inventory = new ItemContainer(GameMechanism.INVENTORY_CAPACITY);
            _abilityContainer = new AbilityContainer(GameMechanism.ABILITY_CAPACITY);
            Utils.LoadEntityDatabase(GetType().Name, ref _inventory, ref _abilityContainer);

            _currentWeapon = (Weapon)_inventory.Weapons()[0];
            _currentAbility = _abilityContainer.Data[0];
        }

        void Update() => _stateMgr.Tick();
    }
}