
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
            db.Insert(new ResourcePathData() { Name = "PlayerAnimClips", Path = "Anims/Player" });
            db.Insert(new ResourcePathData() { Name = "RabbitAnimClips", Path = "Anims/Rabbit" });
            db.Insert(new ResourcePathData() { Name = "AbilityAnimClips", Path = "Anims/Ability" });
            db.Insert(new ResourcePathData() { Name = "WeaponAnimClips", Path = "Anims/Weapon" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-1", Path = "Arts/Items/Icons-1" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-2", Path = "Arts/Items/Icons-2" });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 6);
        }

        [Test]
        public void WriteToWeaponDB()
        {
            var db = Utils.CreateDatabase<WeaponDB>();
            db.Clear(); // clear first
            db.Insert(new WeaponData()
            {
                ClassID = "Fist",
                Name = " 拳头",
                Description = "朝我拳头跑来，我懒得过去打你",
                AnimPattern = "Attack",
                CdTime = 1.1f,
                Hash = 0xEFFFF,
                SpriteIndex = 11
            });
            db.Insert(new WeaponData() { 
                ClassID="Whip",
                Name="长鞭", 
                Description="从小练习挥鞭", 
                AnimPattern="Whip", 
                CdTime=1.1f, 
                Hash=0xE0000, 
                SpriteIndex=0 
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Axe",
                Name = "开山斧",
                Description = "祖传开山神斧",
                AnimPattern = "Axe",
                CdTime = 2f,
                Hash = 0xE0001,
                SpriteIndex = 14
            });
            Assert.AreEqual(db.Data.Count, 0);
            db.Retrieve();
            Assert.AreEqual(db.Data.Count, 3);
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
