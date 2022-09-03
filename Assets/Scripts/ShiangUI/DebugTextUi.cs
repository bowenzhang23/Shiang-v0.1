
using TMPro;
using UnityEngine;

namespace Shiang
{
    public class DebugTextUi : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        void Start()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            _text.text = $"{Application.dataPath}\n{Application.persistentDataPath}\n{Application.consoleLogPath}\n{Application.temporaryCachePath}";
#else
            _text.text = $"{Application.dataPath}\n{Application.persistentDataPath}\n{FindObjectOfType<RanRan>().CurrentWeapon}\n{Utils.GetAndroidExternalFilesDir()}";
#endif
        }
    }
}