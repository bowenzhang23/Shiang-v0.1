
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class ShiangObject : IGameObject, IComparable
    {
        string _classID;

        public ShiangObject() => _classID = GetType().Name;

        public string ClassID { get => _classID; }

        public abstract string Description { get; }

        public abstract uint Hash { get; }

        public abstract Sprite Image { get; }

        public abstract string Name { get; }

        public int CompareTo(object obj)
        {
            if (Hash > ((Item)obj).Hash) return 1;
            if (Hash < ((Item)obj).Hash) return -1;
            else return 0;
        }
    }
}