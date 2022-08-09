using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class Broadsword : Weapon
    {
        public override void Hit(IHurtable hurtable)
        {
        }

        public override Item Clone(int n) => new Broadsword { Count = n };

        public override Sprite Image => Info.SPRITES_ICON1[Info.WEAPON_DATA[ClassID].SpriteIndex];
    }
}