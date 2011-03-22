using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface ISelectWhere : IStatement, IGroupable, IOrderable
    {
        IPredicate<ISelectWhere> And<T>(Expression<Func<T, object>> expression);
        IPredicate<ISelectWhere> Or<T>(Expression<Func<T, object>> expression);
    }
}
