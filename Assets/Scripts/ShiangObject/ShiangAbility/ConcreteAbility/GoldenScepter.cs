using System.Linq;
using UnityEngine;

namespace Shiang
{
    public class GoldenScepter : Ability
    {
        public override AnimationClip[] Clips => Info.PLAYER_ANIM_CLIPS
            .Where(k => k.name.Contains("Magic"))
            .OrderBy(k => k.name.Contains("Right")).ToArray();

        public override string Name => "黄金权杖";
 
        public override string Description => "堆堆之法器";

        public override int Hash => 0x00000003;

        public override Sprite Image => Info.SPRITES_ICON1[12];

    }
}