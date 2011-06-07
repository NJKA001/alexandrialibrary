using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILookup
    {
        string Name { get; }
        Type BaseType { get; }
        string WhereClause { get; }
        IEnumerable<string> Columns { get; }
        IFilter GetFilter();
    }
}
