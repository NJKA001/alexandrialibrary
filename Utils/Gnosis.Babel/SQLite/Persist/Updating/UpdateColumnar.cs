using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public class UpdateColumnar : Statement, IUpdateColumnar
    {
        private const string KeywordAssign = "=";

        public IUpdateColumn ColumnAndValue<TModel>(TModel model, Expression<Func<TModel, object>> property)
            where TModel : IModel
        {
            var name = property.ToName();
            AppendListItem(name);
            AppendWord(KeywordAssign);
            return AppendParameter<IUpdateColumn, UpdateColumn, TModel>(name, model, property);
        }

        public IUpdateColumn ColumnsAndValues<TModel>(TModel model, params Expression<Func<TModel, object>>[] properties)
            where TModel : IModel
        {
            foreach (var property in properties)
            {
                var name = property.ToName();
                AppendListItem(name);
                AppendWord(KeywordAssign);
                AppendParameter<TModel>(name, model, property);
            }

            return Transform<IUpdateColumn, UpdateColumn>();
        }
    }

    public class UpdateColumnar<T> : Statement, IUpdateColumnar<T>
        where T : IModel
    {
        private const string KeywordAssign = "=";

        public IUpdateColumn<T> ColumnAndValue(T model, Expression<Func<T, object>> property)
        {
            var name = property.ToName();
            AppendListItem(name);
            AppendWord(KeywordAssign);
            return AppendParameter<IUpdateColumn<T>, UpdateColumn<T>, T>(name, model, property);
        }

        public IUpdateColumn<T> ColumnsAndValues(T model, IEnumerable<Expression<Func<T, object>>> properties)
        {
            foreach (var property in properties)
            {
                var name = property.ToName();
                AppendListItem(name);
                AppendWord(KeywordAssign);
                AppendParameter<T>(name, model, property);
            }

            return Transform<IUpdateColumn<T>, UpdateColumn<T>>();
        }
    }
}
