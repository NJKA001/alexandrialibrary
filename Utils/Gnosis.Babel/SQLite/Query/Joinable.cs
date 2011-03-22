namespace Gnosis.Babel.SQLite.Query
{
    public class Joinable : Statement, IJoinable
    {
        private const string KeywordInnerJoin = "inner join";
        private const string KeywordLeftOuterJoin = "left outer";

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
