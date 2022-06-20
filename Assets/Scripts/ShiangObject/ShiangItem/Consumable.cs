
using System;
using UnityEngine;

namespace Shiang
{
    public abstract class Consumable : Item
    {
        public event Action<int> OnUse;

        public override uint Hash => Info.CONSUMABLE_DATA[ClassID].Hash;

        public override string Name => Info.CONSUMABLE_DATA[ClassID].Name;

        public override string Description => Info.CONSUMABLE_DATA[ClassID].Description;
    }
}