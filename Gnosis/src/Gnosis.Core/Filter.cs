using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Filter
        : IFilter
    {
        public Filter(string whereClause, IEnumerable<KeyValuePair<string, object>> parameters)
            : this(whereClause, string.Empty, parameters)
        {
        }

        public Filter(string whereClause, string orderByClause, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            this.whereClause = whereClause;
            this.orderByClause = orderByClause;
            this.parameters = parameters;
        }

        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly IEnumerable<KeyValuePair<string, object>> parameters;

        public string WhereClause
        {
            get { return whereClause; }
        }

        public string OrderByClause
        {
            get { return orderByClause; }
        }

        public IEnumerable<KeyValuePair<string, object>> Parameters
        {
            get { return parameters; }
        }
    }
}
