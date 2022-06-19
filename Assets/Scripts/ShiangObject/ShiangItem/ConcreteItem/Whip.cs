using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class Whip : Weapon
    {
        public override AnimationClip[] Clips => Info.PLAYER_ANIM_CLIPS
                    .Where(k => k.name.Contains("Attack"))
                    .OrderBy(k => k.name.Contains("Right")).ToArray();

        public override uint Hash => 0x00000000;

        public override string Name => "Whip";

        public override string Description => "非常好";

        public override Sprite Image => Info.SPRITES_ICON2[0];

    }
}