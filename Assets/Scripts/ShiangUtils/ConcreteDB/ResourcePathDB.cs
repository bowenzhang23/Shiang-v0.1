
using System.Data;

namespace Shiang
{
    public struct ResourcePathData : IDBData
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class ResourcePathDB : Database<ResourcePathData>
    {
        public ResourcePathDB() : base("ResourcePath") { }

        public override string CommandStringCreate()
        {
            return "CREATE TABLE IF NOT EXISTS resourcepath (" +
                "name VARCHAR(20), " +
                "path VARCHAR(200));";
        }

        public override string CommandStringInsert(object entry)
        {
            ResourcePathData entryData = (ResourcePathData)entry;
            return "INSERT INTO resourcepath (name, path) VALUES" +
                $"('{entryData.Name}', '{entryData.Path}');";
        }

        public override string CommandStringRetrive => "SELECT * FROM resourcepath;";

        public override string CommandStringClear() => "DELETE FROM resourcepath;";

        protected override ResourcePathData RetrieveData(IDataReader reader)
        {
            return new ResourcePathData { 
                Name = reader["name"].ToString(), 
                Path = reader["path"].ToString(), 
            };
        }
    }
}
