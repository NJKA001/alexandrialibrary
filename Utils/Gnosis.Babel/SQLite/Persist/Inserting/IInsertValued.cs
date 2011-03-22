using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public interface IInsertValued
    {
        IInsertValue Values();
        IStatement Values<TModel>(TModel model, params Expression<Func<TModel, object>>[] properties) where TModel : IModel;
    }

    public interface IInsertValued<T>
        where T : IModel
    {
        IInsertValue<T> Values();
        IStatement Values(T model, IEnumerable<Expression<Func<T, object>>> properties);
    }
}
