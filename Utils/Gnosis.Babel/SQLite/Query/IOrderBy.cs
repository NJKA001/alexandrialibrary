using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IOrderBy : IStatement
    {
        IOrdering Ascending(string expression);
        IOrdering Ascending<T>(Expression<Func<T, object>> expression);
        IOrdering Descending(string expression);
        IOrdering Descending<T>(Expression<Func<T, object>> expression);
    }
}
