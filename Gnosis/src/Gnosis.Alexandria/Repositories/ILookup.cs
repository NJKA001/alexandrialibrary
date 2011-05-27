using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories
{
    public interface ILookup
    {
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        string WhereClause { get; }
    }

    public interface ILookup<T>
        : ILookup
    {
    }
}
