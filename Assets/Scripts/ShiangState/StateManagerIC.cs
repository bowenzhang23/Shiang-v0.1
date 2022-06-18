namespace Shiang
{
    public abstract class StateManagerIC : StateManager
    {
        InputController _inputController;

        public InputController IC { get => _inputController; }

        public void SetInputController(InputController ic)
            => _inputController = ic;
    }
}