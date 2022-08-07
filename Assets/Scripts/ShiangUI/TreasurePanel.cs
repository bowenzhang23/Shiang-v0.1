
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class TreasurePanel : MonoBehaviour
    {
        List<ItemSlot> _itemSlots;

        public static ItemSlot CurrentSlot { get; set; }
        
        public static ITreasure CurrentTreasure { get; set; }

        private void Awake()
        {
            ItemSlot[] itemSlots = GetComponentsInChildren<ItemSlot>();
            _itemSlots = new List<ItemSlot>(itemSlots.Length);
            foreach (var slot in itemSlots)
                _itemSlots.Add(slot);
            ClearSlots();
        }

        private void ClearSlots()
        {
            foreach (var slot in _itemSlots)
                slot.Clear();
        }

        public void SetOwner(ITreasure treasure)
        {
            ClearSlots();
            
            if (treasure == null)
                return;

            CurrentTreasure = treasure;

            for (int i = 0; i < CurrentTreasure.Items.Size(); ++i)
                _itemSlots[i].Set(CurrentTreasure.Items.Data[i]);
        }

        private void OnEnable()
        {
            SetOwner(UIManagement.CurrentTreasurePanelOwner);
        }
    }
}