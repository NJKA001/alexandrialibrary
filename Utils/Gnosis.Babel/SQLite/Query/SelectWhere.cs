using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class SelectWhere : Statement, ISelectWhere
    {
        private const string KeywordGroupBy = "group by";
        private const string KeywordOrderBy = "order by";
        private const string OpAnd = "and";
        private const string OpOr = "or";

        public IPredicate<ISelectWhere> And<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpAnd);
            return AppendWord<IPredicate<ISelectWhere>, Predicate<ISelectWhere, SelectWhere>>(expression.ToName());
        }

        public IPredicate<ISelectWhere> Or<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpOr);
            return AppendWord<IPredicate<ISelectWhere>, Predicate<ISelectWhere, SelectWhere>>(expression.ToName());
        }

        public IGroupBy GroupBy
        {
            get { return AppendClause<IGroupBy, GroupBy>(KeywordGroupBy); }
        }

        public IOrderBy OrderBy
        {
            get { return AppendClause<IOrderBy, OrderBy>(KeywordOrderBy); }
        }
    }
}
