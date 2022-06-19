
using Shiang;
using NUnit.Framework;

namespace ShiangTest
{
    public class TestDatabase
    {
        [Test]
        public void CreateDatabase()
        {
            var resourcePathDB = Utils.CreateDatabase<ResourcePathDB>();
            var comsumableDB = Utils.CreateDatabase<ConsumableDB>();
            var weaponDB = Utils.CreateDatabase<WeaponDB>();
            var abilityDB = Utils.CreateDatabase<AbilityDB>();
            Assert.IsNotNull(resourcePathDB);
            Assert.IsNotNull(comsumableDB);
            Assert.IsNotNull(weaponDB);
            Assert.IsNotNull(abilityDB);
        }

        [Test]
        public void WriteToDatabase()
        {
            var db = Utils.CreateDatabase<ResourcePathDB>();
            db.Clear(); // clear first
            db.Insert(new ResourcePathData() { Name = "PlayerAnimClips", Path = "Anims/Player" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-1", Path = "Arts/Items/Icons-1" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-2", Path = "Arts/Items/Icons-2" });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 3);
        }
    }
}
