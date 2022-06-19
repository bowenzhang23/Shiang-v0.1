
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

            itemCont.Receive(new SixGod() { Count = 20 }, 10);
            itemCont.Receive(new SixGod(), 10); // Count = 1 by default
            Assert.AreEqual(itemCont.Size(), 1);
            Assert.AreEqual(itemCont.Count(), 11);
            Assert.AreEqual(itemCont.Capacity(), 6);

            itemCont.ReceiveAll(new I1() { Count = 10 });
            Assert.AreEqual(itemCont.Size(), 2);
            Assert.AreEqual(itemCont.Count(), 21);
            Assert.IsFalse(fullSignalEmitted);

            itemCont.Receive(new I2());
            itemCont.Receive(new I2());
            Assert.AreEqual(itemCont.Size(), 3);
            Assert.AreEqual(itemCont.Count(), 23);
            Assert.IsFalse(fullSignalEmitted);

            itemCont.Receive(new Ar1());
            Assert.AreEqual(itemCont.Size(), 4);
            Assert.AreEqual(itemCont.Count(), 24);

            itemCont.Receive(new Whip());
            itemCont.Receive(new W1());
            Assert.AreEqual(itemCont.Size(), 6);
            Assert.AreEqual(itemCont.Count(), 26);

            itemCont.Receive(new W1());
            itemCont.Receive(new W1());
            itemCont.Receive(new W1());
            itemCont.Receive(new W1());
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

            itemCont.Receive(new W1() { Count = 2 }, 2);
            itemCont.Receive(new Ar2() { Count = 2 }, 2);
            itemCont.Receive(new W3());
            itemCont.Receive(new Ar4());
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
            itemCont.Receive(new W1() { Count = 2 }, 2);
            itemCont.Receive(new Ar2() { Count = 2 }, 2);
            itemCont.Receive(new W3());
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
            var iCopy = i.Clone<I1>();
            Assert.AreEqual(iCopy.Count, 42);
            Assert.AreEqual(iCopy.Hash, i.Hash);

            Item w = new W1() { Count = 42 };
            var wCopy = w.Clone<W1>();
            Assert.AreEqual(wCopy.Count, 42);
            Assert.AreEqual(wCopy.Hash, w.Hash);

            Item a = new Ar1() { Count = 42 };
            var aCopy = a.Clone<Ar1>();
            Assert.AreEqual(aCopy.Count, 42);
            Assert.AreEqual(aCopy.Hash, a.Hash);
        }

        [Test]
        public void ContainerWeapons()
        {
            ItemContainer itemCont = Utils.CreateItemContainer(3);
            itemCont.Receive(new Whip());
            itemCont.Receive(new Whip());
            itemCont.Receive(new Whip());
            itemCont.Receive(new W1());
            itemCont.Receive(new W2());
            itemCont.Receive(new W3());
            itemCont.Receive(new W4());

            var weapons = itemCont.Weapons();
            var whip = (Weapon)weapons[0];

            Assert.AreEqual(weapons.Count, 3);
            Assert.IsNotNull(whip);
            Assert.AreEqual(whip.Hash, 0x00000000);
            Assert.IsNull(weapons.Find(w => w.Hash == new W3().Hash));
        }
    }
}