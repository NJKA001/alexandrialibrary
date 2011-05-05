using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase
    {
        protected RepositoryBase(IContext context, string database, string rootTable)
        {
            this.context = context;
            this.database = database;
            this.rootTable = rootTable;

            Initialize();
        }

        private readonly IContext context;
        private string database;
        private string rootTable;

        private void Initialize()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = GetInitializeText();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected abstract string GetInitializeText();

        protected IContext Context
        {
            get { return context; }
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
