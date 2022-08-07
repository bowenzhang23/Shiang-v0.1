
using UnityEngine;

namespace Shiang
{
    public class SnowCream : Consumable
    {
        public override Item Clone(int n) => new SnowCream { Count = n };

        public override Sprite Image => Info.SPRITES_ICON1[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}