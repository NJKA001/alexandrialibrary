using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Data
{
    public abstract class SQLiteDatabase
        : IDatabase
    {
        public SQLiteDatabase(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            connectionString = string.Format("Data Source={0}.db;", name);
        }

        private readonly string name;
        private readonly string connectionString;

        protected abstract string GetInitializeCommandText();

        public string Name
        {
            get { return name; }
        }

        public virtual void Initialize()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = GetInitializeCommandText();

                command.ExecuteNonQuery();
            }
        }

        public IDbConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
