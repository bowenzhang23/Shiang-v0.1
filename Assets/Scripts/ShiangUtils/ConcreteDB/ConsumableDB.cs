
using System.Data;

namespace Shiang
{
    public struct ConsumableData : IDBData
    {
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
                "name NVARCHAR(20), " +
                "description NVARCHAR(200), " +
                "hash INT UNSIGNED," +
                "spriteindex INT);";
        }

        public override string CommandStringInsert(object entry)
        {
            ConsumableData entryData = (ConsumableData)entry;
            return "INSERT INTO consumable " +
                "(name, description, hash, spriteindex) VALUES (" +
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
                Name = reader["name"].ToString(),
                Description = reader["description"].ToString(),
                Hash = uint.Parse(reader["hash"].ToString()),
                SpriteIndex = int.Parse(reader["spriteindex"].ToString()),
            };
        }
    }
}
