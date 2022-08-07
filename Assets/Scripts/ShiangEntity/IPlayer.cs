
using UnityEngine;

namespace Shiang
{
    public interface IPlayer : ITreasure, ICreature, IDynamic, IControllable
    {
        public Vector3 Coordinate { get; }
        public void Cool();
    }
}