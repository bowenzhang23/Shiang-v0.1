
using UnityEngine;

namespace Shiang
{
    public class GoldenScepter : Ability
    {
        public override void Affect(IGameEntity entity)
        {
        }

        public override Sprite Image => Info.SPRITES_ICON1[Info.ABILITY_DATA[ClassID].SpriteIndex];
    }
}