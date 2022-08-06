
using UnityEngine;

namespace Shiang
{
    public class SixDemon : Consumable
    {
        public override Item Clone(int n) => new SixDemon { Count = n };

        public override Sprite Image => Info.SPRITES_ICON1[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}