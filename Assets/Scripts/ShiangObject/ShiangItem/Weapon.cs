
using UnityEngine;

namespace Shiang
{
    public abstract class Weapon : Equipment, IVivid
    {
        string[] _clipNames;
        Cooldown _cd;

        public abstract AnimationClip[] Clips { get; }

        public abstract float CdTime { get; }

        public string[] ClipNames
            => _clipNames == null ? _clipNames = new string[2] { Clips[0].name, Clips[1].name } : _clipNames;

        public float ClipLength => Clips[0].length;

        public Cooldown Cd
            => _cd == null ? _cd = new Cooldown(CdTime) : _cd;
    }
}