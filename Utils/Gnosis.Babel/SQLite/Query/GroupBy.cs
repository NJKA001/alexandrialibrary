using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class GroupBy : Statement, IGroupBy
    {
        public IGrouping Column(string expression)
        {
            return AppendListItem<IGrouping, Grouping>(expression);
        }

        public IGrouping Column<T>(Expression<Func<T, object>> expression)
        {
            return AppendListItem<IGrouping, Grouping>(expression.ToName());
        }
    }
}
