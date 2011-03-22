namespace Gnosis.Babel.SQLite.Query
{
    public class Orderable : Statement, IOrderable
    {
        private const string KeywordOrderBy = "order by";

        public IOrderBy OrderBy
        {
            get { return AppendClause<IOrderBy, OrderBy>(KeywordOrderBy); }
        }
    }
}
