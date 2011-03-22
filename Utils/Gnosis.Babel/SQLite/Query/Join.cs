using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class Join : Statement, IJoin
    {
        private const string KeywordOn = "on";

        public IPredicate<IJoinCondition> On<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(KeywordOn);
            return AppendWord<IPredicate<IJoinCondition>, Predicate<IJoinCondition, JoinCondition>>(expression.ToName());
        }

        public IPredicate<IJoinCondition> On(string expression)
        {
            AppendWord(KeywordOn);
            return AppendWord<IPredicate<IJoinCondition>, Predicate<IJoinCondition, JoinCondition>>(expression);
        }
    }
}
