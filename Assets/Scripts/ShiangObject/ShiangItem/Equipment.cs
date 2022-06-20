
using System;

namespace Shiang
{
    public abstract class Equipment : Item
    {
        public event Action OnEquiped;
        public event Action OnRemoved;
    }
}