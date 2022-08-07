
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace Shiang
{
    public class ItemSlot : MonoBehaviour, ISelectHandler
    {
        [SerializeField] Button _btn;
        [SerializeField] Image _img;
        [SerializeField] TMP_Text _tmp;
        Item _itemHeld;

        public static event Action<string> OnItemSlotSelected;

        public Button Button { get => _btn; set => _btn = value; }
        public Image Image { get => _img; set => _img = value; }
        public TMP_Text Text { get => _tmp; set => _tmp = value; }
        public Item ItemHeld { get => _itemHeld; set => _itemHeld = value; }

        public void OnSelect(BaseEventData eventData)
        {
            OnItemSlotSelected?.Invoke(
                ItemHeld == null ? "" : $"[{ItemHeld.Name}] {ItemHeld.Description}");
            TreasurePanel.CurrentSlot = this;
        }

        public void Clear()
        {
            Image.color = Color.clear;
            Text.text = "";
            ItemHeld = null;
        }

        public void Set(Item data)
        {
            Image.color = Color.white;
            Image.sprite = data.Image;
            Text.text = data.Count.ToString();
            ItemHeld = data;
        }

        private void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                if (ItemHeld == null)
                    return;
                Item cloned = ItemHeld.Clone();
                FindObjectOfType<RanRan>()?.Items.ReceiveAll(ref cloned);
                TreasurePanel.CurrentTreasure.Items.Remove(ItemHeld);
                Clear();
            });
        }
    }
}
