using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.Firebird
{
    public abstract class FirebirdRepositoryBase
    {
        protected FirebirdRepositoryBase(ILogger logger)
        {
            this.logger = logger;
        }

        private const string connectionString = @"Database=.\ALEXANDRIA.FDB;ServerType=1;User='';Password='';Charset=UTF8";

        private readonly FirebirdConnectionFactory connectionFactory = new FirebirdConnectionFactory();
        protected readonly ILogger logger;

        protected void CreateDatabase()
        {
            connectionFactory.CreateDatabase(connectionString);
        }

        protected IDbConnection GetConnection()
        {
            return connectionFactory.Create(connectionString);
        }

        protected void Execute(string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = commandText;
                foreach (var pair in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = pair.Key;
                    parameter.Value = pair.Value;
                    command.Parameters.Add(parameter);
                }
            }
        }
    }
}
