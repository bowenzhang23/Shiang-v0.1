
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Shiang
{
    public class EntityData : IDBData
    {
        // TODO stats

        public Dictionary<uint, int> Items { get; set; }

        public List<uint> Abilities { get; set; }
    }

    /// <summary>
    /// This class stores the following information of an entity
    /// 1) stats (TODO)
    /// 2) items to serialise the inventory
    /// 3) abilities to serialise the ability container
    /// 
    /// At runtime, the data in the database are serialised and 
    /// the actual classes will hold those information.
    /// Once that happen, the database will be cleared at runtime
    /// and only persist the data again when the game exit. 
    /// (Or the game is explicitly saved by the user?)
    /// 
    /// </summary>
    public class EntityDB : SQLiteDatabase
    {
        EntityData _data = new EntityData();

        public EntityDB(string databaseName) : base(databaseName)
        {
        }

        public override object Data { get => _data; }

        public override string CommandStringRetrive(string what)
            => $"SELECT * FROM {what};";

        public override string CommandStringClear()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM items;");
            sb.AppendLine("DELETE FROM abilities;");
            return sb.ToString();
        }

        public override string CommandStringCreate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CREATE TABLE IF NOT EXISTS items (" +
                "hash VARCHAR(20), count INT UNSIGNED);");
            sb.AppendLine("CREATE TABLE IF NOT EXISTS abilities (" +
                "hash VARCHAR(20));");
            return sb.ToString();
        }

        public override string CommandStringInsert(object entry)
        {
            StringBuilder sb = new StringBuilder();
            EntityData entryData = (EntityData)entry;
            
            foreach (var item in entryData.Items)
                sb.AppendLine("INSERT INTO items " +
                    "(hash, count) VALUES (" +
                    $"'{item.Key}', '{item.Value}');");

            foreach (var ability in entryData.Abilities)
                sb.AppendLine("INSERT INTO abilities (hash) VALUES (" +
                    $"'{ability}');");

            return sb.ToString();
        }

        public override void Retrieve()
        {
            // Clear out what _data is currectly holding
            _data = new EntityData();
            ConnectAndRead(CommandStringRetrive("items"), RetrieveItems);
            ConnectAndRead(CommandStringRetrive("abilities"), RetrieveAbilities);
        }

        protected void ConnectAndRead(string str, Func<IDataReader, bool> retrieveFunction)
        {
            using (var connection = new SqliteConnection(Name))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = str;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        retrieveFunction(reader);
                        reader.Close();
                    }
                }

                connection.Close();
            }
        }

        private bool RetrieveItems(IDataReader reader)
        {
            _data.Items = new Dictionary<uint, int>();
            while (reader.Read() && _data.Items.Count < GameMechanism.INVENTORY_CAPACITY)
                _data.Items.Add(uint.Parse(reader["hash"].ToString()), 
                    int.Parse(reader["count"].ToString()));

            return true;
        }

        private bool RetrieveAbilities(IDataReader reader)
        {
            _data.Abilities = new List<uint>();
            while (reader.Read() && _data.Items.Count < GameMechanism.ABILITY_CAPACITY)
                _data.Abilities.Add(uint.Parse(reader["hash"].ToString()));

            return true;
        }
    }
}
