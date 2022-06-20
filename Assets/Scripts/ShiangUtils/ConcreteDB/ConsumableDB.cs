
using System.Data;

namespace Shiang
{
    public struct ConsumableData : IDBData
    {
        public string ClassID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Hash { get; set; }
        public int SpriteIndex { get; set; }
    }

    public class ConsumableDB : Database<ConsumableData>
    {
        public ConsumableDB() : base("Consumable") { }

        public override string CommandStringCreate()
        {
            return "CREATE TABLE IF NOT EXISTS consumable (" +
                "classid VARCHAR(20), " +
                "name NVARCHAR(20), " +
                "description NVARCHAR(200), " +
                "hash INT UNSIGNED," +
                "spriteindex INT);";
        }

        public override string CommandStringInsert(object entry)
        {
            ConsumableData entryData = (ConsumableData)entry;
            return "INSERT INTO consumable " +
                "(classid, name, description, hash, spriteindex) VALUES (" +
                $"'{entryData.ClassID}', " +
                $"'{entryData.Name}', " +
                $"'{entryData.Description}', " +
                $"'{entryData.Hash}', " +
                $"'{entryData.SpriteIndex}');";
        }

        public override string CommandStringRetrive => "SELECT * FROM consumable;";

        public override string CommandStringClear() => "DELETE FROM consumable;";

        protected override ConsumableData RetrieveData(IDataReader reader)
        {
            return new ConsumableData
            {
                ClassID = reader["classid"].ToString(),
                Name = reader["name"].ToString(),
                Description = reader["description"].ToString(),
                Hash = uint.Parse(reader["hash"].ToString()),
                SpriteIndex = int.Parse(reader["spriteindex"].ToString()),
            };
        }
    }
}
