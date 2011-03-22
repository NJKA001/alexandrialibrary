using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public class Where : Statement, IWhere
    {
        private const string OpAnd = "and";
        private const string OpOr = "or";

        public IPredicate<IWhere> And<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpAnd);
            return AppendWord<IPredicate<IWhere>, Predicate<IWhere, Where>>(expression.ToName());
        }

        public IPredicate<IWhere> Or<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpOr);
            return AppendWord<IPredicate<IWhere>, Predicate<IWhere, Where>>(expression.ToName());
        }
    }
}
