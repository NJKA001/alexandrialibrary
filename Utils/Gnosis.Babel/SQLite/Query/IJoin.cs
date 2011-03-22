using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IJoin : IStatement
    {
        IPredicate<IJoinCondition> On(string expression);
        IPredicate<IJoinCondition> On<T>(Expression<Func<T, object>> expression);
    }
}
