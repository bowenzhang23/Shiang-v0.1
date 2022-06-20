using UnityEngine;

namespace Shiang
{
    [DefaultExecutionOrder(-200)]
    public class GameController : MonoBehaviour
    {
        void Awake()
        {
            Info.LoadDatabase();
            Info.LoadResources();
            Pool.Load();
        }
    }
}