using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase
    {
        protected RepositoryBase(string database, string rootTable)
        {
            this.database = database;
            this.rootTable = rootTable;
        }

        private string database;
        private string rootTable;

        private void Initialize()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    //command.CommandText = GetInitializeText();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }
    }
}
