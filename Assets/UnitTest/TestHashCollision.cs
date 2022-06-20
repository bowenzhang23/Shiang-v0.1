
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Shiang;

namespace ShiangTest
{
    public class TestHashCollision
    {
        List<Item> _items;
        List<Ability> _abilities;
        List<IGameObject> _gameObjects;

        [Test]
        public void ItemCollision()
        {
            _items = Utils.GetSubclassesOf<Item>();
            Debug.Log($"{_items.Count} items found");
            List<uint> hashes = _items.Select(k => k.Hash).ToList();
            HashSet<uint> setOfHashes = new HashSet<uint>(hashes);

            Assert.AreEqual(hashes.Count, setOfHashes.Count);
        }

        [Test]
        public void AbilityCollision()
        {
            _abilities = Utils.GetSubclassesOf<Ability>();
            Debug.Log($"{_abilities.Count} abilities found");
            List<uint> hashes = _abilities.Select(k => k.Hash).ToList();
            HashSet<uint> setOfHashes = new HashSet<uint>(hashes);

            Assert.AreEqual(hashes.Count, setOfHashes.Count);
        }

        [Test]
        public void OverallCollision()
        {
            _gameObjects = new List<IGameObject>();
            _gameObjects.AddRange(Utils.GetSubclassesOf<Item>());
            _gameObjects.AddRange(Utils.GetSubclassesOf<Ability>());
            Debug.Log($"{_gameObjects.Count} gameObjects found");
            List<uint> hashes = _gameObjects.Select(k => k.Hash).ToList();
            HashSet<uint> setOfHashes = new HashSet<uint>(hashes);

            Assert.AreEqual(hashes.Count, setOfHashes.Count);
        }
    }
}