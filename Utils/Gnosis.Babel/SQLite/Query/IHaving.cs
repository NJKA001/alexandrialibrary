using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IHaving : IStatement, IOrderable, ICompoundable
    {
        IPredicate<IHaving> And<T>(Expression<Func<T, object>> expression);
        IPredicate<IHaving> Or<T>(Expression<Func<T, object>> expression);
    }
}
