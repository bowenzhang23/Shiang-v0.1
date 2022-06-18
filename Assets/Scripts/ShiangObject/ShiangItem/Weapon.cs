
using UnityEngine;

namespace Shiang
{
    public abstract class Weapon : Equipment, IVivid
    {
        public abstract AnimationClip[] Clips { get; }
        public string[] ClipNames => new string[2] { Clips[0].name, Clips[1].name };
        public float ClipLength => Clips[0].length;
    }
}