namespace Gnosis.Babel.SQLite.Query
{
    public class Compound : Statement, ICompound
    {
        private const string KeywordSelect = "select";

        public ISelect Select
        {
            get { return AppendWord<ISelect, Select>(KeywordSelect); }
        }
    }
}
