using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public class InsertValue : Statement, IInsertValue
    {
        public IInsertValue Value<TModel>(TModel model, Expression<Func<TModel, object>> property)
            where TModel : IModel
        {
            return AppendParentheticalListItem<IInsertValue, InsertValue, TModel>(property.ToName(), model, property);
        }
    }

    public class InsertValue<T> : Statement, IInsertValue<T>
        where T : IModel
    {
        public IInsertValue<T> Value(T model, Expression<Func<T, object>> property)
        {
            return AppendParameter<IInsertValue<T>, InsertValue<T>, T>(property.ToName(), model, property);
        }
    }
}
