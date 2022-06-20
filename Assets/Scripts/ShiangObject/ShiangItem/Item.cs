
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class Item : ShiangObject, IComparable
    {
        private int _count = 1;

        public int Count { get => _count; set => _count = value; }

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
    }
}