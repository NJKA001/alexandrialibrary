using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnar
    {
        IColumnName Column(string name);
        IColumnType Column(string name, string type);
    }

    public interface IColumnar<T>
    {
        IColumnName<T> Column(Expression<Func<T, object>> expression);
        IColumnType<T> Column(Expression<Func<T, object>> expression, string type);
    }
}
