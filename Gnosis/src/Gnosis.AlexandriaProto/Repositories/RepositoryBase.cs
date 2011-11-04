using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Data.Batches;
using Gnosis.Data.Commands;
using Gnosis.Data.Queries;
using Gnosis.Data.SQLite;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IEntity
    {
        protected RepositoryBase(IFactory factory)
            : this(factory, null)
        {
        }

        protected RepositoryBase(IFactory factory, IDbConnection defaultConnection)
        {
            this.factory = factory;
            this.defaultConnection = defaultConnection;
            this.baseType = typeof(T);
            this.connectionFactory = new SQLiteConnectionFactory();
        }

        private readonly IFactory factory;
        private readonly Type baseType;
        private readonly IList<ILookup> lookups = new List<ILookup>();
        private readonly IList<ISearch> searches = new List<ISearch>();
        private readonly IDbConnection defaultConnection;
        private readonly IConnectionFactory connectionFactory;

        protected IFactory Factory
        {
            get { return factory; }
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
                var connection = connectionFactory.Create("Data Source=Catalog.db;Version=3;");
                    //new SQLiteConnection("Data Source=Catalog.db;Version=3;");
                connection.Open();
                return connection;
            }
        }

        protected IEnumerable<T> SelectEntities(IFilter filter)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new Query<T>(factory, filter);
                return query.Execute(connection);
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<TEntity> SelectEntities<TEntity>(IQuery<TEntity> query)
            where TEntity : IEntity
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                return query.Execute(connection);
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<TOutline> SelectOutlines<TEntity, TOutline>(IOutlineQuery<TEntity, TOutline> query)
            where TEntity : IEntity
            where TOutline : IOutline<TEntity>
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                return query.Execute(connection);
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<TValue> SelectValues<TValue>(IValueQuery<TValue> query)
            where TValue : IValue
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                return query.Execute(connection);
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        /*
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

        protected IEnumerable<TOutline> SelectOutline<TOutline>(IFilter filter)
            where TOutline : IOutline<T>, new()
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new OutlineQuery<T, TOutline>(connection, logger, factory, filter);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<TChild> SelectChild<TChild>(IFilter filter)
            where TChild : IChild
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new Query<TChild>(connection, logger, factory, filter);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<TValue> Select<TValue>(IFilter filter, Expression<Func<T, object>> property)
            where TValue : IValue
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new ValueQuery<T, TValue>(connection, logger, factory, filter, property);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<T> SelectForward(IFilter filter)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new ForwardLookupQuery<T>(connection, logger, factory, filter);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<TChild> SelectForward<TChild>(IFilter filter)
            where TChild : IEntity
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new ForwardLookupQuery<TChild>(connection, logger, factory, filter);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected IEnumerable<T> SelectReverse<TValue>(IFilter filter, string entityOrderByClause, Expression<Func<T, object>> property)
            where TValue : IValue
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var query = new ReverseLookupQuery<T, TValue>(connection, logger, factory, filter, entityOrderByClause, property);
                return query.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }
        */

        public T Lookup(Guid id)
        {
            var entityInfo = new EntityInfo(baseType);
            var whereClause = string.Format("{0}.{1} = @Id", entityInfo.Name, entityInfo.Identifier.Name);
            var parameters = new Dictionary<string, object> { { "@Id", id.ToString() } };
            return SelectEntities(new Filter(whereClause, parameters)).FirstOrDefault();
        }

        public T Lookup(IQuery<T> query) //ILookup lookup, IDictionary<string, object> parameters)
        {
            return SelectEntities(query).FirstOrDefault();
            //return SelectEntities(lookup.GetFilter(parameters)).FirstOrDefault();
        }

        public IEnumerable<T> Search()
        {
            var search = searches.Where(x => x.IsDefault == true).FirstOrDefault();
            if (search == null)
                throw new InvalidOperationException("No default search defined for this repository");

            return SelectEntities(search.GetFilter(new Dictionary<string, object>()));
        }

        public IEnumerable<T> Search(IQuery<T> query)//ISearch search, IDictionary<string, object> parameters)
        {
            return SelectEntities(query); //search.GetFilter(parameters));
        }

        public virtual void Initialize()
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var batch = new InitializeTypeBatch(connection, baseType, lookups, searches);
                batch.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        public void Save(T item)
        {
            Save(new List<T> { item });
        }

        public void Save(IEnumerable<T> items)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var batch = new SaveEntitiesBatch<T>(connection, items);
                batch.Execute();
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        public void Delete(T item)
        {
            Delete(new List<T> { item });
        }

        public void Delete(IEnumerable<T> items)
        {
            IDbConnection connection = null;

            try
            {
                connection = GetConnection();
                var batch = new DeleteEntitiesBatch<T>(connection, items);
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
