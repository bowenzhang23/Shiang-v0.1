
using UnityEngine;

namespace Shiang
{
    class FridgeStateManager : StateManagerIC
    {
        IdleState _idle;
        OpenState _open;
        CloseState _close;
        Fridge _fridge;

        public override void InitStates()
        {
            if (_fridge == null)
                _fridge = (Fridge)Owner;

            _idle = new IdleState(_fridge);
            _open = new OpenState(_fridge);
            _close = new CloseState();
        }

        public override void InitTransitions()
        {
            SM.AddAnyTransition(_open, 
                () => _fridge.Detector.PlayerDetected && IC.Interact && !_fridge.IsOpen);
            SM.AddTransiton(_open, _close, 
                () => !_fridge.Detector.PlayerDetected || (IC.Interact && _fridge.IsOpen));
            SM.AddTransiton(_close, _idle,
                () => SM.TimeInState > 1f);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }

    [RequireComponent(typeof(CollisionDetector))]
    public class Fridge : MonoBehaviour, IStatic, IStateHolder, ITreasure, IAnimatorHolder, IBinary
    {
        StateManager _stateMgr;
        ItemContainer _itemContainer = new ItemContainer();
        Animator _anim;
        CollisionDetector _colliDetec;
        [SerializeField] string _databaseName;
        bool _isOpen = false;

        public StateManager StateMgr => _stateMgr;

        public ItemContainer Items => _itemContainer;

        public Animator Anim => _anim;

        public CollisionDetector Detector { get => _colliDetec; }

        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        public void Close()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(OpenState)][0]);
            IsOpen = false;
        }

        public void Idle()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][0]);
        }

        public void Open()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(OpenState)][1]);
            IsOpen = true;
        }

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            Utils.LoadEntityDatabase(_databaseName, ref _itemContainer);
            _stateMgr = Utils.CreateStateManagerIC<FridgeStateManager, Fridge>(this,
                FindObjectOfType<InputController>());
            _colliDetec = GetComponent<CollisionDetector>();
        }

        private void Update() => _stateMgr.Tick();
    }
}
