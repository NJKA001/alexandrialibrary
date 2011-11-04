using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;

namespace Gnosis.Alexandria.Repositories
{
    public class ValueSearchBase<TEntity, TValue>
        : ISearch
        where TEntity : IEntity
        where TValue : IValue
    {
        protected ValueSearchBase(string whereClause, Expression<Func<TEntity, IEnumerable<TValue>>> valueExpression, params Expression<Func<TValue, object>>[] columns)
        {
            this.entityInfo = new EntityInfo(typeof(TEntity));
            this.whereClause = whereClause;

            if (valueExpression != null)
            {
                this.property = valueExpression.ToPropertyInfo();
                if (this.property != null)
                {
                    this.valueInfo = new ValueInfo(entityInfo, property, typeof(TValue));
                }
            }

            var orderByBuilder = new StringBuilder();
            foreach (var column in columns)
            {
                var columnName = column.ToPropertyInfo().Name;
                this.columns.Add(columnName, true);

                if (orderByBuilder.Length > 0)
                    orderByBuilder.Append(", ");

                orderByBuilder.AppendFormat("{0}.{1} ASC", valueInfo.Name, columnName);
            }
            this.orderByClause = orderByBuilder.ToString();
        }

        private readonly EntityInfo entityInfo;
        private readonly ValueInfo valueInfo;
        private readonly PropertyInfo property;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IDictionary<string, bool> columns = new Dictionary<string, bool>();

        #region ISearch Members

        public string Name
        {
            get { return string.Format("{0}_{1}", SourceName, this.GetType().Name); }
        }

        public string SourceName
        {
            get { return valueInfo.Name; }
        }

        public bool IsDefault
        {
            get { return false; }
        }

        public IEnumerable<string> Columns
        {
            get { return columns.Keys; }
        }

        public IFilter GetFilter(IDictionary<string, object> parameters)
        {
            return new Filter(whereClause, orderByClause, string.Empty, false, parameters);
        }

        #endregion
    }
}
