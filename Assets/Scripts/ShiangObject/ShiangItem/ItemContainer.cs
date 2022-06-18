using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class ItemContainer
    {
        List<Item> _items = new List<Item>();
        int _count = 0;

        public int Size() => _items.Count;

        public int Count() => _count;

        public int Count(Type itemType)
        {
            if (Find(itemType, out var existedItem))
                return existedItem.Count;
            return 0;
        }

        public void Receive<T1>(T1 item) where T1 : Item, new() 
            => Receive<T1>(item, 1);

        public void ReceiveAll<T1>(T1 item) where T1 : Item, new() 
            => Receive<T1>(item, item.Count);

        public void Receive<T1>(T1 item, int n) where T1: Item, new()
        {
            Item receivedItem = Utils.ItemLose<T1>(n, ref item);
            _count += receivedItem.Count;

            if (Find(item, out var existedItem))
                Utils.ItemMerge(existedItem, ref receivedItem);
            else
                _items.Add(receivedItem);
        }

        public void Remove(Item item) => _items.Remove(item);

        public bool IsEmpty() => Size() == 0;

        public bool Find(Item item, out Item exist)
        {
            exist = _items.Find((Item i) => i.Hash == item.Hash);
            if (exist != null)
                return true;
            return false;
        }

        public bool Find(Type itemType, out Item exist)
        {
            exist = _items.Find((Item i) => i.GetType() == itemType);
            if (exist != null)
                return true;
            return false;
        }

        public List<Item> Weapons() 
            => _items.FindAll((Item i) => i.GetType().IsSubclassOf(typeof(Weapon)));
    }
}