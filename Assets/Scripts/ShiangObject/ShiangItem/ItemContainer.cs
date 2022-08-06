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
        int _capacity = 3;

        public ItemContainer() { }

        public ItemContainer(int capacity) => _capacity = capacity;

        public event Action OnFull;

        public int Size() => _items.Count;

        public int Capacity() => _capacity;

        public int Count() => _count;

        public int Count(Type itemType)
        {
            if (Find(itemType, out var existedItem))
                return existedItem.Count;
            return 0;
        }

        public void Receive(ref Item item)
            => Receive(ref item, 1);

        public void ReceiveAll(ref Item item) 
            => Receive(ref item, item.Count);

        public void Receive(ref Item item, int n)
        {
            Item receivedItem = Utils.ItemLose(n, ref item);

            if (Find(receivedItem, out var existedItem))
            {
                _count += receivedItem.Count;
                Utils.ItemMerge(existedItem, ref receivedItem);
            }
            else if (Size() == Capacity())
                OnFull?.Invoke();
            else
            {
                _count += receivedItem.Count;
                _items.Add(receivedItem);
            }
        }

        public void Remove(Item item)
        {
            if (Find(item, out var exist))
            {
                _count -= exist.Count;
                _items.Remove(exist);
            }
        }

        public void Remove(Type itemType)
        {
            if (Find(itemType, out var exist))
            {
                _count -= exist.Count;
                _items.Remove(exist);
            }
        }

        public bool IsEmpty() => Size() == 0;

        /// <summary>
        /// Find item in the container by its Hash value
        /// </summary>
        /// <param name="item">An item object. Usually it can be a temp object</param>
        /// <param name="exist">If exist, the Item will be assigned to this <c>out</c>
        /// value. If not, a <c>null</c> will be assigned</param>
        /// <returns>Is existed or not</returns>
        /// <seealso cref="Shiang.IGameObject.Hash"/>
        public bool Find(Item item, out Item exist)
        {
            exist = _items.Find((Item i) => i.Hash == item.Hash);
            if (exist != null)
                return true;
            return false;
        }

        /// <summary>
        /// Find item in the container by its type
        /// </summary>
        /// <param name="itemType">The type of the object, which can be found by 
        /// <c>typeof()</c> or <c>Object.GetType()</c></param>
        /// <param name="exist">If exist, the Item will be assigned to this <c>out</c>
        /// value. If not, a <c>null</c> will be assigned</param>
        /// <returns>Is existed or not</returns>
        /// <seealso cref="System.Type"/>
        public bool Find(Type itemType, out Item exist)
        {
            exist = _items.Find((Item i) => i.GetType() == itemType);
            if (exist != null)
                return true;
            return false;
        }

        public List<Item> Weapons() 
            => _items.FindAll((Item i) => i.GetType().IsSubclassOf(typeof(Weapon)));

        public List<Item> Data { get => _items; }
    }
}