
using UnityEngine;

namespace Shiang
{
    public class Mints : Consumable
    {
        public override Item Clone(int n) => new Mints { Count = n };

        public override Sprite Image => Info.SPRITES_ICON2[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}