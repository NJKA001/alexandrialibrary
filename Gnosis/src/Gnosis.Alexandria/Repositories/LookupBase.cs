using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public abstract class LookupBase<T>
        : ILookup
    {
        protected LookupBase(string name, string whereClause, IEnumerable<string> columns, IDictionary<string, object> parameters)
        {
            this.name = name;
            this.whereClause = whereClause;
            this.columns = columns;
            this.parameters = parameters;
        }

        private readonly string name;
        private readonly string whereClause;
        private readonly IEnumerable<string> columns;
        private readonly IDictionary<string, object> parameters;

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

        public IEnumerable<string> Columns
        {
            get { return columns; }
        }

        public IFilter GetFilter()
        {
            return new Filter(whereClause, string.Empty, parameters);
        }
    }
}
