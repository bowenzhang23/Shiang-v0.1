
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class Item : IGameObject, IComparable
    {
        private int _count = 1;

        public int Count { get => _count; set => _count = value; }

        public abstract string Description { get; }

        public abstract uint Hash { get; }

        public abstract Sprite Image { get; }

        public abstract string Name { get; }

        public T1 Clone<T1>() where T1 : Item, new()
        {
            return Clone<T1>(Count);
        }

        public T1 Clone<T1>(int n) where T1 : Item, new()
        {
            T1 item = new T1();
            item.Count = n;
            return item;
        }

        public int CompareTo(object obj)
        {
            if (Hash > ((Item)obj).Hash) return 1;
            if (Hash < ((Item)obj).Hash) return -1;
            else return 0;
        }
    }
}