using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ForeignKeyConstraint : Statement, IForeignKeyConstraint
    {
        public IForeignKeyColumn Column(string name)
        {
            return AppendParentheticalSubListItem<IForeignKeyColumn, ForeignKeyColumn>(name);
        }
    }

    public class ForeignKeyConstraint<T> : Statement, IForeignKeyConstraint<T>
    {
        public IForeignKeyColumn<T> Column(Expression<Func<T, object>> expression)
        {
            return AppendParentheticalSubListItem<IForeignKeyColumn<T>, ForeignKeyColumn<T>>(expression.ToName());
        }
    }
}
