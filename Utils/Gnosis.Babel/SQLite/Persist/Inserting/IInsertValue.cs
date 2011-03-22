using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertValue : IStatement
    {
        IInsertValue Value<TModel>(TModel model, Expression<Func<TModel, object>> property) where TModel : IModel;
    }

    public interface IInsertValue<T> : IStatement
        where T : IModel
    {
        IInsertValue<T> Value(T model, Expression<Func<T, object>> property);
    }

}
