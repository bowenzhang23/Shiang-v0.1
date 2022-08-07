
using UnityEngine;

namespace Shiang
{
    public class Bonbon : Consumable
    {
        public override Item Clone(int n) => new Bonbon { Count = n };

        public override Sprite Image => Info.SPRITES_ICON2[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}