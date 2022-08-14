
using Mono.Data.Sqlite;
using System;
using System.IO;
using System.Data;
using UnityEngine;

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
            Debug.Log($"Creating {_databaseName}");
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (!Directory.Exists($"{Application.dataPath}/Databases/"))
                Directory.CreateDirectory($"{Application.dataPath}/Databases/");
            _name = $"URI=file:{Application.dataPath}/Databases/{databaseName}.db";
#else
            Debug.Log($"Creating {_databaseName} - android");
            if (!Directory.Exists($"{Application.persistentDataPath}/Databases/"))
                Directory.CreateDirectory($"{Application.persistentDataPath}/Databases/");
            _name = $"URI=file:{Application.persistentDataPath}/Databases/{databaseName}.db";
            Debug.Log($"Creating {_name} - android");
#endif
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
            Debug.Log($"ConnectAndWrite {Name}");
            using (var connection = new SqliteConnection(Name))
            {
                Debug.Log($"Connection {connection}");
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
