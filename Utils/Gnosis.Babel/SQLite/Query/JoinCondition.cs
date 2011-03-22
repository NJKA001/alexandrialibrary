using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class JoinCondition : Joinable, IJoinCondition
    {
        private const string KeywordAnd = "and";
        private const string KeywordOr = "or";
        private const string KeywordWhere = "where";

        public IJoin And<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(KeywordAnd);
            return AppendWord<IJoin, Join>(expression.ToName());
        }

        public IJoin Or<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(KeywordOr);
            return AppendWord<IJoin, Join>(expression.ToName());
        }

        public IPredicate<IWhere> Where<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(KeywordWhere);
            return AppendWord<IPredicate<IWhere>, Predicate<IWhere, Where>>(expression.ToName());
        }
    }
}
