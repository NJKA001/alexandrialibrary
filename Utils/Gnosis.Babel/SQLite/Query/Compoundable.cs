namespace Gnosis.Babel.SQLite.Query
{
    public class Compoundable : Statement, ICompoundable
    {
        private const string KeywordUnion = "union";
        private const string KeywordUnionAll = "union all";
        private const string KeywordIntersect = "intersect";
        private const string KeywordExcept = "except";

        public ICompound Union
        {
            get { return AppendClause<ICompound, Compound>(KeywordUnion); }
        }

        public ICompound UnionAll
        {
            get { return AppendClause<ICompound, Compound>(KeywordUnionAll); }
        }

        public ICompound Intersect
        {
            get { return AppendClause<ICompound, Compound>(KeywordIntersect); }
        }

        public ICompound Except
        {
            get { return AppendClause<ICompound, Compound>(KeywordExcept); }
        }
    }
}
