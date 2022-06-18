
namespace Shiang
{ 
    public class PlayerStateManager : StateManagerIC
    {
        IdleState _idle;
        MoveState _move;
        UseWeaponState _useWeapon;
        UseAbilityState _useAbility;
        RanRan _ranran;

        float _useWeaponStayTime = 0f;
        float _useAbilityStayTime = 0f;
        readonly float _EXTITIME = 0.01f;

        public PlayerStateManager() : base() { }

        public override void InitStates()
        {
            if (_ranran == null)
                _ranran = (RanRan)Owner;

            _idle = new IdleState(_ranran);
            _move = new MoveState(_ranran);
            _useWeapon = new UseWeaponState();
            _useAbility = new UseAbilityState();

            _useWeapon.OnStateEnter += _ranran.UseWeapon;
            _useWeapon.OnStateEnter += () 
                => _useWeaponStayTime = _ranran.weapon.ClipLength + _EXTITIME;

            _useAbility.OnStateEnter += _ranran.UseAbility;
            _useAbility.OnStateEnter += () 
                => _useAbilityStayTime = _ranran.ability.ClipLength + _EXTITIME;
        }

        public override void InitTransitions()
        {
            SM.AddTransiton(_idle, _move, () => IC.ChangeX != 0);
            SM.AddAnyTransition(_useWeapon, () => IC.UseWeapon);
            SM.AddAnyTransition(_useAbility, () => IC.UseAbility);

            SM.AddTransiton(_move, _idle, () => IC.ChangeX == 0);
            SM.AddTransiton(_useWeapon, _idle, () => SM.TimeInState > _useWeaponStayTime);
            SM.AddTransiton(_useAbility, _idle, () => SM.TimeInState > _useAbilityStayTime);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
}