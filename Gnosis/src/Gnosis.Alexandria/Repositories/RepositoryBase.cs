using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Batches;
using Gnosis.Core.Commands;
using Gnosis.Core.Queries;

//using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IEntity
    {
        protected RepositoryBase(IContext context)
        {
            this.context = context;

            Initialize();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(RepositoryBase<T>));
        private readonly IContext context;
        private string database = "Catalog.db";

        private void Initialize()
        {
            try
            {
                var batch = new InitializeTypeBatch(() => GetConnection(), typeof(T));
                batch.Execute();
            }
            catch (Exception ex)
            {
                log.Error("RepositoryBase.Initialize", ex);
            }
        }

        protected abstract T CreateDefault();
        protected abstract IEnumerable<T> Read(IDataReader reader);
        
        protected IContext Context
        {
            get { return context; }
        }

        protected IDbConnection GetConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }

        //protected ITimeStamp GetTimeStamp(IDataReader reader)
        //{
        //    var createdBy = new Uri(reader["TimeStamp_CreatedBy"].ToString());
        //    var createdDate = DateTime.Parse(reader["TimeStamp_CreatedDate"].ToString());
        //    var lastAccessedBy = new Uri(reader["TimeStamp_LastAccessedBy"].ToString());
        //    var lastAccessedDate = DateTime.Parse(reader["TimeStamp_LastAccessedDate"].ToString());
        //    var lastModifiedBy = new Uri(reader["TimeStamp_LastModifiedBy"].ToString());
        //    var lastModifiedDate = DateTime.Parse(reader["TimeStamp_LastModifiedDate"].ToString());

        //    return new TimeStamp(createdBy, createdDate, lastAccessedBy, lastAccessedDate, lastModifiedBy, lastModifiedDate);
        //}

        //protected int Execute(CommandBuilder commandBuilder)
        //{
        //    using (var connection = GetConnection())
        //    {
        //        connection.Open();
        //        using (var command = commandBuilder.GetCommand(connection))
        //        {
        //            return command.ExecuteNonQuery();
        //        }
        //    }
        //}

        protected IEnumerable<T> Select()
        {
            return Select(string.Empty, string.Empty, null);
        }

        protected IEnumerable<T> Select(string whereClause)
        {
            return Select(whereClause, string.Empty, null);
        }

        protected IEnumerable<T> Select(ISearch search)
        {
            return Select(search.WhereClause, search.OrderByClause, search.Parameters);
        }

        protected IEnumerable<T> Select(string whereClause, string orderByClause, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            var query = new Query<T>(() => GetConnection(), whereClause, orderByClause, parameters);
            return query.Execute();
        }

        public T Lookup(Guid id)
        {
            var whereClause = string.Format("{0}.Id = @Id", typeof(T).GetTableInfo().Name);
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            return Select(whereClause, string.Empty, parameters).FirstOrDefault();
        }

        public T Lookup(ILookup<T> lookup)
        {
            return Select(lookup.WhereClause, string.Empty, lookup.Parameters).FirstOrDefault();
        }

        public IEnumerable<T> Search()
        {
            return Select();
        }

        public IEnumerable<T> Search(ISearch<T> search)
        {
            return Select(search);
        }

        public void Save(IEnumerable<T> items)
        {
            var batch = new SaveEntitiesBatch<T>(() => GetConnection(), items);
            batch.Execute();
        }

        public void Delete(IEnumerable<T> items)
        {
            var batch = new DeleteEntitiesBatch<T>(() => GetConnection(), items);
            batch.Execute();
        }
    }
}
