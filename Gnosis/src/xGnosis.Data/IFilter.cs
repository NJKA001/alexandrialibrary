using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data.Commands;

namespace Gnosis.Data
{
    public interface IFilter
    {
        string WhereClause { get; }
        string OrderByClause { get; }
        string JoinClause { get; }
        bool AutoJoin { get; }
        IEnumerable<IParameter> Parameters { get; }
    }
}
