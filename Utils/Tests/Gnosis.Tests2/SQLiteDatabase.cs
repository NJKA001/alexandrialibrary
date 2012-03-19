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
        protected SQLiteDatabase(string databaseName, string tableName)
        {
            if (databaseName == null)
                throw new ArgumentNullException("databaseName");
            if (tableName == null)
                throw new ArgumentNullException("tableName");

            this.databaseName = databaseName;
            this.tableName = tableName;

            connectionString = string.Format("Data Source={0}.db;", databaseName);
            initLinkCommandText = string.Format("create table if not exists {0}Link (Id integer primary key, {0} integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null); create unique index if not exists {0}Link_unique on {0}Link ({0}, Name, Relationship, Target);", tableName);
            initTagCommandText = string.Format("create table if not exists {0}Tag (Id integer primary key, {0} integer not null, Name text not null, Category integer not null, Source integer not null); create unique index if not exists {0}Tag_unique on {0}Tag ({0}, Name, Category, Source);", tableName);
            selectAllEntityCommandText = string.Format("select * from {0};", tableName);
            selectAllLinkCommandText = string.Format("select * from {0}Link;", tableName);
            selectAllTagCommandText = string.Format("select * from {0}Tag;", tableName);
            deleteAllCommandText = string.Format("delete from {0} where Id = @Id; delete from {0}Link where {0} = @Id; delete from {0}Tag where {0} = @Id;", tableName);
            deleteLinkCommandText = string.Format("delete from {0}Link where Id = @Id;", tableName);
            deleteTagCommandText = string.Format("delete from {0}Tag where Id = @Id;", tableName);
            insertLinkCommandText = string.Format("insert into {0}Link ({0}, Name, Relationship, Source, Target) values (@{0}, @Name, @Relationship, @Source, @Target); select last_insert_rowid();", tableName);
            insertTagCommandText = string.Format("insert into {0}Tag ({0}, Name, Category, Source) values (@{0}, @Name, @Category, @Source); select last_insert_rowid();", tableName);
            updateLinkCommandText = string.Format("update {0}Link set {0} = @{0}, Name = @Name, Relationship = @Relationship, Source = @Source, Target = @Target;", tableName);
            updateTagCommandText = string.Format("update {0}Tag set {0} = @{0}, Name = @Name, Category = @Category, Source = @Source;", tableName);
            parentParameterName = string.Format("@{0}", tableName);
        }

        private readonly string databaseName;
        private readonly string tableName;

        private readonly string connectionString;
        private readonly string initLinkCommandText;
        private readonly string initTagCommandText;
        private readonly string selectAllEntityCommandText;
        private readonly string selectAllLinkCommandText;
        private readonly string selectAllTagCommandText;
        private readonly string deleteAllCommandText;
        private readonly string deleteLinkCommandText;
        private readonly string deleteTagCommandText;
        private readonly string insertLinkCommandText;
        private readonly string insertTagCommandText;
        private readonly string updateLinkCommandText;
        private readonly string updateTagCommandText;
        private readonly string parentParameterName;

        private readonly IList<Action> postLoadActions = new List<Action>();

        protected abstract void LoadEntity(IDataRecord record, Action<uint, T> entityLoaded);
        protected abstract string GetInitEntityCommandText();
        protected abstract IStep GetInsertEntityStep(T entity);
        protected abstract IStep GetUpdateEntityStep(uint id, T entity);

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

        protected void AddPostLoadAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            postLoadActions.Add(action);
        }

        protected void ExecutePostLoadActions()
        {
            foreach (var action in postLoadActions)
                action();
        }

        protected virtual void InitializeEntities(IDbConnection connection)
        {
            var command = GetCommand(connection, GetInitEntityCommandText());
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

        protected virtual void LoadLink(IDataRecord record, Action<uint, Link, uint> linkLoaded)
        {
            var id = (uint)record.GetInt64(0);
            var parent = (uint)record.GetInt64(1);
            var name = record.GetString(2);
            var relationship = (Relationship)record.GetInt32(3);
            var source = (Source)record.GetInt32(4);
            var target = record.GetString(5);

            linkLoaded(id, new Link(name, relationship, source, target), parent);
        }

        protected virtual void LoadTag(IDataRecord record, Action<uint, Tag, uint> tagLoaded)
        {
            var id = (uint)record.GetInt64(0);
            var parent = (uint)record.GetInt64(1);
            var name = record.GetString(2);
            var category = (Category)record.GetInt32(3);
            var source = (Source)record.GetInt32(4);

            tagLoaded(id, new Tag(name, category, source), parent);
        }

        protected virtual void LoadEntities(IDbConnection connection, Action<uint, T> entityLoaded)
        {
            if (entityLoaded == null)
                return;

            var command = GetCommand(connection, selectAllEntityCommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    LoadEntity(reader, entityLoaded);
                }
            }
        }

        protected virtual void LoadLinks(IDbConnection connection, Action<uint, Link, uint> linkLoaded)
        {
            if (linkLoaded == null)
                return;

            var command = GetCommand(connection, selectAllLinkCommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    LoadLink(reader, linkLoaded);
                }
            }
        }

        protected virtual void LoadTags(IDbConnection connection, Action<uint, Tag, uint> tagLoaded)
        {
            if (tagLoaded == null)
                return;

            var command = GetCommand(connection, selectAllTagCommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    LoadTag(reader, tagLoaded);
                }
            }
        }

        protected virtual IStep GetInsertLinkStep(Link link, uint entityId)
        {
            var step = new Step(insertLinkCommandText);
            step.AddItem(parentParameterName, entityId);
            step.AddItem("@Name", link.Name);
            step.AddItem("@Relationship", (ushort)link.Relationship);
            step.AddItem("@Source", (ushort)link.Source);
            step.AddItem("@Target", link.Target);

            return step;
        }

        protected virtual IStep GetInsertTagStep(Tag tag, uint entityId)
        {
            var step = new Step(insertTagCommandText);
            step.AddItem(parentParameterName, entityId);
            step.AddItem("@Name", tag.Name);
            step.AddItem("@Category", (ushort)tag.Category);
            step.AddItem("@Source", (ushort)tag.Source);

            return step;
        }

        protected virtual IStep GetUpdateLinkStep(uint id, Link link, uint entityId)
        {
            var step = new Step(updateLinkCommandText);
            step.AddItem(parentParameterName, entityId);
            step.AddItem("@Name", link.Name);
            step.AddItem("@Relationship", (ushort)link.Relationship);
            step.AddItem("@Source", (ushort)link.Source);
            step.AddItem("@Target", link.Target);
            step.AddItem("@Id", id);

            return step;
        }

        protected virtual IStep GetUpdateTagStep(uint id, Tag tag, uint entityId)
        {
            var step = new Step(insertTagCommandText);
            step.AddItem(parentParameterName, entityId);
            step.AddItem("@Name", tag.Name);
            step.AddItem("@Category", (ushort)tag.Category);
            step.AddItem("@Source", (ushort)tag.Source);
            step.AddItem("@Id", id);

            return step;
        }

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


        public void Initialize(Action<uint, T> entityLoaded, Action<uint, Link, uint> linkLoaded, Action<uint, Tag, uint> tagLoaded)
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

            ExecutePostLoadActions();
        }

        public uint CreateEntity(IBatch<T> batch, T entity)
        {
            var result = batch.Execute(GetInsertEntityStep(entity));

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint CreateLink(IBatch<T> batch, Link link, uint entityId)
        {
            var result = batch.Execute(GetInsertLinkStep(link, entityId));

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint CreateTag(IBatch<T> batch, Tag tag, uint entityId)
        {
            var result = batch.Execute(GetInsertTagStep(tag, entityId));

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
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

        public void SaveEntity(IBatch<T> batch, uint id, T entity)
        {
            batch.Execute(GetUpdateEntityStep(id, entity));
        }

        public void SaveLink(IBatch<T> batch, uint id, Link link, uint entityId)
        {
            batch.Execute(GetUpdateLinkStep(id, link, entityId));
        }

        public void SaveTag(IBatch<T> batch, uint id, Tag tag, uint entityId)
        {
            batch.Execute(GetUpdateTagStep(id, tag, entityId));
        }

        public IBatch<T> CreateBatch(ICache<T> cache)
        {
            return new DatabaseBatch<T>(cache, this, GetConnection(), false);
        }
    }
}
