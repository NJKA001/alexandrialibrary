using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class Grouping : GroupBy, IGrouping
    {
        private const string KeywordHaving = "having";
        private const string KeywordOrderBy = "order by";

        public IPredicate<IHaving> Having<T>(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IOrderBy OrderBy
        {
            get { return AppendClause<IOrderBy, OrderBy>(KeywordOrderBy); }
        }
    }
}
