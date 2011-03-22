using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Inserting
{
    public class InsertConflictClause : Statement, IInsertConflictClause
    {
        private const string KeywordInto = "into";

        public IInsertColumn Into(string table)
        {
            AppendClause(KeywordInto);
            return AppendWord<IInsertColumn, InsertColumn>(table);
        }
    }

    public class InsertConflictClause<T> : Statement, IInsertConflictClause<T>
        where T : IModel
    {
        private const string KeywordInto = "into";

        public IInsertColumn<T> Into(string table)
        {
            AppendClause(KeywordInto);
            return AppendWord<IInsertColumn<T>, InsertColumn<T>>(table);
        }
    }
}
