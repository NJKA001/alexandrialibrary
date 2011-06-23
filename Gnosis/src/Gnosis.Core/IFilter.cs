using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Commands;

namespace Gnosis.Core
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
