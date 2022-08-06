
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;

namespace Shiang
{
    public abstract class TableDatabase<T1> : SQLiteDatabase
    {
        List<T1> _data = new List<T1>();

        public TableDatabase(string databaseName) : base(databaseName)
        {
        }

        public override object Data { get => _data; }

        public override void Retrieve()
        {
            // Clear out what _data is currectly holding
            // maybe implement a LRU cache
            _data = new List<T1>();
            ConnectAndRead(CommandStringRetrive(DatabaseName));
        }

        protected override void ConnectAndRead(string str)
        {
            using (var connection = new SqliteConnection(Name))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = str;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            _data.Add(RetrieveData(reader));
                        reader.Close();
                    }
                }

                connection.Close();
            }
        }

        // data retriving depends on how data are structed in each entry
        protected abstract T1 RetrieveData(IDataReader reader);
    }
}
