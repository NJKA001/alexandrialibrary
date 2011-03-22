using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IGrouping : IStatement, IOrderable, IGroupBy
    {
        IPredicate<IHaving> Having<T>(Expression<Func<T, object>> expression);
    }
}
