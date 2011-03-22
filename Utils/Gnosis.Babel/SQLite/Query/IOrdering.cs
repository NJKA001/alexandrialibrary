using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IOrdering : IStatement, IOrderBy
    {
        ILimit Limit(int limit);
        ILimit Limit(int limit, int offset);
    }
}
