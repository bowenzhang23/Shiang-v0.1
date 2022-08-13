
using UnityEngine;

namespace Shiang
{
    public class UiManagement : MonoBehaviour
    {
        public static ITreasure CurrentTreasurePanelOwner { get; set; }

        public static GameObject CurrentDialogueActivator { get; set; }
    }
}
