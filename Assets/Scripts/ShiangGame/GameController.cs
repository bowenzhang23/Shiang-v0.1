using UnityEngine;

namespace Shiang
{
    [DefaultExecutionOrder(-200)]
    public class GameController : GenericSingleton<GameController>
    {
        public override void Awake()
        {
            base.Awake();
#if !UNITY_EDITOR
            Utils.LockCursor();
#endif
            Info.LoadDatabase();
            Info.LoadResources();
            Pool.Load();
        }

        public static void QuitGame()
        {
            Utils.SaveForPersistence();
            Application.Quit();
        }
    }
}