using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
    {
        protected RepositoryBase(IContext context)
        {
            this.context = context;
            //this.database = "Catalog.db";
            //this.rootTable = rootTable;

            Initialize();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(RepositoryBase<T>));
        private readonly IContext context;
        private string database = "Catalog.db";

        private void Initialize()
        {
            try
            {
                var instance = Create();
                var commandBuilder = new CreateCommandBuilder(typeof(T), instance);
                var commandText = commandBuilder.ToString();

                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("RepositoryBase.Initialize", ex);
            }
        }

        private static void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        protected abstract T Create();
        protected abstract T Create(IDataReader reader);

        protected IContext Context
        {
            get { return context; }
        }

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }

        protected static IDbCommand GetCommand(IDbConnection connection, string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;

            foreach (var parameter in parameters)
                AddParameter(command, parameter.Key, parameter.Value);

            return command;
        }

        protected void ExecuteNonQuery(string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        {
        }

        protected IEnumerable<T> ExecuteReader(string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var items = new List<T>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = GetCommand(connection, commandText, parameters))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var item = Create(reader);
                        items.Add(item);
                    }
                }
            }

            return items;
        }
    }
}
