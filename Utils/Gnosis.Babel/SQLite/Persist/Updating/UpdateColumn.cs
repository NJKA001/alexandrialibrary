using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public class UpdateColumn : UpdateColumnar, IUpdateColumn
    {
        private const string KeywordWhere = "where";

        public IPredicate<IWhere> Where<T>(Expression<Func<T, object>> expression)
        {
            AppendClause(KeywordWhere);
            return AppendWord<IPredicate<IWhere>, Predicate<IWhere, Where>>(expression.ToName());
        }
    }

    public class UpdateColumn<T> : UpdateColumnar<T>, IUpdateColumn<T>
        where T : IModel
    {
        private const string KeywordWhere = "where";

        public IPredicate<IWhere> Where<TOther>(Expression<Func<TOther, object>> expression)
            where TOther : IModel
        {
            AppendClause(KeywordWhere);
            return AppendWord<IPredicate<IWhere>, Predicate<IWhere, Where>>(expression.ToName());
        }

        public IPredicate<IWhere> Where(Expression<Func<T, object>> expression)
        {
            AppendClause(KeywordWhere);
            return AppendWord<IPredicate<IWhere>, Predicate<IWhere, Where>>(expression.ToName());
        }
    }
}
