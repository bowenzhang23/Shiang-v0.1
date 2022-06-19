
using System.Data;

namespace Shiang
{
    public struct WeaponData : IDBData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Hash { get; set; }
        public int SpriteIndex { get; set; }
        public string AnimPattern { get; set; }
    }

    public class WeaponDB : Database<WeaponData>
    {
        public WeaponDB() : base("Weapon") { }

        public override string CommandStringCreate()
        {
            return "CREATE TABLE IF NOT EXISTS weapon (" +
                "name NVARCHAR(20), " +
                "description NVARCHAR(200), " +
                "hash INT UNSIGNED," +
                "spriteindex INT," +
                "animpattern VARCHAR(20));";
        }

        public override string CommandStringInsert(object entry)
        {
            WeaponData entryData = (WeaponData)entry;
            return "INSERT INTO weapon " +
                "(name, description, hash, spriteindex, animpattern) VALUES (" +
                $"'{entryData.Name}', " +
                $"'{entryData.Description}', " +
                $"'{entryData.Hash}', " +
                $"'{entryData.SpriteIndex}', " +
                $"'{entryData.AnimPattern}');";
        }

        public override string CommandStringRetrive => "SELECT * FROM weapon;";

        public override string CommandStringClear() => "DELETE FROM weapon;";

        protected override WeaponData RetrieveData(IDataReader reader)
        {
            return new WeaponData
            {
                Name = reader["name"].ToString(),
                Description = reader["description"].ToString(),
                Hash = (uint)reader["hash"],
                SpriteIndex = (int)reader["spriteindex"],
                AnimPattern = reader["animpattern"].ToString()
            };
        }
    }
}
