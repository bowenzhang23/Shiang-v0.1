
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class Item : ShiangObject, IComparable
    {
        private int _count = 1;

        public int Count { get => _count; set => _count = value; }

        public Item Clone() => Clone(Count);

        public abstract Item Clone(int n);
    }
}