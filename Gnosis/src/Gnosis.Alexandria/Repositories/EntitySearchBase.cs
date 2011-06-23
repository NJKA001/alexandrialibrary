using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class EntitySearchBase<T>
        : ISearch
        where T : IEntity
    {
        protected EntitySearchBase(params Expression<Func<T, object>>[] columns)
            : this(string.Empty, string.Empty, columns)
        {
        }

        protected EntitySearchBase(string whereClause, params Expression<Func<T, object>>[] columns)
            : this(whereClause, string.Empty, columns)
        {
        }

        protected EntitySearchBase(string whereClause, string joinClause, params Expression<Func<T, object>>[] columns)
        {
            this.entityInfo = new EntityInfo(typeof(T));
            this.whereClause = whereClause;
            this.joinClause = joinClause;

            var orderByBuilder = new StringBuilder();
            foreach (var column in columns)
            {
                var columnName = column.AsProperty().Name;
                this.columns.Add(columnName, true);

                if (orderByBuilder.Length > 0)
                    orderByBuilder.Append(", ");

                orderByBuilder.AppendFormat("{0}.{1} ASC", entityInfo.Name, columnName);
            }
            this.orderByClause = orderByBuilder.ToString();
        }

        private readonly EntityInfo entityInfo;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly string joinClause;
        private readonly IDictionary<string, bool> columns = new Dictionary<string, bool>();

        public string Name
        {
            get { return string.Format("{0}_{1}", SourceName, this.GetType().Name ); }
        }

        public string SourceName
        {
            get { return entityInfo.Name; }
        }

        public bool IsDefault
        {
            get { return string.IsNullOrEmpty(whereClause); }
        }

        public IEnumerable<string> Columns
        {
            get { return columns.Keys; }
        }

        public IFilter GetFilter(IDictionary<string, object> parameters)
        {
            return new Filter(whereClause, orderByClause, joinClause, true, parameters);
        }
    }
}
