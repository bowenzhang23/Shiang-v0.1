
using NUnit.Framework;
using Shiang;

namespace ShiangTest
{
    public class TestAbilityContainer
    {
        [Test]
        public void ContainerConstruct()
        {
            AbilityContainer abilityCont = Utils.CreateAbilityContainer(2);

            Assert.IsTrue(abilityCont.IsEmpty());
            Assert.AreEqual(abilityCont.Capacity(), 2);
            Assert.IsFalse(abilityCont.Find(typeof(A1), out var _));
        }

        [Test]
        public void ContainerReceive()
        {
            AbilityContainer abilityCont = Utils.CreateAbilityContainer(6);
            bool fullSignalEmitted = false;
            abilityCont.OnFull += () => fullSignalEmitted = true;

            abilityCont.Receive(new GoldenScepter());
            Assert.AreEqual(abilityCont.Size(), 1);
            Assert.AreEqual(abilityCont.Capacity(), 6);

            abilityCont.Receive(new A1());
            abilityCont.Receive(new A1());
            Assert.AreEqual(abilityCont.Size(), 2);
            Assert.IsFalse(fullSignalEmitted);

            abilityCont.Receive(new A2());
            abilityCont.Receive(new A3());
            abilityCont.Receive(new A4());
            abilityCont.Receive(new A5());
            Assert.AreEqual(abilityCont.Size(), 6);
            Assert.IsFalse(fullSignalEmitted);
        }

        [Test]
        public void ContainerFull()
        {
            AbilityContainer abilityCont = Utils.CreateAbilityContainer(2);
            bool fullSignalEmitted = false;
            abilityCont.OnFull += () => fullSignalEmitted = true;

            abilityCont.Receive(new A1());
            abilityCont.Receive(new A2());
            abilityCont.Receive(new A3());
            abilityCont.Receive(new A4());
            Assert.AreEqual(abilityCont.Size(), 2);
            Assert.IsTrue(fullSignalEmitted);
            Assert.IsFalse(abilityCont.Find(typeof(A3), out var _));
            Assert.IsFalse(abilityCont.Find(typeof(A4), out var _));
            Assert.IsFalse(abilityCont.IsEmpty());
        }

        [Test]
        public void ContainerRemove()
        {
            AbilityContainer abilityCont = Utils.CreateAbilityContainer(2);
            abilityCont.Receive(new A1());
            abilityCont.Receive(new A2());
            abilityCont.Receive(new A3());
            abilityCont.Remove(typeof(A3));
            abilityCont.Remove(typeof(A4));
            Assert.AreEqual(abilityCont.Size(), 2);
            abilityCont.Remove(new A1());
            Assert.AreEqual(abilityCont.Size(), 1);
            Assert.IsFalse(abilityCont.Find(typeof(A1), out var _));
        }
    }
}