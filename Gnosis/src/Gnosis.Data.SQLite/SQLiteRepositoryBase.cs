using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Data.SQLite
{
    public abstract class SQLiteRepositoryBase
    {
        protected SQLiteRepositoryBase(ILogger logger, string connectionString)
            : this(logger, connectionString, null)
        {
        }

        protected SQLiteRepositoryBase(ILogger logger, string connectionString, IDbConnection defaultConnection)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            this.logger = logger;
            this.connectionString = connectionString;
            this.defaultConnection = defaultConnection;
        }

        protected readonly ILogger logger;
        private readonly IDbConnection defaultConnection;
        private readonly string connectionString;
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected IDbConnection GetConnection()
        {
            if (defaultConnection != null)
                return defaultConnection;
            else
            {
                var connection = connectionFactory.Create(connectionString);
                connection.Open();
                return connection;
            }
        }

        protected T GetRecord<T>(ISimpleCommandBuilder builder, Func<IDataRecord, T> readRecord)
        {
            return GetRecords<T>(builder, readRecord).FirstOrDefault();
        }

        protected IEnumerable<T> GetRecords<T>(ISimpleCommandBuilder builder, Func<IDataRecord, T> readRecord)
        {
            IDbConnection connection = null;
            var records = new List<T>();

            try
            {
                connection = GetConnection();
                
                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = readRecord(reader);
                        records.Add(record);
                    }
                }
                
                return records;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected void ExecuteNonQuery(ISimpleCommandBuilder builder)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var command = builder.ToCommand(connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected object ExecuteScalar(ISimpleCommandBuilder builder)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var command = builder.ToCommand(connection);
                return command.ExecuteScalar();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected void ExecuteTransaction(ISimpleCommandBuilder builder)
        {

            IDbConnection connection = null;
            IDbTransaction transaction = null;

            try
            {
                connection = GetConnection();
                transaction = connection.BeginTransaction();
                var command = builder.ToCommand(connection);
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                logger.Error("  ExecuteTransaction", ex);

                throw;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }
    }
}
