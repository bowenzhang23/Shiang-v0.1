using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class Whip : Weapon
    {
        AnimationClip[] _animationClips;

        public override AnimationClip[] Clips
            => _animationClips == null 
            ? _animationClips = Utils.BuildClips(Info.PLAYER_ANIM_CLIPS, "Attack")
            : _animationClips;

        public override uint Hash => 0x00000000;

        public override string Name => "Whip";

        public override string Description => "非常好";

        public override Sprite Image => Info.SPRITES_ICON2[0];

        public override float CdTime => 1.1f;
    }
}