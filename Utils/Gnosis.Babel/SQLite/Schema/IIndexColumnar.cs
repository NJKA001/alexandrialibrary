using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IIndexColumnar : IStatement
    {
        IIndexColumnar ColumnAsc(string name);
        IIndexColumnar ColumnDesc(string name);
        IIndexColumnar Columns(IEnumerable<IIndexedField> fields);
    }

    public interface IIndexColumnar<T> : IStatement
    {
        IIndexColumnar<T> ColumnAsc(Expression<Func<T, object>> expression);
        IIndexColumnar<T> ColumnDesc(Expression<Func<T, object>> expression);
        IIndexColumnar<T> Columns(IEnumerable<IIndexedField> fields);
    }
}
