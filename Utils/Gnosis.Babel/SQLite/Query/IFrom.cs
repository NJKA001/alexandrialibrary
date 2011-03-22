using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IFrom : IStatement, IGroupable, IOrderable, ICompoundable, IJoinable
    {
        IPredicate<ISelectWhere> Where(string expression);
        IPredicate<ISelectWhere> Where<T>(Expression<Func<T, object>> expression);
    }
}
