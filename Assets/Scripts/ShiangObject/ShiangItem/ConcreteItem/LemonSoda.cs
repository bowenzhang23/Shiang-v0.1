
using UnityEngine;

namespace Shiang
{
    public class LemonSoda : Consumable
    {
        public override Item Clone(int n) => new LemonSoda { Count = n };

        public override Sprite Image => Info.SPRITES_ICON1[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}