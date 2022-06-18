
using UnityEngine;

namespace Shiang
{
    public abstract class Item : IGameObject
    {
        private int _count = 1;

        public int Count { get => _count; set => _count = value; }

        public abstract string Description { get; }

        public abstract int Hash { get; }

        public abstract Sprite Image { get; }

        public abstract string Name { get; }

        public T1 Clone<T1>() where T1 : Item, new()
        {
            T1 item = new T1();
            item.Count = Count;
            return item;
        }
    }
}