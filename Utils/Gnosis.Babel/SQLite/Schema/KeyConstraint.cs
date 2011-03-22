using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Schema
{
    public class KeyConstraint : Statement, IKeyConstraint
    {
        private const string KeywordAsc = "asc";
        private const string KeywordDesc = "desc";

        public IKeyColumn ColumnAsc(string name)
        {
            AppendParentheticalSubListItem(name);
            return AppendWord<IKeyColumn, KeyColumn>(KeywordAsc);
        }

        public IKeyColumn ColumnDesc(string name)
        {
            AppendParentheticalSubListItem(name);
            return AppendWord<IKeyColumn, KeyColumn>(KeywordDesc);
        }
    }

    public class KeyConstraint<T> : Statement, IKeyConstraint<T>
    {
        private const string KeywordAsc = "asc";
        private const string KeywordDesc = "desc";

        public IKeyColumn<T> ColumnAsc(Expression<Func<T, object>> expression)
        {
            AppendParentheticalSubListItem(expression.ToName());
            return AppendWord<IKeyColumn<T>, KeyColumn<T>>(KeywordAsc);
        }

        public IKeyColumn<T> ColumnDesc(Expression<Func<T, object>> expression)
        {
            AppendParentheticalSubListItem(expression.ToName());
            return AppendWord<IKeyColumn<T>, KeyColumn<T>>(KeywordDesc);
        }
    }
}
