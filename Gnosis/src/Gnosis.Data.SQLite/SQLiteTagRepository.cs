using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Data.Queries;

namespace Gnosis.Data.SQLite
{
    public class SQLiteTagRepository
    {
        public SQLiteTagRepository(ILogger logger, IDbConnection defaultConnection)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;
            this.defaultConnection = defaultConnection;
            this.connectionFactory = new SQLiteConnectionFactory();
        }

        private readonly ILogger logger;
        private readonly IDbConnection defaultConnection;
        private readonly IConnectionFactory connectionFactory;
        private readonly Func<IDataRecord, ITag> readTag = (record) =>
            {
                var name = record.GetString("Name");
                var nameAmericanized = record.GetString("NameAmericanized");
                var nameSoundsLike = record.GetString("NameSoundsLike");
                var type = TagType.Parse(record.GetInt64("Type"));
                var target = record.GetUri("Target");
                return new Tag(name, nameSoundsLike, nameAmericanized, type, target);
            };


        private const string connectionString = "Data Source=Tag.db;Version=3;";

        #region Private Methods

        private IDbConnection GetConnection()
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

        private void AddParameter(IDbCommand command, int count, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = string.Format("@{0}{1}", name, count);
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        private void BuildDeleteCommand(IDbCommand command, StringBuilder sql, ITag tag, int count)
        {
            sql.AppendFormat("delete from Tag where Id = @Id{0}", count);
            AddParameter(command, count, "Id", tag.Id);
        }

        private void BuildSaveCommand(IDbCommand command, StringBuilder sql, ITag tag, int count)
        {
            sql.AppendFormat("insert into Tag (Name, NameAmericanized, NameSoundsLike, Type, Target) values (@Name{0}, @NameAmericanized{0}, @NameSoundsLike{0}, @Type{0}, @Target{0})", count);
            AddParameter(command, count, "Name", tag.Name);
            AddParameter(command, count, "NameAmericanized", tag.NameAmericanized);
            AddParameter(command, count, "NameSoundsLike", tag.NameSoundsLike);
            AddParameter(command, count, "Type", tag.Type.Id);
            AddParameter(command, count, "Target", tag.Target.ToString());
        }

        private void Execute(Action<IDbConnection> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            IDbConnection connection = null;

            try
            {
                logger.Info("SQLiteTagRepository.Execute");
            }
            catch (Exception ex)
            {
                logger.Error("  Execute", ex);
                throw;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private void ExecuteReader(Func<IDbConnection, IDataReader> getReader, Action<IDataRecord> recordAction)
        {
            if (getReader == null)
                throw new ArgumentNullException("getReader");
            if (recordAction == null)
                throw new ArgumentNullException("recordAction");

            IDbConnection connection = null;

            try
            {
                logger.Info("SQLiteTagRepository.ExecuteReader");

                connection = GetConnection();

                using (var reader = getReader(connection))
                {
                    while (reader.Read())
                    {
                        recordAction(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  ExecuteReader", ex);
                throw;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        private void ExecuteTransaction(Action<IDbConnection> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            IDbConnection connection = null;
            IDbTransaction transaction = null;

            try
            {
                logger.Info("SQLiteTagRepository.ExecuteTransaction");
                
                connection = GetConnection();
                transaction = connection.BeginTransaction();

                action(connection);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                logger.Error("  ExecuteTransaction", ex);
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        logger.Error("  Could not rollback transaction", rollbackEx);
                    }
                }
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        #endregion

        public ITag Lookup(long id)
        {
            try
            {
                logger.Info("SQLiteTagRepository.Lookup");

                ITag tag = null;

                ExecuteReader(connection =>
                    {
                        var command = connection.CreateCommand();
                        command.CommandType = CommandType.Text;
                        command.CommandText = "select * from Tag where Id = @Id";
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@Id";
                        parameter.Value = id;
                        command.Parameters.Add(parameter);
                        return command.ExecuteReader();
                    },
                    record =>
                    {
                        tag = readTag(record);
                    }
                );

                return tag;
            }
            catch (Exception ex)
            {
                logger.Error("  Lookup", ex);
                throw;
            }
        }

        public IEnumerable<ITag> All()
        {
            try
            {
                logger.Info("SQLiteTagRepository.All()");

                var tags = new List<ITag>();
               
                ExecuteReader(connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from Tag";
                    return command.ExecuteReader();
                },
                    record =>
                    {
                        tags.Add(readTag(record));
                    }
                );

                return tags;
            }
            catch (Exception ex)
            {
                logger.Error("  All", ex);
                throw;
            }
        }

        public void Initialize()
        {
            try
            {
                logger.Info("SQLiteTagRepository.Initialize");

                Execute(
                connection =>
                    {
                        var command = connection.CreateCommand();
                        command.CommandType = CommandType.Text;
                        var sql = new StringBuilder();
                        sql.Append("create table if not exists Tag (Id integer primary key not null, Name text not null, NameAmericanized text not null, NameSoundsLike text not null, Type integer not null, Target text not null);");
                        sql.Append("create index if not exists Tag_Name on Tag (Name asc);");
                        sql.Append("create index if not exists Tag_NameAmericanized on Tag (NameAmericanized asc);");
                        sql.Append("create index if not exists Tag_NameSoundsLike on Tag (NameSoundsLike asc);");
                        sql.Append("create index if not exists Tag_Type on Tag (Type asc);");
                        sql.Append("create index if not exists Tag_Target on Tag (Target asc);");

                        command.CommandText = sql.ToString();
                        command.ExecuteNonQuery();
                    }
                );
            }
            catch (Exception ex)
            {
                logger.Error("  Initialize", ex);
                throw;
            }
        }

        public void Save(ITag item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            try
            {
                logger.Info("SQLiteTagRepository.Save(ITag)");

                Execute(connection =>
                    {
                        var command = connection.CreateCommand();
                        command.CommandType = CommandType.Text;
                        var sql = new StringBuilder();
                        BuildSaveCommand(command, sql, item, 1);
                        command.ExecuteNonQuery();
                    });
            }
            catch (Exception ex)
            {
                logger.Error("  Save(ITag)", ex);
                throw;
            }
        }

        public void Save(IEnumerable<ITag> items)
        {
            try
            {
                logger.Info("SQLiteTagRepository.Save(IEnumerable<ITag>)");

                ExecuteTransaction(connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    var sql = new StringBuilder();

                    var count = 0;
                    foreach (var item in items)
                    {
                        count++;
                        BuildSaveCommand(command, sql, item, count);
                    }

                    command.ExecuteNonQuery();
                });
            }
            catch (Exception ex)
            {
                logger.Error("  Save(IEnumerable<ITag>)", ex);
                throw;
            }
        }

        public void Delete(ITag item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            try
            {
                logger.Info("SQLiteTagRepository.Delete(ITag)");

                Execute(connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    var sql = new StringBuilder();
                    BuildDeleteCommand(command, sql, item, 1);
                    command.ExecuteNonQuery();
                });
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(ITag)", ex);
                throw;
            }
        }

        public void Delete(IEnumerable<ITag> items)
        {
            try
            {
                logger.Info("SQLiteTagRepository.Delete(IEnumerable<ITag>)");

                ExecuteTransaction(connection =>
                {
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    var sql = new StringBuilder();

                    var count = 0;
                    foreach (var item in items)
                    {
                        count++;
                        BuildDeleteCommand(command, sql, item, count);
                    }

                    command.ExecuteNonQuery();
                });
            }
            catch (Exception ex)
            {
                logger.Error("  Delete(IEnumerable<ITag>)", ex);
                throw;
            }
        }
    }
}
