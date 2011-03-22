using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdateColumnar
    {
        IUpdateColumn ColumnAndValue<TModel>(TModel model, Expression<Func<TModel, object>> property)
            where TModel : IModel;
        IUpdateColumn ColumnsAndValues<TModel>(TModel model, params Expression<Func<TModel, object>>[] properties)
            where TModel : IModel;
    }

    public interface IUpdateColumnar<T>
        where T : IModel
    {
        IUpdateColumn<T> ColumnAndValue(T model, Expression<Func<T, object>> property);
        IUpdateColumn<T> ColumnsAndValues(T model, IEnumerable<Expression<Func<T, object>>> properties);
    }
}
