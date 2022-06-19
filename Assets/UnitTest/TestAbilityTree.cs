
using NUnit.Framework;
using Shiang;

namespace ShiangTest
{
    public class TestAbilityTree
    {
        [Test]
        public void TreeConstructionCommon()
        {
            var tree = Utils.CreateAbilityTree(new GoldenScepter());
            Assert.AreEqual(tree.Size, 1);
            tree.AddAbilityNextTo<A1>(typeof(GoldenScepter));
            tree.AddAbilityNextTo<A2>(typeof(GoldenScepter));
            tree.AddAbilityNextTo<A3>(typeof(GoldenScepter));
            Assert.AreEqual(tree.Size, 4);
            Assert.AreEqual(tree.FindNode(typeof(GoldenScepter)).followingNodes.Count, 3);
            tree.AddAbilityNextTo<A4>(typeof(A1));
            tree.AddAbilityNextTo<A5>(typeof(A1));
            tree.AddAbilityNextTo<A6>(typeof(A2));
            tree.AddAbilityNextTo<A7>(typeof(A6));
            tree.AddAbilityNextTo<A8>(typeof(A6));
            Assert.AreEqual(tree.Size, 9);
            Assert.AreEqual(tree.FindNode(typeof(A6)).followingNodes.Count, 2);
        }

        [Test]
        public void TreeConstructionNotExist()
        {
            var tree = Utils.CreateAbilityTree(new GoldenScepter());
            Assert.AreEqual(tree.Size, 1);
            tree.AddAbilityNextTo<A2>(typeof(A1));
            Assert.AreEqual(tree.Size, 1);
        }

        [Test]
        public void TreeConstructionExist()
        {
            var tree = Utils.CreateAbilityTree(new GoldenScepter());
            Assert.AreEqual(tree.Size, 1);
            tree.AddAbilityNextTo<A1>(typeof(GoldenScepter));
            tree.AddAbilityNextTo<A1>(typeof(GoldenScepter));
            tree.AddAbilityNextTo<A1>(typeof(GoldenScepter));
            Assert.AreEqual(tree.Size, 2);
        }
    }
}