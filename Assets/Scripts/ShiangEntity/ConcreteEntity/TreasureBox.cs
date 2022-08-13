
using System;
using System.Collections;
using UnityEngine;

namespace Shiang
{
    #region state manager
    class TreasureBoxStateManager : StateManager
    {
        public static readonly float ANIM_DURATION = 0.75f;
        IdleState _idle;
        OpenState _open;
        CloseState _close;
        TreasureBox _treasureBox;

        public override void InitStates()
        {
            if (_treasureBox == null)
                _treasureBox = (TreasureBox)Owner;

            _idle = new IdleState(_treasureBox);
            _open = new OpenState(_treasureBox);
            _close = new CloseState();
        }

        public override void InitTransitions()
        {
            var IC = InputController.Instance;

            SM.AddAnyTransition(_open, 
                () => _treasureBox.Detector.PlayerDetected && IC.Interact && !_treasureBox.IsOpen);
            SM.AddTransiton(_open, _close, 
                () => {
                    bool ret = _treasureBox.IsClosedByIC && _treasureBox.IsOpen;
                    _treasureBox.IsClosedByIC = false;
                    return ret;
                });
            SM.AddTransiton(_close, _idle,
                () => SM.TimeInState > ANIM_DURATION);
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
    #endregion

    [RequireComponent(typeof(CollisionDetector))]
    public class TreasureBox : MonoBehaviour, IStatic, IStateHolder, ITreasure, IAnimatorHolder, IBinary, IPersist
    {
        StateManager _stateMgr;
        ItemContainer _itemContainer = new ItemContainer(GameMechanism.TREASUREBOX_CAPACITY);
        Animator _anim;
        CollisionDetector _colliDetec;
        Persister _persister;
        [SerializeField] string _databaseName;
        bool _isOpen = false;

        public static event Action OnOpen;
        public static event Action OnClose;

        public StateManager StateMgr => _stateMgr;

        public ItemContainer Items => _itemContainer;

        public Animator Anim => _anim;

        public CollisionDetector Detector { get => _colliDetec; }

        public Persister Persister => _persister;
        
        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        public bool IsClosedByIC { get; set; }

        public void Close()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(OpenState)][0]);
            IsOpen = false;
            UiManagement.CurrentTreasurePanelOwner = null;
            OnClose?.Invoke();
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
            yield return new WaitForSeconds(TreasureBoxStateManager.ANIM_DURATION);
            OnOpen?.Invoke();
        }

        private void Awake()
        {
            _persister = new Persister(this);
            _anim = GetComponent<Animator>();
            _stateMgr = Utils.CreateStateManager<TreasureBoxStateManager, TreasureBox>(this);
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
