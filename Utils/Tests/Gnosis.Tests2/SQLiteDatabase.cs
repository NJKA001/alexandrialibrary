using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public abstract class SQLiteDatabase<T>
        : IEntityStore<T>
        where T : Entity
    {
        protected SQLiteDatabase(string connectionString, string tableName, string initEntityCommandText)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");
            if (tableName == null)
                throw new ArgumentNullException("tableName");
            if (initEntityCommandText == null)
                throw new ArgumentNullException("initEntityCommandText");
            
            this.connectionString = connectionString;
            this.tableName = tableName;
            this.initEntityCommandText = initEntityCommandText;

            initLinkCommandText = string.Format("create table {0}Link if not exists (Id integer primary key, {0} integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null); create unique index if not exists {0}Link_unique on {0}Link ({0}, Name, Relationship, Target);", tableName);
            initTagCommandText = string.Format("create table {0}Tag if not exists (Id integer primary key, {0} integer not null, Name text not null, Category integer not null, Source integer not null); create unique index if not exists {0}Tag_unique on {0}Tag ({0}, Name, Category, Source);", tableName);
            selectAllEntityCommandText = string.Format("select * from {0};", tableName);
            selectAllLinkCommandText = string.Format("select * from {0}Link;", tableName);
            selectAllTagCommandText = string.Format("select * from {0}Tag;", tableName);
            deleteAllCommandText = string.Format("delete from {0} where Id = @Id; delete from {0}Link where {0} = @Id; delete from {0}Tag where {0} = @Id;", tableName);
            deleteLinkCommandText = string.Format("delete from {0}Link where Id = @Id;", tableName);
            deleteTagCommandText = string.Format("delete from {0}Tag where Id = @Id;", tableName);
        }

        private readonly string connectionString;
        private readonly string tableName;
        private readonly string initEntityCommandText;

        private readonly string initLinkCommandText;
        private readonly string initTagCommandText;
        private readonly string selectAllEntityCommandText;
        private readonly string selectAllLinkCommandText;
        private readonly string selectAllTagCommandText;
        private readonly string deleteAllCommandText;
        private readonly string deleteEntityComamndText;
        private readonly string deleteLinkCommandText;
        private readonly string deleteTagCommandText;

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        protected IDbCommand GetCommand(IDbConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            return command;
        }

        //protected void AddParameter(IDbCommand command, string name, object value)
        //{
        //    command.Parameters.Add(new SQLiteParameter(name, value));
        //}

        protected virtual void InitializeEntities(IDbConnection connection)
        {
            var command = GetCommand(connection, initEntityCommandText);
            command.ExecuteNonQuery();
        }

        protected virtual void InitializeLinks(IDbConnection connection)
        {
            var command = GetCommand(connection, initLinkCommandText);
            command.ExecuteNonQuery();
        }

        protected virtual void InitializeTags(IDbConnection connection)
        {
            var command = GetCommand(connection, initTagCommandText);
            command.ExecuteNonQuery();
        }

        protected virtual void LoadEntities(IDbConnection connection, Action<T> entityLoaded)
        {
            if (entityLoaded == null)
                return;

            var command = GetCommand(connection, selectAllEntityCommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entity = ReadEntity(reader);
                    entityLoaded(entity);
                }
            }
        }

        protected virtual void LoadLinks(IDbConnection connection, Action<Link> linkLoaded)
        {
            if (linkLoaded == null)
                return;

            var command = GetCommand(connection, selectAllLinkCommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var link = ReadLink(reader);
                    linkLoaded(link);
                }
            }
        }

        protected virtual void LoadTags(IDbConnection connection, Action<Tag> tagLoaded)
        {
            if (tagLoaded == null)
                return;

            var command = GetCommand(connection, selectAllTagCommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tag = ReadTag(reader);
                    tagLoaded(tag);
                }
            }
        }

        protected abstract T ReadEntity(IDataRecord record);

        protected abstract Link ReadLink(IDataRecord record);

        protected abstract Tag ReadTag(IDataRecord record);

        protected abstract IStep GetInsertEntityStep(T entity);

        protected abstract IStep GetUpdateEntityStep(T entity, uint id);

        protected virtual IStep GetDeleteEntityStep(uint id)
        {
            return new Step(deleteAllCommandText, "@Id", id);
        }

        protected virtual IStep GetDeleteLinkStep(uint id)
        {
            return new Step(deleteLinkCommandText, "@Id", id);
        }

        protected virtual IStep GetDeleteTagStep(uint id)
        {
            return new Step(deleteTagCommandText, "@Id", id);
        }

        public void Initialize(Action<T> entityLoaded, Action<Link> linkLoaded, Action<Tag> tagLoaded)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                InitializeEntities(connection);
                InitializeLinks(connection);
                InitializeTags(connection);

                LoadEntities(connection, entityLoaded);
                LoadLinks(connection, linkLoaded);
                LoadTags(connection, tagLoaded);
            }
        }

        public void DeleteEntity(IBatch<T> batch, uint id)
        {
            batch.Execute(GetDeleteEntityStep(id));
        }

        public void DeleteLink(IBatch<T> batch, uint id)
        {
            batch.Execute(GetDeleteLinkStep(id));
        }

        public void DeleteTag(IBatch<T> batch, uint id)
        {
            batch.Execute(GetDeleteTagStep(id));
        }

        public void SaveEntity(IBatch<T> batch, uint id, T entity, Action<uint> entityCreated)
        {
            if (id > 0)
            {
                batch.Execute(GetUpdateEntityStep(entity, id));
            }
            else
            {
                var result = batch.Execute(GetInsertEntityStep(entity));
                
                uint newId = 0;
                if (entityCreated != null && result != null && uint.TryParse(result.ToString(), out newId))
                {
                    entityCreated(newId);
                }
            }
        }

        public void SaveLink(IBatch<T> batch, uint id, Link link, uint entityId, Action<uint> linkCreated)
        {
            if (id > 0)
            {
                //batch.Execute(GetUpdateLinkStep(link, id);
            }
            else
            {
            }
        }

        public void SaveTag(IBatch<T> batch, uint id, Tag tag, uint entityId, Action<uint> tagCreated)
        {
            if (id > 0)
            {
            }
            else
            {
            }
        }

        public IBatch<T> CreateBatch(ICache<T> cache)
        {
            return new DatabaseBatch<T>(cache, this, GetConnection(), false);
        }
    }
}
