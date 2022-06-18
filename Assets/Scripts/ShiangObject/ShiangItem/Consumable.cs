
using System;

namespace Shiang
{
    public abstract class Consumable : Item
    {
        public event Action<int> OnUse;
    }
}