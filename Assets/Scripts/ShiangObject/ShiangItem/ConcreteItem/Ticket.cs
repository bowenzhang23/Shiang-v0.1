
using UnityEngine;

namespace Shiang
{
    public class Ticket : Consumable
    {
        public override Item Clone(int n) => new Ticket { Count = n };

        public override Sprite Image => Info.SPRITES_ICON2[Info.CONSUMABLE_DATA[ClassID].SpriteIndex];
    }
}