namespace Gnosis.Babel.SQLite.Query
{
    public class Groupable : Statement, IGroupable
    {
        private const string KeywordGroupBy = "group by";

        public IGroupBy GroupBy
        {
            get { return AppendClause<IGroupBy, GroupBy>(KeywordGroupBy); }
        }
    }
}
