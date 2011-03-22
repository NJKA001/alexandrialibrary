namespace Gnosis.Babel.SQLite.Query
{
    public class Sourcable : Statement, ISourcable
    {
        private const string KeywordFrom = "from";

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
