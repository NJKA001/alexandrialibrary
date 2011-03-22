using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class OrderBy : Statement, IOrderBy
    {
        private const string KeywordAsc = "asc";
        private const string KeywordDesc = "desc";

        public IOrdering Ascending(string expression)
        {
            AppendListItem(expression);
            return AppendWord<IOrdering, Ordering>(KeywordAsc);
        }

        public IOrdering Ascending<T>(Expression<Func<T, object>> expression)
        {
            AppendListItem(expression.ToName());
            return AppendWord<IOrdering, Ordering>(KeywordAsc);
        }

        public IOrdering Descending(string expression)
        {
            AppendListItem(expression);
            return AppendWord<IOrdering, Ordering>(KeywordDesc);
        }

        public IOrdering Descending<T>(Expression<Func<T, object>> expression)
        {
            AppendListItem(expression.ToName());
            return AppendWord<IOrdering, Ordering>(KeywordDesc);
        }
    }
}
