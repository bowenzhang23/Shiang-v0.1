
using UnityEngine;

namespace Shiang
{
    [DefaultExecutionOrder(-100)]
    public class InputController : MonoBehaviour, IGameEntity
    {
        public enum InputMode { Game, Ui }

        private InputStateManager _stateMgr;

        public InputMode Mode { get; set; }

        public float ChangeX { get; private set; }

        public float ChangeY { get; private set; }

        public bool Interact { get; private set; }

        public bool Zoom { get; private set; }

        public bool UseWeapon { get; private set; }

        public bool UseAbility { get; private set; }

        public bool Exit { get; private set; }

        public bool OpenStatMenu { get; private set; }

        private void Awake()
        {
            _stateMgr = Utils.CreateStateManager<InputStateManager, InputController>(this);
            Mode = InputMode.Game;
        }

        public void Idle() { }

        public void GameMode()
        {
            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");

            ChangeX = dx != 0f ? Mathf.Sign(dx) : 0f;
            ChangeY = dy != 0f ? Mathf.Sign(dy) : 0f;

            Interact = Input.GetKeyDown(KeyCode.I);
            Zoom = Input.GetKeyDown(KeyCode.Z);

            UseWeapon = Input.GetKeyDown(KeyCode.Space);
            UseAbility = Input.GetKeyDown(KeyCode.LeftControl);

            Exit = Input.GetKeyDown(KeyCode.Escape);
            OpenStatMenu = Input.GetKeyDown(KeyCode.Alpha1);

            if (OpenStatMenu) Mode = InputMode.Ui;
        }

        public void UiMode()
        {
            Exit = Input.GetKeyDown(KeyCode.Escape);
            if (Exit) Mode = InputMode.Game;
        }

        private void Update() => _stateMgr.Tick();
    }
}