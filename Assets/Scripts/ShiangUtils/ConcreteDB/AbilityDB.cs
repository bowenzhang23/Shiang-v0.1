
using System.Data;

namespace Shiang
{
    public struct AbilityData : IDBData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Hash { get; set; }
        public int SpriteIndex { get; set; }
        public string AnimPattern { get; set; }
    }

    public class AbilityDB : Database<AbilityData>
    {
        public AbilityDB() : base("Ability") { }

        public override string CommandStringCreate()
        {
            return "CREATE TABLE IF NOT EXISTS ability (" +
                "name NVARCHAR(20), " +
                "description NVARCHAR(200), " +
                "hash INT UNSIGNED," +
                "spriteindex INT," +
                "animpattern VARCHAR(20));";
        }

        public override string CommandStringInsert(object entry)
        {
            AbilityData entryData = (AbilityData)entry;
            return "INSERT INTO ability " +
                "(name, description, hash, spriteindex, animpattern) VALUES (" +
                $"'{entryData.Name}', " +
                $"'{entryData.Description}', " +
                $"'{entryData.Hash}', " +
                $"'{entryData.SpriteIndex}', " +
                $"'{entryData.AnimPattern}');";
        }

        public override string CommandStringRetrive => "SELECT * FROM ability;";

        public override string CommandStringClear() => "DELETE FROM ability;";

        protected override AbilityData RetrieveData(IDataReader reader)
        {
            return new AbilityData
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
