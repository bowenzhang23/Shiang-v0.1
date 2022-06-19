
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data;

namespace Shiang
{
    public abstract class Database<T1> : IDatabase
    {
        string _name = "...";
        List<T1> _data = new List<T1>();

        public Database(string databaseName)
        {
            _name = $"URI=file:Databases/{databaseName}.db";
        }
        public List<T1> Data { get => _data; }

        public abstract string CommandStringCreate();

        public abstract string CommandStringInsert(object entry);

        public abstract string CommandStringRetrive { get; }

        public abstract string CommandStringClear();

        protected abstract T1 RetrieveData(IDataReader reader);
       
        public void Create() 
            => ConnectAndWrite(CommandStringCreate());

        public void Insert(object entry)
            => ConnectAndWrite(CommandStringInsert(entry));

        public void Retrieve()
            => ConnectAndRead(CommandStringRetrive);

        public void Clear()
            => ConnectAndWrite(CommandStringClear());

        private void ConnectAndWrite(string str)
        {
            using (var connection = new SqliteConnection(_name))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = str;
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void ConnectAndRead(string str)
        {
            // Clear out what _data is currectly holding
            // maybe implement a LRU cache
            _data = new List<T1>();
            using (var connection = new SqliteConnection(_name))
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
    }
}
