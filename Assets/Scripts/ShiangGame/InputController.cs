
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Shiang
{
    #region state manager
    public class InputStateManager : StateManager
    {
        InputController _controller;
        GameModeState _gameMode;
        UiModeState _uiMode;
        StoryModeState _storyMode;

        public InputStateManager()
            : base()
        {
        }

        public override void InitStates()
        {
            // the casting here is safe
            if (_controller == null)
                _controller = ((InputController)Owner);

            _gameMode = new GameModeState();
            _uiMode = new UiModeState();
            _storyMode = new StoryModeState();

            _gameMode.OnStateTick += _controller.GameMode;
            _storyMode.OnStateTick += _controller.StoryMode;
            _uiMode.OnStateTick += _controller.UiMode;
        }

        public override void InitTransitions()
        {
            SM.AddTransition(_gameMode, _uiMode,
                () => _controller.Mode == InputController.InputMode.Ui);
            SM.AddTransition(_gameMode, _storyMode,
                () => _controller.Mode == InputController.InputMode.Story);
            SM.AddTransition(_uiMode, _gameMode,
                () => _controller.Exit);
            SM.AddTransition(_storyMode, _gameMode,
                () => _controller.Mode == InputController.InputMode.Game);
        }

        public override void SetInitialState() => SM.ChangeState(_gameMode);
    }
    #endregion

    [DefaultExecutionOrder(-100)]
    public class InputController : GenericSingleton<InputController>, IGameEntity
    {
        public enum InputMode { Game, Ui, Story }

        InputStateManager _stateMgr;

        MobileUI _mobileUI;

        public InputMode Mode { get; set; }

        public float ChangeX { get; private set; }

        public float ChangeY { get; private set; }

        public bool Interact { get; private set; }

        public bool CameraZoomIn { get; private set; }

        public bool CameraZoomOut { get; private set; }

        public bool CameraSwitch { get; private set; }

        public bool UseWeapon { get; private set; }

        public bool UseAbility { get; private set; }

        public bool Exit { get; private set; }

        public static event Action OnExitFromUIMode;

        public override void Awake()
        {
            base.Awake();
            _stateMgr = Utils.CreateStateManager<InputStateManager, InputController>(this);
            Mode = InputMode.Game;
            UiSceneLoader.OnUISceneLoad += () => Mode = InputMode.Ui;
            UiSceneLoader.OnUISceneUnload += () => Mode = InputMode.Game;

#if UNITY_STANDALONE || UNITY_EDITOR
            _mobileUI = FindObjectOfType<MobileUI>();
            if (_mobileUI)
                _mobileUI.gameObject.SetActive(false);
#endif
        }

        public void Idle() { }

        public void GameMode()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            float dx = Input.GetAxisRaw("Horizontal_m");
            float dy = Input.GetAxisRaw("Vertical_m");

            ChangeX = dx != 0f ? Mathf.Sign(dx) : 0f;
            ChangeY = dy != 0f ? Mathf.Sign(dy) : 0f;

            Interact = Input.GetKeyDown(KeyCode.I);
            CameraZoomIn = Input.GetKey(KeyCode.Z);
            CameraZoomOut = Input.GetKeyUp(KeyCode.Z);
            CameraSwitch = Input.GetKeyDown(KeyCode.X);

            UseWeapon = Input.GetKeyDown(KeyCode.Space);
            UseAbility = Input.GetKeyDown(KeyCode.LeftControl);

            Exit = Input.GetKeyDown(KeyCode.Escape);
#else
            float dx = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            // float dy = CrossPlatformInputManager.GetAxisRaw("Vertical"); // TODO

            ChangeX = dx != 0f ? Mathf.Sign(dx) : 0f;
            ChangeY = 0f; // TODO

            Interact = CrossPlatformInputManager.GetButtonDown("Interact");
            CameraZoomIn = CrossPlatformInputManager.GetButton("Zoom");
            CameraZoomOut = CrossPlatformInputManager.GetButtonUp("Zoom");
            CameraSwitch = CrossPlatformInputManager.GetButtonDown("Switch");

            UseWeapon = CrossPlatformInputManager.GetButtonDown("Weapon");
            UseAbility = CrossPlatformInputManager.GetButtonDown("Ability");

            Exit = CrossPlatformInputManager.GetButtonDown("Exit");
            OpenStatMenu = false; // TODO
#endif

            if (Exit) GameController.QuitGame(); // TODO
        }

        public void UiMode()
        {
            Time.timeScale = 0f;

#if UNITY_STANDALONE || UNITY_EDITOR
            Exit = Input.GetKeyDown(KeyCode.Escape);
#else
            Exit = CrossPlatformInputManager.GetButtonDown("Exit");
#endif

            if (Exit)
            {
                Mode = InputMode.Game;
                Time.timeScale = 1f;
                OnExitFromUIMode?.Invoke();
            }
        }

        public void StoryMode()
        {
            CameraZoomIn = Input.GetKey(KeyCode.Z);
            CameraZoomOut = Input.GetKeyUp(KeyCode.Z);
        }

        private void Update() => _stateMgr.Tick();
    }
}