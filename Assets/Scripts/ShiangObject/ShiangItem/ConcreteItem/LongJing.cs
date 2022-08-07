
using UnityEngine;

namespace Shiang
{
    public class LongJing : Consumable
    {
        public override Item Clone(int n) => new LongJing { Count = n };

        public override Sprite Image => Info.SPRITES_ICON1[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}