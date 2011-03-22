namespace Gnosis.Babel.SQLite.Query
{
    public class Ordering : OrderBy, IOrdering
    {
        private const string KeywordLimit = "limit";
        private const string KeywordOffset = "offset";

        public ILimit Limit(int limit)
        {
            AppendClause(KeywordLimit);
            return AppendWord<ILimit, Limit>(limit.ToString());
        }

        public ILimit Limit(int limit, int offset)
        {
            AppendClause(KeywordLimit);
            AppendWord(limit.ToString());
            AppendWord(KeywordOffset);
            return AppendWord<ILimit, Limit>(offset.ToString());
        }
    }
}
