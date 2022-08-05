
using UnityEngine;

namespace Shiang
{
    public abstract class Weapon : Equipment, IVivid
    {
        string[] _clipNames;
        AnimationClip[] _animationClips;
        Cooldown _cd;

        public virtual AnimationClip[] Clips
            => _animationClips == null
            ? _animationClips = Utils.BuildClips(
                Info.ANIM_CLIPS[typeof(Weapon)],
                Info.WEAPON_DATA[ClassID].AnimPattern)
            : _animationClips;

        public override string Name => Info.WEAPON_DATA[ClassID].Name;

        public override string Description => Info.WEAPON_DATA[ClassID].Description;

        public override uint Hash => Info.WEAPON_DATA[ClassID].Hash;

        public virtual float CdTime => Info.WEAPON_DATA[ClassID].CdTime;

        public string[] ClipNames
            => _clipNames == null ? _clipNames = new string[2] { Clips[0].name, Clips[1].name } : _clipNames;

        public float ClipLength => Clips[0].length;

        public Cooldown Cd
            => _cd == null ? _cd = new Cooldown(CdTime) : _cd;

        public abstract void Hit(IHurtable hurtable);
    }
}