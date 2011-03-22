using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IResult : IStatement, ICompoundable, ISourcable
    {
        IResult AllColumns();
        IResult Column<T>(Expression<Func<T, object>> expression);
        IResult Column<T>(Expression<Func<T, object>> expression, string alias);
    }
}
