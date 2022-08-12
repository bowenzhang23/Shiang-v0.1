
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class Ability : ShiangObject, IVivid
    {
        public event Action OnUse;

        string[] _clipNames;
        AnimationClip[] _animationClips;
        Cooldown _cd;

        public virtual AnimationClip[] Clips
            => _animationClips == null
            ? _animationClips = Utils.BuildClips(
                Info.ANIM_CLIPS[typeof(Ability)],
                Info.ABILITY_DATA[ClassID].AnimPattern)
            : _animationClips;

        public override string Name => Info.ABILITY_DATA[ClassID].Name;

        public override string Description => Info.ABILITY_DATA[ClassID].Description;

        public override uint Hash => Info.ABILITY_DATA[ClassID].Hash;

        public float CdTime => Info.ABILITY_DATA[ClassID].CdTime;

        public string SoundtrackName => Info.ABILITY_DATA[ClassID].SoundtrackName;

        public string[] ClipNames
            => _clipNames == null ? _clipNames = new string[2] { Clips[0].name, Clips[1].name } : _clipNames;

        public float ClipLength => Clips[0].length;

        public Cooldown Cd
            => _cd == null ? _cd = new Cooldown(CdTime) : _cd;

        public abstract void Affect(IGameEntity entity);
    }
}