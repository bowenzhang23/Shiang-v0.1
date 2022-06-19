using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class SixGod : Consumable
    {
        public override uint Hash => 0x00000001;

        public override string Name => "SixGod";

        public override string Description => "沁人心脾";

        public override Sprite Image => Info.SPRITES_ICON1[26];

    }
}