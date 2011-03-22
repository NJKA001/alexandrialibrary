namespace Gnosis.Babel.SQLite.Query
{
    public class Select : Statement, ISelect
    {
        public Select()
        {
        }

        private const string KeywordAll = "all";
        private const string KeywordDistinct = "distinct";
        private const string FunctionLastInsertRowId = "last_insert_rowid()";

        public IResult All
        {
            get { return AppendWord<IResult, Result>(KeywordAll); }
        }

        public IResult Distinct
        {
            get { return AppendWord<IResult, Result>(KeywordDistinct); }
        }

        public IResult LastInsertRowId
        {
            get { return AppendWord<IResult, Result>(FunctionLastInsertRowId); }
        }
    }
}
