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
        protected RepositoryBase(string database, string table, string orderBy)
        {
            this.database = database;
            this.table = table;
            this.orderBy = orderBy;
            Initialize();
        }

        private string database;
        private string table;
        private string orderBy;

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

        protected virtual IDbCommand GetDeleteCommand(IDbConnection connection, Guid id)
        {
            var command = connection.CreateCommand();
            command.CommandText = string.Format("delete from {0} where Id = @Id;", table);
            AddParameter(command, "@Id", id.ToString());

            return command;
        }

        protected virtual IDbCommand GetSelectCommand(IDbConnection connection)
        {
            return GetSelectCommand(connection, new Dictionary<string, object>());
        }

        protected virtual IDbCommand GetSelectCommand(IDbConnection connection, Guid id)
        {
            return GetSelectCommand(connection, new Dictionary<string, object> { { "Id", id } });
        }

        protected virtual IDbCommand GetSelectCommand(IDbConnection connection, IEnumerable<KeyValuePair<string, object>> criteria)
        {
            var command = connection.CreateCommand();

            var sql = new StringBuilder();
            sql.AppendFormat("select * from {0}", table);

            if (criteria != null && criteria.Count() > 0)
            {
                sql.Append(" where");
                var prefix = string.Empty;
                foreach (var criterium in criteria)
                {
                    if (criterium.Value != null)
                    {
                        var parameterName = string.Format("@{0}", Guid.NewGuid().ToString().Replace("-", string.Empty));
                        AddParameter(command, parameterName, criterium.Value);

                        if (criterium.Value.ToString().Contains('%'))
                            sql.AppendFormat(" {0}{1} like {2}", prefix, criterium.Key, parameterName);
                        else
                            sql.AppendFormat(" {0}{1} = {2}", prefix, criterium.Key, parameterName);
                    }
                    else
                        sql.AppendFormat(" {0}{1} is null", prefix, criterium.Key);

                    prefix = "or ";
                }
            }

            sql.AppendFormat(" order by {0};", orderBy);

            command.CommandText = sql.ToString();
            return command;
        }

        protected abstract string GetInitializeText();
        protected abstract T GetRecord(IDataReader reader);
        protected abstract IDbCommand GetSaveCommand(IDbConnection connection, T record);

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
                        return GetRecord(reader);
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
                using (var command = GetSelectCommand(connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = GetRecord(reader);
                            results.Add(record);
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
                            var record = GetRecord(reader);
                            results.Add(record);
                        }
                    }
                }
            }

            return results;
        }
    }
}
