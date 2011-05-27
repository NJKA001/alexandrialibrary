using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class SearchBase<T>
        : ISearch<T>
    {
        protected SearchBase(string name, string whereClause, string orderByClause, IEnumerable<KeyValuePair<string, object>> parameters)
            : this(name, whereClause, orderByClause, parameters, false)
        {
        }

        protected SearchBase(string name, string whereClause, string orderByClause, IEnumerable<KeyValuePair<string, object>> parameters, bool isDefault)
        {
            this.name = name;
            this.whereClause = whereClause;
            this.orderByClause = orderByClause;
            this.parameters = parameters;
            this.isDefault = isDefault;
        }

        private readonly string name;
        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<KeyValuePair<string, object>> parameters;
        private readonly bool isDefault;

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return parameters; }
        }

        public string Name
        {
            get { return name; }
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
    }
}
