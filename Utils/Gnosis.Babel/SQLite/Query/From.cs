using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class From : Compoundable, IFrom
    {
        private const string KeywordGroupBy = "group by";
        private const string KeywordInnerJoin = "inner join";
        private const string KeywordLeftOuterJoin = "left outer join";
        private const string KeywordOrderBy = "order by";
        private const string KeywordWhere = "where";

        public IPredicate<ISelectWhere> Where(string expression)
        {
            AppendClause(KeywordWhere);
            return AppendWord<IPredicate<ISelectWhere>, Predicate<ISelectWhere, SelectWhere>>(expression);
        }

        public IPredicate<ISelectWhere> Where<T>(Expression<Func<T, object>> expression)
        {
            AppendClause(KeywordWhere);
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

        public IJoin InnerJoin(string table, string alias)
        {
            AppendWord(KeywordInnerJoin);
            AppendWord(table);
            return AppendWord<IJoin, Join>(alias);
        }

        public IJoin LeftOuterJoin(string table, string alias)
        {
            AppendWord(KeywordLeftOuterJoin);
            AppendWord(table);
            return AppendWord<IJoin, Join>(alias);
        }
    }
}
