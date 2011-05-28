using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IFilter
    {
        string WhereClause { get; }
        string OrderByClause { get; }
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
    }
}
