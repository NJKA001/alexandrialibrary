using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Archon.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected RepositoryBase(string database, string recordName)
        {
            this.database = database;
            this.recordName = recordName;
            Initialize();
        }

        private string database;
        private string recordName;

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

        protected virtual IDbCommand GetSelectCommand(IDbConnection connection, Guid id)
        {
            return GetSelectCommand(connection, new Dictionary<string, object> { { "Id", id } });
        }

        protected abstract string GetInitializeText();
        protected abstract T Get(IDataReader reader);
        protected abstract IDbCommand GetSaveCommand(IDbConnection connection, T record);
        protected abstract IDbCommand GetDeleteCommand(IDbConnection connection, Guid id);
        protected abstract IDbCommand GetSelectCommand(IDbConnection connection, IEnumerable<KeyValuePair<string, object>> criteria);

        public T Get(Guid id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = GetSelectCommand(connection, id))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        return Get(reader);
                    }
                }
            }
        }

        public void Save(T record)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = GetSaveCommand(connection, record))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = GetDeleteCommand(connection, id))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<T> All()
        {
            var results = new List<T>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = GetSelectCommand(connection, null))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var result = Get(reader);
                            results.Add(result);
                        }
                    }
                }
            }

            return results;
        }

        public IEnumerable<T> Search(IEnumerable<KeyValuePair<string, object>> criteria)
        {
            var results = new List<T>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = GetSelectCommand(connection, criteria))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var result = Get(reader);
                            results.Add(result);
                        }
                    }
                }
            }

            return results;
        }
    }
}
