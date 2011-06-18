using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using Gnosis.Core;
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
        protected RepositoryBase(IContext context, ILogger logger, IFactory factory)
            : this(context, logger, factory, null)
        {
        }

        protected RepositoryBase(IContext context, ILogger logger, IFactory factory, IDbConnection defaultConnection)
        {
            this.context = context;
            this.logger = logger;
            this.factory = factory;
            this.defaultConnection = defaultConnection;
            this.baseType = typeof(T);
        }

        private readonly IContext context;
        private readonly ILogger logger;
        private readonly IFactory factory;
        private readonly Type baseType;
        private readonly IList<ILookup> lookups = new List<ILookup>();
        private readonly IList<ISearch> searches = new List<ISearch>();
        private readonly IDbConnection defaultConnection;
        
        protected IContext Context
        {
            get { return context; }
        }

        protected ILogger Logger
        {
            get { return logger; }
        }

        protected void AddLookup(ILookup lookup)
        {
            lookups.Add(lookup);
        }

        protected void AddSearch(ISearch search)
        {
            searches.Add(search);
        }

        protected IDbConnection GetConnection()
        {
            if (defaultConnection != null)
                return defaultConnection;
            else
            {
                var connection = new SQLiteConnection("Data Source=Catalog.db;Version=3;");
                connection.Open();
                return connection;
            }
        }

        protected IEnumerable<T> Select(IFilter filter)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new Query<T>(connection, logger, factory, filter);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        public T Lookup(Guid id)
        {
            var entityInfo = new EntityInfo(baseType);
            var whereClause = string.Format("{0}.{1} = @Id", entityInfo.Name, entityInfo.Identifier.Name);
            var parameters = new Dictionary<string, object> { { "@Id", id.ToString() } };
            return Select(new Filter(whereClause, parameters)).FirstOrDefault();
        }

        public T Lookup(ILookup lookup, IDictionary<string, object> parameters)
        {
            return Select(lookup.GetFilter(parameters)).FirstOrDefault();
        }

        public IEnumerable<T> Search()
        {
            var search = searches.Where(x => x.IsDefault == true).FirstOrDefault();
            if (search == null)
                throw new InvalidOperationException("No default search defined for this repository");

            return Select(search.GetFilter(new Dictionary<string, object>()));
        }

        public IEnumerable<T> Search(ISearch search, IDictionary<string, object> parameters)
        {
            return Select(search.GetFilter(parameters));
        }

        public virtual void Initialize()
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var batch = new InitializeTypeBatch(connection, logger, baseType, lookups, searches);
                batch.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        public void Save(IEnumerable<T> items)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var batch = new SaveEntitiesBatch<T>(connection, logger, items);
                batch.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        public void Delete(IEnumerable<T> items)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var batch = new DeleteEntitiesBatch<T>(connection, logger, items);
                batch.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }
    }
}
