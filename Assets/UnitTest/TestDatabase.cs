
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
            var consumableDB = Utils.CreateDatabase<ConsumableDB>();
            var weaponDB = Utils.CreateDatabase<WeaponDB>();
            var abilityDB = Utils.CreateDatabase<AbilityDB>();
            Assert.IsNotNull(resourcePathDB);
            Assert.IsNotNull(consumableDB);
            Assert.IsNotNull(weaponDB);
            Assert.IsNotNull(abilityDB);
        }

        [Test]
        public void WriteToResourcePathDB()
        {
            var db = Utils.CreateDatabase<ResourcePathDB>();
            db.Clear(); // clear first
            db.Insert(new ResourcePathData() { Name="PlayerAnimClips", Path="Anims/Player" });
            db.Insert(new ResourcePathData() { Name="SpritesIcon-1", Path="Arts/Items/Icons-1" });
            db.Insert(new ResourcePathData() { Name="SpritesIcon-2", Path="Arts/Items/Icons-2" });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 3);
        }

        [Test]
        public void WriteToWeaponDB()
        {
            var db = Utils.CreateDatabase<WeaponDB>();
            db.Clear(); // clear first
            db.Insert(new WeaponData() { 
                ClassID="Whip",
                Name="长鞭", 
                Description="从小练习挥鞭", 
                AnimPattern="Attack", 
                CdTime=1.1f, 
                Hash=0xE0000, 
                SpriteIndex=0 });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 1);
        }

        [Test]
        public void WriteToAbilityDB()
        {
            var db = Utils.CreateDatabase<AbilityDB>();
            db.Clear(); // clear first
            db.Insert(new AbilityData()
            {
                ClassID="GoldenScepter",
                Name = "黄金权杖",
                Description = "堆中法器",
                AnimPattern = "Magic",
                CdTime = 5f,
                Hash = 0xA0000,
                SpriteIndex = 12
            });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 1);
        }

        [Test]
        public void WriteToConsumableDB()
        {
            var db = Utils.CreateDatabase<ConsumableDB>();
            db.Clear(); // clear first
            db.Insert(new ConsumableData()
            {
                ClassID="SixDemon",
                Name = "六魔",
                Description = "沁人心脾",
                Hash = 0x10000,
                SpriteIndex = 12
            });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 1);
        }
    }
}
