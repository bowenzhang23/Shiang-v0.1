
using NUnit.Framework;
using Shiang;

namespace ShiangTest
{
    public class TestItemContainer
    {
        [Test]
        public void ContainerConstruct()
        {
            ItemContainer itemCont = Utils.CreateItemContainer(6);

            Assert.IsTrue(itemCont.IsEmpty());
            Assert.AreEqual(itemCont.Count(), 0);
            Assert.AreEqual(itemCont.Capacity(), 6);
            Assert.IsFalse(itemCont.Find(typeof(I1), out var _));
        }

        [Test]
        public void ContainerReceive()
        {
            ItemContainer itemCont = Utils.CreateItemContainer(6);
            bool fullSignalEmitted = false;
            itemCont.OnFull += () => fullSignalEmitted = true;

            Item sixGod = new SixDemon() { Count = 20 };
            itemCont.Receive(ref sixGod, 10); // Count = 1 by default
            itemCont.Receive(ref sixGod); // Count = 1 by default
            Assert.AreEqual(itemCont.Size(), 1);
            Assert.AreEqual(itemCont.Count(), 11);
            Assert.AreEqual(itemCont.Capacity(), 6);

            Item i1 = new I1() { Count = 10 };
            itemCont.ReceiveAll(ref i1);
            Assert.AreEqual(itemCont.Size(), 2);
            Assert.AreEqual(itemCont.Count(), 21);
            Assert.IsFalse(fullSignalEmitted);

            Item i2 = new I2() { Count = 2 };
            itemCont.Receive(ref i2);
            itemCont.Receive(ref i2);
            Assert.AreEqual(itemCont.Size(), 3);
            Assert.AreEqual(itemCont.Count(), 23);
            Assert.IsFalse(fullSignalEmitted);

            Item ar1 = new Ar1();
            itemCont.Receive(ref ar1);
            Assert.AreEqual(itemCont.Size(), 4);
            Assert.AreEqual(itemCont.Count(), 24);

            Item whip = new Whip();
            itemCont.Receive(ref whip);
            Item w1 = new W1() { Count = 10 };
            itemCont.Receive(ref w1);
            Assert.AreEqual(itemCont.Size(), 6);
            Assert.AreEqual(itemCont.Count(), 26);

            itemCont.Receive(ref w1);
            itemCont.Receive(ref w1);
            itemCont.Receive(ref w1);
            itemCont.Receive(ref w1);
            Assert.AreEqual(itemCont.Size(), 6);
            Assert.AreEqual(itemCont.Count(), 30);
            Assert.IsFalse(fullSignalEmitted); 
        }

        [Test]
        public void ContainerFull()
        {
            ItemContainer itemCont = Utils.CreateItemContainer(2);
            bool fullSignalEmitted = false;
            itemCont.OnFull += () => fullSignalEmitted = true;

            Item w1 = new W1() { Count = 2 };
            Item ar2 = new Ar2() { Count = 2 };
            Item w3 = new W3();
            Item ar4 = new Ar4();
            itemCont.Receive(ref w1, 2);
            itemCont.Receive(ref ar2, 2);
            itemCont.Receive(ref w3);
            itemCont.Receive(ref ar4);
            Assert.AreEqual(itemCont.Size(), 2);
            Assert.AreEqual(itemCont.Count(), 4);
            Assert.IsTrue(fullSignalEmitted);
            Assert.IsFalse(itemCont.Find(typeof(W3), out var _));
            Assert.IsFalse(itemCont.Find(typeof(Ar4), out var _));
            Assert.IsFalse(itemCont.IsEmpty());
        }

        [Test]
        public void ContainerRemove()
        {
            ItemContainer itemCont = Utils.CreateItemContainer(2);
            Item w1 = new W1() { Count = 2 };
            Item ar2 = new Ar2() { Count = 2 };
            Item w3 = new W3();
            itemCont.Receive(ref w1, 2);
            itemCont.Receive(ref ar2, 2);
            itemCont.Receive(ref w3);
            itemCont.Remove(typeof(W3));
            itemCont.Remove(typeof(W4));
            Assert.AreEqual(itemCont.Size(), 2);
            Assert.AreEqual(itemCont.Count(), 4);
            itemCont.Remove(new W1());
            Assert.AreEqual(itemCont.Size(), 1);
            Assert.AreEqual(itemCont.Count(), 2);
            Assert.IsFalse(itemCont.Find(typeof(W1), out var _));
        }

        [Test]
        public void ItemClone()
        {
            Item i = new I1() { Count = 42 };
            var iCopy = i.Clone();
            Assert.AreEqual(iCopy.Count, 42);
            Assert.AreEqual(iCopy.Hash, i.Hash);

            Item w = new W1() { Count = 42 };
            var wCopy = w.Clone();
            Assert.AreEqual(wCopy.Count, 42);
            Assert.AreEqual(wCopy.Hash, w.Hash);

            Item a = new Ar1() { Count = 42 };
            var aCopy = a.Clone();
            Assert.AreEqual(aCopy.Count, 42);
            Assert.AreEqual(aCopy.Hash, a.Hash);
        }

        [Test]
        public void ContainerWeapons()
        {
            ItemContainer itemCont = Utils.CreateItemContainer(3);
            Item whip1 = new Whip() { Count = 3 };
            Item w1 = new W1();
            Item w2 = new W2();
            Item w3 = new W3();
            Item w4 = new W4();
            itemCont.Receive(ref whip1);
            itemCont.Receive(ref whip1);
            itemCont.Receive(ref whip1);
            itemCont.Receive(ref w1);
            itemCont.Receive(ref w2);
            itemCont.Receive(ref w3);
            itemCont.Receive(ref w4);

            var weapons = itemCont.Weapons();
            var whip = (Weapon)weapons[0];

            Assert.AreEqual(weapons.Count, 3);
            Assert.IsNotNull(whip);
            Assert.AreEqual(whip.ClassID, "Whip");
            Assert.IsNull(weapons.Find(w => w.Hash == new W3().Hash));
        }

        [Test]
        public void ItemTransfer()
        {
            Item i1 = new I1() { Count = 10 };
            ItemContainer itemCont = new ItemContainer(10);
            itemCont.ReceiveAll(ref i1);
            Assert.IsNull(i1);
        }
    }
}