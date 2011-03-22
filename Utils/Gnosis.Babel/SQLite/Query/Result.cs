using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite.Query
{
    public class Result : Compoundable, IResult
    {
        private const string KeywordAllColumns = "*";
        private const string KeywordFrom = "from";

        public IResult AllColumns()
        {
            return AppendWord<IResult, Result>(KeywordAllColumns);
        }

        public IResult Column<T>(Expression<Func<T, object>> expression)
        {
            return AppendWord<IResult, Result>(expression.ToName());
        }

        public IResult Column<T>(Expression<Func<T, object>> expression, string alias)
        {
            AppendWord(expression.ToName());
            return AppendWord<IResult, Result>(alias);
        }

        public IFrom From<T>()
        {
            AppendClause(KeywordFrom);
            return AppendWord<IFrom, From>(typeof(T).AsSchemaName());
        }

        public IFrom From(string table)
        {
            AppendClause(KeywordFrom);
            return AppendWord<IFrom, From>(table);
        }

        public IFrom From(string table, string alias)
        {
            AppendClause(KeywordFrom);
            AppendWord(table);
            return AppendWord<IFrom, From>(alias);            
        }
    }
}
