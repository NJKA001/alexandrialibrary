using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Gnosis.Babel.SQLite
{
    public abstract class SQLiteStore : IStore
    {
        protected SQLiteStore(string name)
        {
            _name = name;
        }

        private readonly string _name;

        private string GetConnectionString()
        {
            return string.Format("Data Source={0}.db;Version=3;", _name);
        }

        private IDbConnection GetDbConnection()
        {
            return GetDbConnection(GetConnectionString());
        }

        private IDbConnection GetDbConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        private static IDbCommand CreateDbCommand(ICommand command, IDbConnection connection, IDbTransaction transaction = null)
        {
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = command.ToString();

            if (transaction != null)
                dbCommand.Transaction = transaction;

            foreach (var parameter in command.Parameters)
            {
                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = parameter.Name;
                dbParameter.Value = parameter.GetValue();
                dbCommand.Parameters.Add(dbParameter);
            }

            return dbCommand;
        }

        private static IDataReader GetDataReader(ICommand command, IDbConnection connection)
        {
            var dbCommand = CreateDbCommand(command, connection);
            return dbCommand.ExecuteReader();
        }

        private static object GetResult(ICommand command, IDbConnection connection, IDbTransaction transaction)
        {
            var dbCommand = CreateDbCommand(command, connection, transaction);
            return dbCommand.ExecuteScalar();
        }

        public string Name
        {
            get { return _name; }
        }

        public void Execute(IBatch batch)
        {
            Execute(new List<IBatch> { batch });
        }

        public void Execute(IEnumerable<IBatch> batches)
        {
            if (batches == null)
                throw new ArgumentNullException("batches");

            IDbTransaction transaction = null;

            using (var connection = GetDbConnection())
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (var batch in batches)
                    {
                        foreach (var command in batch.Commands)
                        {
                            var result = GetResult(command, connection, transaction);
                            batch.InvokeCallback(command.Id, result);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction != null)
                        transaction.Rollback();

                    throw;
                }
            }
        }

        public ICollection<T> Execute<T>(IQuery<T> query)
        {
            return Execute<T>(new List<IQuery<T>> { query });
        }

        public ICollection<T> Execute<T>(IEnumerable<IQuery<T>> queries)
        {
            var results = new List<T>();

            using (var connection = GetDbConnection())
            {
                connection.Open();

                foreach (IQuery<T> query in queries)
                {
                    foreach (var command in query.Commands)
                    {
                        using (var reader = GetDataReader(command, connection))
                        {
                            while (reader.Read())
                            {
                                var model = default(T);

                                if (query.Cache != null && !query.Cache.IsEmpty)
                                {
                                    model = query.Cache.GetOne(reader["Id"]);
                                }

                                if (model == null && query.ModelMapper != null)
                                    model = query.ModelMapper.GetModel(reader);

                                if (model != null)
                                    results.Add(model);
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}
