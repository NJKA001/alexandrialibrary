using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IGroupBy : IStatement
    {
        IGrouping Column(string expression);
        IGrouping Column<T>(Expression<Func<T, object>> expression);
    }
}
