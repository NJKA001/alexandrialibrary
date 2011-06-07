using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISearch
    {
        string Name { get; }
        Type BaseType { get; }
        string WhereClause { get; }
        string OrderByClause { get; }
        bool IsDefault { get; }
        IEnumerable<string> Columns { get; }
        IFilter GetFilter();
    }
}
