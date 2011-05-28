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
        protected LookupBase(string name, string whereClause, IEnumerable<string> columns)
        {
            this.name = name;
            this.whereClause = whereClause;
            this.columns = columns;
        }

        private readonly string name;
        private readonly string whereClause;
        private readonly IEnumerable<string> columns;

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

        public IFilter GetFilter(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return new Filter(whereClause, string.Empty, parameters);
        }
    }
}
