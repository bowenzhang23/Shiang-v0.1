
namespace Shiang
{
    public class InputStateManager : StateManager
    {
        InputController _controller;
        GameModeState _gameMode;
        UiModeState _uiMode;

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

            _gameMode.OnStateTick += _controller.GameMode;
            _uiMode.OnStateTick += _controller.UiMode;
        }

        public override void InitTransitions()
        {
            SM.AddTransiton(_gameMode, _uiMode,
                () => _controller.Mode == InputController.InputMode.Ui);
            SM.AddTransiton(_uiMode, _gameMode,
                () => _controller.Mode == InputController.InputMode.Game);
        }

        public override void SetInitialState() => SM.ChangeState(_gameMode);
    }
}