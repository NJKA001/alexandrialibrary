using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertColumnar
    {
        IInsertColumn Column(string name);
        IInsertColumn Columns(IEnumerable<string> names);
    }

    public interface IInsertColumnar<T>
        where T : IModel
    {
        IInsertColumn<T> Column(Expression<Func<T, object>> name);
        IInsertColumn<T> Columns(IEnumerable<Expression<Func<T, object>>> names);
    }
}
