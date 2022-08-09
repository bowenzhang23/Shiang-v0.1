
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
                ClassID = "Whip",
                Name = "长鞭",
                Description = "从小练习挥鞭",
                AnimPattern = "Whip",
                CdTime = 1.1f,
                Hash = 0xE2000,
                SpriteIndex = 0
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Fist",
                Name = " 拳头",
                Description = "朝我拳头跑来，我懒得过去打你",
                AnimPattern = "Attack",
                CdTime = 1.1f,
                Hash = 0xE1011,
                SpriteIndex = 11
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Axe",
                Name = "开山斧",
                Description = "祖传开山神斧",
                AnimPattern = "Axe",
                CdTime = 2f,
                Hash = 0xE1014,
                SpriteIndex = 14
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Broadsword",
                Name = "跋扈的大刀",
                Description = "一刀一个不管埋！",
                AnimPattern = "Broadsword",
                CdTime = 4f,
                Hash = 0xEE000,
                SpriteIndex = 11
            });
            Assert.AreEqual(((List<WeaponData>)db.Data).Count, 0);
            db.Retrieve();
            Assert.AreEqual(((List<WeaponData>)db.Data).Count, 4);
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
                AnimPattern = "GoldenScepter",
                CdTime = 5f,
                Hash = 0xA1012,
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
                ClassID = "OpuntiaFruit",
                Name = "仙人掌果",
                Description = "防风固土",
                Hash = 0x10024,
                SpriteIndex = 24
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "WangWangMilk",
                Name = "旺旺牛奶",
                Description = "茁壮成长",
                Hash = 0x10025,
                SpriteIndex = 25
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "SixDemon",
                Name = "六魔",
                Description = "沁人心脾",
                Hash = 0x10026,
                SpriteIndex = 26
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "LemonSoda",
                Name = "青柠苏打",
                Description = "安神解毒",
                Hash = 0x10027,
                SpriteIndex = 27
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "SnowCream",
                Name = "雪人雪糕",
                Description = "开始融化，面目狰狞",
                Hash = 0x10028,
                SpriteIndex = 28
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "LongJing",
                Name = "西湖龙井",
                Description = "清新健康，务必用此杯引用",
                Hash = 0x10029,
                SpriteIndex = 29
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "Ticket",
                Name = "门票",
                Description = "成人20，儿童500",
                Hash = 0x20001,
                SpriteIndex = 1
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "Bonbon",
                Name = "便携糖果",
                Description = "每次两粒，每日十次",
                Hash = 0x20003,
                SpriteIndex = 3
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "Mints",
                Name = "薄荷糖",
                Description = "空罐，留有余香",
                Hash = 0x20004,
                SpriteIndex = 4
            });
            Assert.AreEqual(((List<ConsumableData>)db.Data).Count, 0);
            db.Retrieve();
            Assert.AreEqual(((List<ConsumableData>)db.Data).Count, 9);
        }

        [Test]
        public void WriteToEntityDB()
        {
            var db = Utils.CreateSQLiteDatabase<EntityDB>("RanRan");
            var itemsToInsert = new Dictionary<uint, int>();
            var abilitiesToInsert = new List<uint>();

            itemsToInsert.Add(0x10026, 10); // SixDemon
            itemsToInsert.Add(0xE2000, 1); // Whip

            abilitiesToInsert.Add(0xA1012);

            db.Clear(); // clear first
            db.Insert(new EntityData() { 
                Items = itemsToInsert, 
                Abilities = abilitiesToInsert 
            });
            
            Assert.IsNull(((EntityData)db.Data).Items);
            db.Retrieve();
            Assert.AreEqual(((EntityData)db.Data).Items.Count, 2);
            Assert.AreEqual(((EntityData)db.Data).Abilities.Count, 1);
        }

        [Test]
        public void WriteToEntityDBForFridge()
        {
            var db = Utils.CreateSQLiteDatabase<EntityDB>("Fridge-Test");
            var itemsToInsert = new Dictionary<uint, int>();
            var abilitiesToInsert = new List<uint>();

            itemsToInsert.Add(0x10024, 5); // 
            itemsToInsert.Add(0x10025, 5); // 
            itemsToInsert.Add(0x10026, 5); // 
            itemsToInsert.Add(0x10027, 5); // 
            itemsToInsert.Add(0x10028, 5); // 
            itemsToInsert.Add(0x10029, 5); // 
            itemsToInsert.Add(0x20001, 5); // 
            itemsToInsert.Add(0x20003, 5); // 
            itemsToInsert.Add(0x20004, 5); // 
            itemsToInsert.Add(0xE2000, 1); // Whip

            db.Clear(); // clear first
            db.Insert(new EntityData()
            {
                Items = itemsToInsert,
                Abilities = abilitiesToInsert
            });

            Assert.IsNull(((EntityData)db.Data).Items);
            db.Retrieve();
            Assert.AreEqual(((EntityData)db.Data).Items.Count, 10);
            Assert.AreEqual(((EntityData)db.Data).Abilities.Count, 0);
        }
    }
}
