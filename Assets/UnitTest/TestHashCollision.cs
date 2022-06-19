
using NUnit.Framework;
using Shiang;
using System.Collections.Generic;

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
            _items = Utils.GetListOfType<Item>();
            HashSet<Item> itemSet = new HashSet<Item>(_items);

            Assert.AreEqual(_items.Count, itemSet.Count);
        }

        [Test]
        public void AbilityCollision()
        {
            _abilities = Utils.GetListOfType<Ability>();
            HashSet<Ability> abilitySet = new HashSet<Ability>(_abilities);

            Assert.AreEqual(_abilities.Count, abilitySet.Count);
        }

        [Test]
        public void OverallCollision()
        {
            _gameObjects = Utils.GetListOfType<IGameObject>();
            HashSet<IGameObject> goSet = new HashSet<IGameObject>(_gameObjects);

            Assert.AreEqual(_gameObjects.Count, goSet.Count);
        }
    }
}