
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shiang
{
    public static class Pool
    {
        static Dictionary<Type, uint> _hashes = new Dictionary<Type, uint>();
        static Dictionary<uint, Type> _hashes_r = new Dictionary<uint, Type>();
        static Dictionary<uint, Item> _items;
        static Dictionary<uint, Weapon> _weapons;
        static Dictionary<uint, Ability> _abilities;

        public static Dictionary<Type, uint> Mapping { get => _hashes; }
        public static Dictionary<uint, Type> MappingReverse { get => _hashes_r; }
        public static Dictionary<uint, Item> Items { get => _items; }
        public static Dictionary<uint, Weapon> Weapons { get => _weapons; }
        public static Dictionary<uint, Ability> Abilities { get => _abilities; }

        public static void Load()
        {
            _items = LoadToDictionaryAndMapping<Item>();
            _weapons = LoadToDictionary<Weapon>();
            _abilities = LoadToDictionaryAndMapping<Ability>();
        }

        private static Dictionary<uint, T1> LoadToDictionaryAndMapping<T1>() where T1 : IGameObject
        {
            var dict = Utils.GetSubclassesOf<T1>().ToDictionary(k => k.Hash, k => k);
            dict.ToList().ForEach((KeyValuePair<uint, T1> k) => { 
                _hashes.Add(k.Value.GetType(), k.Key);
                _hashes_r.Add(k.Key, k.Value.GetType());
            });
            return dict;
        }

        private static Dictionary<uint, T1> LoadToDictionary<T1>() where T1 : IGameObject
        {
            return Utils.GetSubclassesOf<T1>().ToDictionary(k => k.Hash, k => k);
        }
    }
}
