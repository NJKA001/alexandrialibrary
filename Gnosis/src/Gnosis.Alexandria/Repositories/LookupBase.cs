using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class LookupBase<T>
        : ILookup
        where T : IEntity
    {
        protected LookupBase(string whereClause, params Expression<Func<T, object>>[] columns)
        {
            this.entityInfo = new EntityInfo(typeof(T));
            this.whereClause = whereClause;

            foreach (var column in columns)
            {
                var columnName = column.AsProperty().Name;
                this.columns.Add(columnName);
            }
        }

        private readonly EntityInfo entityInfo;
        private readonly string whereClause;
        private readonly IList<string> columns = new List<string>();

        public string Name
        {
            get { return string.Format("{0}_{1}", SourceName, this.GetType().Name); }
        }

        public string SourceName
        {
            get { return entityInfo.Name; }
        }

        public IEnumerable<string> Columns
        {
            get { return columns; }
        }

        public IFilter GetFilter(IDictionary<string, object> parameters)
        {
            return new Filter(whereClause, string.Empty, parameters);
        }
    }
}
