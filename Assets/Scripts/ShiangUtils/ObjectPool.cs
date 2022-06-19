
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shiang
{
    public static class ObjectPool
    {
        static Dictionary<Type, uint> _hashes;
        static Dictionary<uint, Ability> _abilities;
        static Dictionary<uint, Item> _items;

        public static Dictionary<Type, uint> Mapping { get => _hashes; }
        public static Dictionary<uint, Item> Items { get => _items; }
        public static Dictionary<uint, Ability> Abilities { get => _abilities; }

        public static void Load()
        {
            _hashes = new Dictionary<Type, uint>(); // first

            _items = LoadToDictionary<Item>();
            _abilities = LoadToDictionary<Ability>();
        }

        private static Dictionary<uint, T1> LoadToDictionary<T1>() where T1 : IGameObject
        {
            var dict = Utils.GetSubclassesOf<T1>().ToDictionary(k => k.Hash, k => k);
            dict.ToList().ForEach((KeyValuePair<uint, T1> k) => _hashes.Add(k.Value.GetType(), k.Key));
            return dict;
        }
    }
}
