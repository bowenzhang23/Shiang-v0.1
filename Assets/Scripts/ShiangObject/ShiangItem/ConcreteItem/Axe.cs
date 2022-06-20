using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class Axe : Weapon
    {
        public override void Hit(IHurtable hurtable)
        {
        }
        
        public override Sprite Image => Info.SPRITES_ICON1[Info.WEAPON_DATA[ClassID].SpriteIndex];
    }
}