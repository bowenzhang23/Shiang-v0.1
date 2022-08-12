
using System.Data;

namespace Shiang
{
    public struct AbilityData : IDBData
    {
        public string ClassID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Hash { get; set; }
        public int SpriteIndex { get; set; }
        public string AnimPattern { get; set; }
        public string SoundtrackName { get; set; }
        public float CdTime { get; set; }
    }

    public class AbilityDB : TableDatabase<AbilityData>
    {
        public AbilityDB() : base("Ability") { }

        public override string CommandStringCreate()
        {
            return "CREATE TABLE IF NOT EXISTS ability (" +
                "classid VARCHAR(20), " +
                "name NVARCHAR(20), " +
                "description NVARCHAR(200), " +
                "hash INT UNSIGNED, " +
                "spriteindex INT ," +
                "animpattern VARCHAR(20), " +
                "soundtrackname NVARCHAR(20), " +
                "cdtime FLOAT);";
        }

        public override string CommandStringInsert(object entry)
        {
            AbilityData entryData = (AbilityData)entry;
            return "INSERT INTO ability " +
                "(classid, name, description, hash, spriteindex, animpattern, soundtrackname, cdtime) VALUES (" +
                $"'{entryData.ClassID}', " +
                $"'{entryData.Name}', " +
                $"'{entryData.Description}', " +
                $"'{entryData.Hash}', " +
                $"'{entryData.SpriteIndex}', " +
                $"'{entryData.AnimPattern}', " +
                $"'{entryData.SoundtrackName}', " +
                $"'{entryData.CdTime}');";
        }

        public override string CommandStringRetrive(string what) 
            => $"SELECT * FROM {what};";

        public override string CommandStringClear() 
            => "DELETE FROM ability;";

        protected override AbilityData RetrieveData(IDataReader reader)
        {
            return new AbilityData
            {
                ClassID = reader["classid"].ToString(),
                Name = reader["name"].ToString(),
                Description = reader["description"].ToString(),
                Hash = uint.Parse(reader["hash"].ToString()),
                SpriteIndex = int.Parse(reader["spriteindex"].ToString()),
                AnimPattern = reader["animpattern"].ToString(),
                SoundtrackName = reader["soundtrackname"].ToString(),
                CdTime = float.Parse(reader["cdtime"].ToString()),
            };
        }
    }
}
