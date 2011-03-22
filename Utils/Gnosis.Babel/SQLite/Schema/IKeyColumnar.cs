using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IKeyColumnar
    {
        IKeyColumn ColumnAsc(string name);
        IKeyColumn ColumnDesc(string name);
    }

    public interface IKeyColumnar<T>
    {
        IKeyColumn<T> ColumnAsc(Expression<Func<T, object>> expression);
        IKeyColumn<T> ColumnDesc(Expression<Func<T, object>> expression);
    }
}
