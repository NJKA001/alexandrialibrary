using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class DatabaseBatch<T>
        : IBatch<T>, IDisposable
        where T : Entity
    {
        public DatabaseBatch(ICache<T> cache, IEntityStore<T> database, IDbConnection connection, bool useTransaction)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");
            if (database == null)
                throw new ArgumentNullException("database");
            if (connection == null)
                throw new ArgumentNullException("connection");

            this.cache = cache;
            this.database = database;
            this.connection = connection;
            this.useTransaction = useTransaction;
        }

        private readonly ICache<T> cache;
        private readonly IEntityStore<T> database;
        private readonly IDbConnection connection;
        private readonly bool useTransaction;

        private IDbTransaction transaction;
        
        private bool isStarted;
        private bool isFinished;
        private bool isCancelled;
        private bool isClosed;

        private IDbCommand GetCommand(IStep step)
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = step.Text;

            if (transaction != null)
                command.Transaction = transaction;

            foreach (var item in step.Items)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = item.Key;
                parameter.Value = item.Value;
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private void SynchronizeLinks(T entity, uint id)
        {
            foreach (var link in cache.GetLinksFor(id))
            {
                if (!entity.Links.Contains(link))
                {
                    DeleteLink(entity, link);
                }
            }

            foreach (var link in entity.Links)
            {
                SaveLink(entity, link);
            }
        }

        private void SynchronizeTags(T entity, uint id)
        {
            foreach (var tag in cache.GetTagsFor(id))
            {
                if (!entity.Tags.Contains(tag))
                {
                    DeleteTag(entity, tag);
                }
            }

            foreach (var tag in entity.Tags)
            {
                SaveTag(entity, tag);
            }
        }

        private void DeleteLink(T entity, Link link)
        {
            var id = cache.GetId(link);
            if (id == 0)
                return;

            database.DeleteLink(this, id);
            cache.Remove(entity, link);
        }

        private void DeleteTag(T entity, Tag tag)
        {
            var id = cache.GetId(tag);
            if (id == 0)
                return;

            database.DeleteTag(this, id);
            cache.Remove(entity, tag);
        }

        private void SaveLink(T entity, Link link)
        {
            if (entity == null || link == null)
                return;

            var entityId = cache.GetId(entity);
            if (entityId == 0)
                return;

            var id = cache.GetId(link);

            if (id > 0)
            {
                database.SaveLink(this, id, link, entityId);
            }
            else
            {
                id = database.CreateLink(this, link, entityId);
                cache.Add(id, entity, link);
            }
        }

        private void SaveTag(T entity, Tag tag)
        {
            if (entity == null || tag == null)
                return;

            var entityId = cache.GetId(entity);
            if (entityId == 0)
                return;

            var id = cache.GetId(tag);

            if (id > 0)
            {
                database.SaveTag(this, id, tag, entityId);
            }
            else
            {
                id = database.CreateTag(this, tag, entityId);
                cache.Add(id, entity, tag);
            }
        }


        public void Start()
        {
            if (isStarted)
                throw new InvalidOperationException("batch is already started");

            connection.Open();

            if (useTransaction)
                transaction = connection.BeginTransaction();

            isStarted = true;
        }


        public void Delete(T entity)
        {
            if (!isStarted)
                throw new InvalidOperationException("batch is not started");
            if (isFinished)
                throw new InvalidOperationException("batch is already stopped");
            if (isCancelled)
                throw new InvalidOperationException("batch is cancelled");

            if (entity == null)
                return;

            var id = cache.GetId(entity);
            if (id == 0)
                return;

            database.DeleteEntity(this, id);
            cache.Remove(entity);
        }


        public void Save(T entity)
        {
            if (!isStarted)
                throw new InvalidOperationException("batch is not started");
            if (isFinished)
                throw new InvalidOperationException("batch is finished");
            if (isCancelled)
                throw new InvalidOperationException("batch is cancelled");

            if (entity == null)
                return;

            var id = cache.GetId(entity);

            if (id > 0)
            {
                database.SaveEntity(this, id, entity);
            }
            else
            {
                id = database.CreateEntity(this, entity);
                cache.Add(id, entity);
            }

            SynchronizeLinks(entity, id);
            SynchronizeTags(entity, id);
        }


        public void Finish()
        {
            if (!isStarted)
                throw new InvalidOperationException("batch is not started");
            if (isFinished)
                throw new InvalidOperationException("batch is already finished");
            if (isCancelled)
                throw new InvalidOperationException("batch is already cancelled");

            if (useTransaction && transaction != null)
                transaction.Commit();

            isFinished = true;
        }


        public void Cancel()
        {
            if (!isStarted)
                throw new InvalidOperationException("batch is not started");
            if (isFinished)
                throw new InvalidOperationException("batch is already finished");
            if (isCancelled)
                throw new InvalidOperationException("batch is already cancelled");

            if (useTransaction && transaction != null)
                transaction.Rollback();

            isCancelled = true;
        }

        public void Close()
        {
            if (!isStarted)
                throw new InvalidOperationException("batch is not started");

            if (!isFinished && !isCancelled)
                throw new InvalidOperationException("batch is still running");

            connection.Close();

            isClosed = true;
        }


        public object Execute(IStep step)
        {
            if (!isStarted)
                throw new InvalidOperationException("batch is not started");
            if (isFinished)
                throw new InvalidOperationException("batch is already stopped");
            if (isCancelled)
                throw new InvalidOperationException("batch is cancelled");

            var command = GetCommand(step);

            return command.ExecuteScalar();
        }


        #region IDisposable

        ~DatabaseBatch()
        {
            Dispose(false);
        }

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (connection != null && !isClosed)
                        connection.Close();
                }
            }
            disposed = true;
            
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
