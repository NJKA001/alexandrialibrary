using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core
{
    public class Filter
        : IFilter
    {
        public Filter(string whereClause, string orderByClause)
            : this(whereClause, orderByClause, string.Empty, new Dictionary<string, object>())
        {
        }

        public Filter(string whereClause, IEnumerable<KeyValuePair<string, object>> parameters)
            : this(whereClause, string.Empty, string.Empty, parameters)
        {
        }

        public Filter(string whereClause, string orderByClause, IEnumerable<KeyValuePair<string, object>> parameters)
            : this(whereClause, orderByClause, string.Empty, parameters)
        {
        }

        public Filter(string whereClause, string orderByClause, string joinClause, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            this.whereClause = whereClause;
            this.orderByClause = orderByClause;
            this.joinClause = joinClause;
            this.parameters = parameters.Select(x => new Parameter(x.Key, x.Value, false));
        }

        private readonly string whereClause;
        private readonly string orderByClause;
        private readonly string joinClause;
        private readonly IEnumerable<IParameter> parameters;

        public string WhereClause
        {
            get { return whereClause; }
        }

        public string OrderByClause
        {
            get { return orderByClause; }
        }

        public string JoinClause
        {
            get { return joinClause; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { return parameters; }
        }
    }
}
