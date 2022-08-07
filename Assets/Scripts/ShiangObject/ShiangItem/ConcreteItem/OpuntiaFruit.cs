
using UnityEngine;

namespace Shiang
{
    public class OpuntiaFruit : Consumable
    {
        public override Sprite Image => Info.SPRITES_ICON1[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];

        public override Item Clone(int n) => new OpuntiaFruit { Count = n };
    }
}