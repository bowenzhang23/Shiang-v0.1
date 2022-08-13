using System;
using UnityEngine;

namespace Shiang
{
    #region state manager
    public class RanRanStateManager : StateManager
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
            var IC = InputController.Instance;

            SM.AddTransiton(_idle, _move, () => IC.ChangeX != 0);
            SM.AddTransiton(_cool, _move,
                () => IC.ChangeX != 0 && SM.TimeInState > _EXTITIME);
            SM.AddAnyTransition(_useWeapon,
                () => _ranran.CurrentWeapon != null && _ranran.CurrentWeapon.Cd.IsCooldown && IC.UseWeapon);
            SM.AddAnyTransition(_useAbility,
                () => _ranran.CurrentAbility != null && _ranran.CurrentAbility.Cd.IsCooldown && IC.UseAbility);

            SM.AddTransiton(_move, _idle, () => IC.ChangeX == 0);
            SM.AddTransiton(_useWeapon, _cool, () => SM.TimeInState > _useWeaponStayTime);
            SM.AddTransiton(_useAbility, _idle, () => SM.TimeInState > _useAbilityStayTime);
            SM.AddTransiton(_cool, _idle, () => SM.TimeInState > _coolStayTime);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
    #endregion
    
    public class RanRan : MonoBehaviour, IPlayer, IPersist
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
        Persister _persister;

        private Weapon _currentWeapon;
        private Ability _currentAbility;

        private SoundEffect _weaponSoundEffect;
        private SoundEffect _abilitySoundEffect;

        public StateManager StateMgr => _stateMgr;
        
        public Animator Anim => _anim;

        public Orientation Orientation => _orientation;

        public ItemContainer Items => _inventory;

        public AbilityContainer Abilities => _abilityContainer;

        public Weapon CurrentWeapon => _currentWeapon;

        public Ability CurrentAbility => _currentAbility;

        public Vector3 Coordinate => transform.position;

        public Persister Persister => _persister;

        public void Idle()
        {
#if UNITY_EDITOR
            Anim.speed = 1f;
#endif
            Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][(int)_orientation]);
        }

        public void Move()
        {
#if UNITY_EDITOR
            Anim.speed = 5f;
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = _inputController.ChangeX > 0 ? Orientation.Right : Orientation.Left;
            transform.position += Vector3.right * _inputController.ChangeX * _speed * 5f * Time.deltaTime;
#else
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = _inputController.ChangeX > 0 ? Orientation.Right : Orientation.Left;
            transform.position += Vector3.right * _inputController.ChangeX * _speed * Time.deltaTime;
#endif
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
            _weaponSoundEffect.Play(_currentWeapon.SoundtrackName);
            StartCoroutine(_currentWeapon.Cd.CountdownCo());
        }

        public void UseAbility()
        {
            if (_currentAbility == null)
                return;
            Anim.Play(_currentAbility.ClipNames[(int)_orientation]);
            _abilitySoundEffect.Play(_currentAbility.SoundtrackName);
            StartCoroutine(_currentAbility.Cd.CountdownCo());
        }

        public void Load()
        {
            _inventory = Utils.CreateItemContainer(GameMechanism.INVENTORY_CAPACITY);
            _abilityContainer = Utils.CreateAbilityContainer(GameMechanism.ABILITY_CAPACITY);
            Utils.LoadEntityDatabase(GetType().Name, ref _inventory, ref _abilityContainer);

            _currentWeapon = (Weapon)_inventory.Weapons()[0];
            _currentAbility = _abilityContainer.Data[0];
        }

        public void Save()
            => Utils.SaveEntityDatabase(GetType().Name, _inventory, _abilityContainer);

        private void Awake()
        {
            _persister = new Persister(this);
            _orientation = Orientation.Right;
            _anim = GetComponent<Animator>();
            _inputController = InputController.Instance;
            _stateMgr = Utils.CreateStateManager<RanRanStateManager, RanRan>(this);
            _weaponSoundEffect = gameObject.AddComponent<SoundEffect>();
            _abilitySoundEffect = gameObject.AddComponent<SoundEffect>();
        }

        void Update() => _stateMgr.Tick();
    }
}