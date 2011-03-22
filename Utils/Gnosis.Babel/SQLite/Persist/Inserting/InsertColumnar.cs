using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public class InsertColumnar : Statement, IInsertColumnar
    {
        public IInsertColumn Column(string name)
        {
            return AppendParentheticalListItem<IInsertColumn, InsertColumn>(name);
        }

        public IInsertColumn Columns(IEnumerable<string> names)
        {
            foreach (var name in names)
                AppendParentheticalListItem(name);

            return Transform<IInsertColumn, InsertColumn>();
        }
    }

    public class InsertColumnar<T> : Statement, IInsertColumnar<T>
        where T : IModel
    {
        public IInsertColumn<T> Column(Expression<Func<T, object>> expression)
        {
            return AppendParentheticalListItem<IInsertColumn<T>, InsertColumn<T>>(expression.ToName());
        }

        public IInsertColumn<T> Columns(IEnumerable<Expression<Func<T, object>>> expressions)
        {
            foreach (var expression in expressions)
                AppendParentheticalListItem(expression.ToName());

            return Transform<IInsertColumn<T>, InsertColumn<T>>();
        }
    }
}
