
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class Ability : IGameObject, IVivid, IComparable
    {
        public event Action OnUse;

        public abstract AnimationClip[] Clips { get; }

        public abstract string Description { get; }

        public abstract uint Hash { get; }

        public abstract Sprite Image { get; }

        public abstract string Name { get; }

        public string[] ClipNames => new string[2] { Clips[0].name, Clips[1].name };
        
        public float ClipLength => Clips[0].length;

        public int CompareTo(object obj)
        {
            if (Hash > ((Item)obj).Hash) return 1;
            if (Hash < ((Item)obj).Hash) return -1;
            else return 0;
        }
    }
}