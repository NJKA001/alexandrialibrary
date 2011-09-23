using System;
using System.Collections.Generic;
using System.IO;
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
            var file = new FileInfo(@".\ALEXANDRIA.FDB");
            if (!file.Exists)
            {
                connectionFactory.CreateDatabase(connectionString);
            }
            else
            {
                logger.Debug("CreateDatabase: file already exists, skipping CreateDatabase call");
            }
        }

        protected IDbConnection GetConnection()
        {
            return connectionFactory.Create(connectionString);
        }

        private IDbCommand GetCommand(IDbConnection connection, string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;

            if (parameters == null)
                return command;

            foreach (var pair in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = pair.Key;
                parameter.Value = pair.Value;
                command.Parameters.Add(parameter);
            }
            return command;
        }

        protected void Execute(IEnumerable<Tuple<string, IEnumerable<KeyValuePair<string, object>>>> commandInfo)
        {
            IDbTransaction transaction = null;
            var commited = false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    transaction = connection.BeginTransaction();

                    foreach (var info in commandInfo)
                    {
                        var command = GetCommand(connection, info.Item1, info.Item2);
                        command.Transaction = transaction;
                        logger.Debug("Execute: " + command.CommandText);
                        command.ExecuteNonQuery();
                    }

                    commited = true;
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null && !commited)
                    transaction.Rollback();

                logger.Error("  Execute(commandInfo)", ex);
                throw;
            }
        }

        protected void Execute(string commandText)
        {
            Execute(commandText, null);
        }

        protected void Execute(string commandText, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = GetCommand(connection, commandText, parameters);
                command.ExecuteNonQuery();
            }
        }
    }
}
