using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

using log4net;

using Gnosis.Core;
using Gnosis.Core.Batches;
using Gnosis.Core.Commands;
using Gnosis.Core.Queries;

using Gnosis.Alexandria.Helpers;
//using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IEntity
    {
        protected RepositoryBase(IContext context, IFactory factory, ILogger logger)
        {
            this.context = context;
            this.factory = factory;
            this.logger = logger;
            this.baseType = typeof(T);
        }

        private readonly IContext context;
        private readonly IFactory factory;
        private readonly ILogger logger;
        private readonly Type baseType;
        private readonly IList<ILookup> lookups = new List<ILookup>();
        private readonly IList<ISearch> searches = new List<ISearch>();
        private string database = "Catalog.db";

        protected void Initialize()
        {
            try
            {
                var batch = new InitializeTypeBatch(() => GetConnection(), logger, baseType, lookups, searches);
                batch.Execute();
            }
            catch (Exception ex)
            {
                logger.Error("RepositoryBase.Initialize", ex);
            }
        }
        
        protected IContext Context
        {
            get { return context; }
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
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", database));
        }

        protected IEnumerable<T> Select(IFilter filter)
        {
            var query = new Query<T>(() => GetConnection(), logger, factory, filter);
            return query.Execute();
        }

        public T Lookup(Guid id)
        {
            var entityInfo = new EntityInfo(baseType);
            var whereClause = string.Format("{0}.{1} = @Id", entityInfo.Name, entityInfo.Identifier.Name);
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            return Select(new Filter(whereClause, parameters)).FirstOrDefault();
        }

        public T Lookup(ILookup lookup)
        {
            return Select(lookup.GetFilter()).FirstOrDefault();
        }

        public IEnumerable<T> Search()
        {
            var search = searches.Where(x => x.IsDefault == true).FirstOrDefault();
            if (search == null)
                throw new InvalidOperationException("No default search defined for this repository");

            return Select(search.GetFilter());
        }

        public IEnumerable<T> Search(ISearch search)
        {
            return Select(search.GetFilter());
        }

        public void Save(IEnumerable<T> items)
        {
            var batch = new SaveEntitiesBatch<T>(() => GetConnection(), logger, items);
            batch.Execute();
        }

        public void Delete(IEnumerable<T> items)
        {
            var batch = new DeleteEntitiesBatch<T>(() => GetConnection(), logger, items);
            batch.Execute();
        }
    }
}
