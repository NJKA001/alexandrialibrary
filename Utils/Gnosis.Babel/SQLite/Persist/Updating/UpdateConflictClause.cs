using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public class UpdateConflictClause : Statement, IUpdateConflictClause
    {
        public IUpdateSet Table(string table)
        {
            return AppendClause<IUpdateSet, UpdateSet>(table);
        }
    }

    public class UpdateConflictClause<T> : Statement, IUpdateConflictClause<T>
        where T : IModel
    {
        public IUpdateSet<T> Table(string table)
        {
            return AppendClause<IUpdateSet<T>, UpdateSet<T>>(table);
        }
    }
}
