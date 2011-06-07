using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class SearchBase<T>
        : ISearch
    {
        protected SearchBase(string name, string whereClause, string orderByClause, IEnumerable<string> columns, IDictionary<string, object> parameters)
            : this(name, whereClause, orderByClause, columns, parameters, false)
        {
        }

        protected SearchBase(string name, string whereClause, string orderByClause, IEnumerable<string> columns, IDictionary<string, object> parameters, bool isDefault)
        {
            this.name = name;
            this.whereClause = whereClause;
            this.orderByClause = orderByClause;
            this.columns = columns;
            this.parameters = parameters;
            this.isDefault = isDefault;
        }

        private readonly string name;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<string> columns;
        private readonly IDictionary<string, object> parameters;
        private readonly bool isDefault;

        public string Name
        {
            get { return name; }
        }

        public Type BaseType
        {
            get { return typeof(T); }
        }

        public string WhereClause
        {
            get { return whereClause; }
        }

        public string OrderByClause
        {
            get { return orderByClause; }
        }

        public bool IsDefault
        {
            get { return isDefault; }
        }

        public IEnumerable<string> Columns
        {
            get { return columns; }
        }

        public IFilter GetFilter()
        {
            return new Filter(whereClause, orderByClause, parameters);
        }
    }
}
