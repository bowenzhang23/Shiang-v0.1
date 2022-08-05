
namespace Shiang
{ 
    public class PlayerStateManager : StateManagerIC
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

        public PlayerStateManager() : base() { }

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
}