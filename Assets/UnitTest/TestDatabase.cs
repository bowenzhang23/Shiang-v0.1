
using Shiang;
using NUnit.Framework;
using System.Collections.Generic;

namespace ShiangTest
{
    public class TestDatabase
    {
        [Test]
        public void CreateDatabase()
        {
            var resourcePathDB = Utils.CreateSQLiteDatabase<ResourcePathDB>();
            var consumableDB = Utils.CreateSQLiteDatabase<ConsumableDB>();
            var weaponDB = Utils.CreateSQLiteDatabase<WeaponDB>();
            var abilityDB = Utils.CreateSQLiteDatabase<AbilityDB>();
            var entityDB = Utils.CreateSQLiteDatabase<EntityDB>("TestingEntity");
            
            Assert.IsNotNull(resourcePathDB);
            Assert.IsNotNull(consumableDB);
            Assert.IsNotNull(weaponDB);
            Assert.IsNotNull(abilityDB);
            Assert.IsNotNull(entityDB);
        }

        [Test]
        public void WriteToResourcePathDB()
        {
            var db = Utils.CreateSQLiteDatabase<ResourcePathDB>();
            db.Clear(); // clear first
            db.Insert(new ResourcePathData() { Name = "PlayerAnimClips", Path = "Anims/Player" });
            db.Insert(new ResourcePathData() { Name = "RabbitAnimClips", Path = "Anims/Rabbit" });
            db.Insert(new ResourcePathData() { Name = "AbilityAnimClips", Path = "Anims/Ability" });
            db.Insert(new ResourcePathData() { Name = "WeaponAnimClips", Path = "Anims/Weapon" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-1", Path = "Arts/Icons/Icons-1" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-2", Path = "Arts/Icons/Icons-2" });
            Assert.AreEqual(((List<ResourcePathData>)db.Data).Count, 0);
            db.Retrieve();
            Assert.AreEqual(((List<ResourcePathData>)db.Data).Count, 6);
        }

        [Test]
        public void WriteToWeaponDB()
        {
            var db = Utils.CreateSQLiteDatabase<WeaponDB>();
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
            Assert.AreEqual(((List<WeaponData>)db.Data).Count, 0);
            db.Retrieve();
            Assert.AreEqual(((List<WeaponData>)db.Data).Count, 3);
        }

        [Test]
        public void WriteToAbilityDB()
        {
            var db = Utils.CreateSQLiteDatabase<AbilityDB>();
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
            Assert.AreEqual(((List<AbilityData>)db.Data).Count, 0);
            db.Retrieve();
            Assert.AreEqual(((List<AbilityData>)db.Data).Count, 1);
        }

        [Test]
        public void WriteToConsumableDB()
        {
            var db = Utils.CreateSQLiteDatabase<ConsumableDB>();
            db.Clear(); // clear first
            db.Insert(new ConsumableData()
            {
                ClassID="SixDemon",
                Name = "六魔",
                Description = "沁人心脾",
                Hash = 0x10000,
                SpriteIndex = 12
            });
            Assert.AreEqual(((List<ConsumableData>)db.Data).Count, 0);
            db.Retrieve();
            Assert.AreEqual(((List<ConsumableData>)db.Data).Count, 1);
        }

        [Test]
        public void WriteToEntityDB()
        {
            var db = Utils.CreateSQLiteDatabase<EntityDB>("RanRan");
            var itemsToInsert = new Dictionary<uint, int>();
            var abilitiesToInsert = new List<uint>();

            itemsToInsert.Add(0x10000, 10); // SixDemon
            itemsToInsert.Add(0xEFFFF, 1); // Fist
            itemsToInsert.Add(0xE0000, 1); // Whip

            abilitiesToInsert.Add(0xA0000);

            db.Clear(); // clear first
            db.Insert(new EntityData() { 
                Items = itemsToInsert, 
                Abilities = abilitiesToInsert 
            });
            
            Assert.IsNull(((EntityData)db.Data).Items);
            db.Retrieve();
            Assert.AreEqual(((EntityData)db.Data).Items.Count, 3);
            Assert.AreEqual(((EntityData)db.Data).Abilities.Count, 1);
        }

        [Test]
        public void WriteToEntityDBForFridge()
        {
            var db = Utils.CreateSQLiteDatabase<EntityDB>("Fridge-Test");
            var itemsToInsert = new Dictionary<uint, int>();
            var abilitiesToInsert = new List<uint>();

            itemsToInsert.Add(0x10000, 5); // SixDemon
            itemsToInsert.Add(0xE0000, 1); // Whip

            db.Clear(); // clear first
            db.Insert(new EntityData()
            {
                Items = itemsToInsert,
                Abilities = abilitiesToInsert
            });

            Assert.IsNull(((EntityData)db.Data).Items);
            db.Retrieve();
            Assert.AreEqual(((EntityData)db.Data).Items.Count, 2);
            Assert.AreEqual(((EntityData)db.Data).Abilities.Count, 0);
        }
    }
}
