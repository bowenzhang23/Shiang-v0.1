using System.Linq;
using UnityEngine;

namespace Shiang
{
    public class GoldenScepter : Ability
    {
        AnimationClip[] _animationClips;

        public override AnimationClip[] Clips
            => _animationClips == null 
            ? _animationClips = Utils.BuildClips(Info.PLAYER_ANIM_CLIPS, "Magic")
            : _animationClips;

        public override string Name => "黄金权杖";
 
        public override string Description => "堆堆之法器";

        public override uint Hash => 0x00000003;

        public override Sprite Image => Info.SPRITES_ICON1[12];

        public override float CdTime => 5f;
    }
}