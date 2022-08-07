
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shiang
{
    public class UISceneLoader : MonoBehaviour
    {
        public static event Action OnUISceneLoad;
        public static event Action OnUISceneUnload;

        private void Awake()
        {
            Fridge.OnFridgeOpen += LoadTreasureUI;
            InputController.OnExitFromUIMode += UnloadTreasureUI;
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
