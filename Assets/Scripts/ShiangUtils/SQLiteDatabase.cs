
using Mono.Data.Sqlite;
using System;
using System.Data;

namespace Shiang
{
    public abstract class SQLiteDatabase : IDatabase
    {
        string _name = "...";
        string _databaseName = "...";

        protected string Name { get => _name; }
        protected string DatabaseName { get => _databaseName; }
        public abstract object Data { get; }

        public SQLiteDatabase(string databaseName)
        {
            _databaseName = databaseName.ToLower();
            _name = $"URI=file:Databases/{databaseName}.db";
        }

        public abstract string CommandStringCreate();

        public abstract string CommandStringInsert(object entry);

        public abstract string CommandStringRetrive(string what);

        public abstract string CommandStringClear();

        // write are straightforwardly creating tables and
        // inserting/clear things from those tables
        // here standard ways are provided

        public virtual void Create()
            => ConnectAndWrite(CommandStringCreate());

        public virtual void Insert(object entry)
            => ConnectAndWrite(CommandStringInsert(entry));

        public virtual void Clear()
            => ConnectAndWrite(CommandStringClear());

        protected virtual void ConnectAndWrite(string str)
        {
            using (var connection = new SqliteConnection(Name))
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

        protected virtual void ConnectAndRead(string str) { }

        // retrieve depends on how datas are structured
        // no standard ways can be provided
        public abstract void Retrieve();
    }
}
