
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shiang
{
    public class DescriptionPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        private void Awake()
        {
            ItemSlot.OnItemSlotSelected += UpdateText;
        }

        public void UpdateText(string str) => _text.text = str;
    }
}
