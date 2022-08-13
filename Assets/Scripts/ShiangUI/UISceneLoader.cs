
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shiang
{
    public class UiSceneLoader : GenericSingleton<UiSceneLoader>
    {
        public static event Action OnUISceneLoad;
        public static event Action OnUISceneUnload;

        public override void Awake()
        {
            base.Awake();
            TreasureBox.OnOpen += LoadTreasureUI;
            InputController.OnExitFromUIMode += UnloadTreasureUI;
        }

        private void OnDestroy()
        {
            TreasureBox.OnOpen -= LoadTreasureUI;
            InputController.OnExitFromUIMode -= UnloadTreasureUI;
        }

        public static void LoadTreasureUI()
        {
            if (SceneManager.GetSceneByName("TreasureUI").isLoaded)
                return;
            SceneManager.LoadSceneAsync("TreasureUI", LoadSceneMode.Additive);
            OnUISceneLoad?.Invoke();
        }

        public static void UnloadTreasureUI()
        {
            if (!SceneManager.GetSceneByName("TreasureUI").isLoaded)
                return;
            SceneManager.UnloadSceneAsync("TreasureUI");
            OnUISceneUnload?.Invoke();
        }
    }
}
