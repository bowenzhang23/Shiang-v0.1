
using System;
using System.Collections;
using UnityEngine;

namespace Shiang
{
    #region state manager
    class FridgeStateManager : StateManagerIC
    {
        public static readonly float ANIM_DURATION = 0.75f;
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
                () => {
                    bool ret = _fridge.IsClosedByIC && _fridge.IsOpen;
                    _fridge.IsClosedByIC = false;
                    return ret;
                });
            SM.AddTransiton(_close, _idle,
                () => SM.TimeInState > ANIM_DURATION);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
    #endregion

    [RequireComponent(typeof(CollisionDetector))]
    public class Fridge : MonoBehaviour, IStatic, IStateHolder, ITreasure, IAnimatorHolder, IBinary, IPersist
    {
        StateManager _stateMgr;
        ItemContainer _itemContainer = new ItemContainer(GameMechanism.TREASUREBOX_CAPACITY);
        Animator _anim;
        CollisionDetector _colliDetec;
        [SerializeField] string _databaseName;
        bool _isOpen = false;

        public static event Action OnFridgeOpen;
        public static event Action OnFridgeClose;

        public StateManager StateMgr => _stateMgr;

        public ItemContainer Items => _itemContainer;

        public Animator Anim => _anim;

        public CollisionDetector Detector { get => _colliDetec; }

        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        public bool IsClosedByIC { get; set; }

        public void Close()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(OpenState)][0]);
            IsOpen = false;
            UiManagement.CurrentTreasurePanelOwner = null;
            OnFridgeClose?.Invoke();
        }

        public void Idle()
            => Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][0]);

        public void Open()
            => StartCoroutine(OpenCo());

        public IEnumerator OpenCo()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(OpenState)][1]);
            IsOpen = true;
            UiManagement.CurrentTreasurePanelOwner = this;
            yield return new WaitForSeconds(FridgeStateManager.ANIM_DURATION);
            OnFridgeOpen?.Invoke();
        }

        private void Awake()
        {
            Utils.RegisterForPersistenceAndLoad(this);
            _anim = GetComponent<Animator>();
            
            _stateMgr = Utils.CreateStateManagerIC<FridgeStateManager, Fridge>(this,
                FindObjectOfType<InputController>());
            _colliDetec = GetComponent<CollisionDetector>();

            InputController.OnExitFromUIMode += () => IsClosedByIC = true;
        }

        private void Update() => _stateMgr.Tick();

        public void Save()
        {
            Utils.SaveEntityDatabase(_databaseName, _itemContainer);
        }

        public void Load()
        {
            Utils.LoadEntityDatabase(_databaseName, ref _itemContainer);
        }
    }
}
