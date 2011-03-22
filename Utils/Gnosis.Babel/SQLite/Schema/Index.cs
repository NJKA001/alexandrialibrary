using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class Index : Statement, IIndex
    {
        private const string KeywordOn = "on";

        public IIndexTable On(string table)
        {
            AppendClause(KeywordOn);
            return AppendWord<IIndexTable, IndexTable>(table);
        }
    }

    public class Index<T> : Statement, IIndex<T>
    {
        private const string KeywordOn = "on";

        public IIndexTable<T> On(string table)
        {
            AppendClause(KeywordOn);
            return AppendWord<IIndexTable<T>, IndexTable<T>>(table);
        }
    }
}
