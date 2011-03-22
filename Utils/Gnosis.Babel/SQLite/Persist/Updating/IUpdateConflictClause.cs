using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdateConflictClause : IStatement, IUpdatable
    {
    }

    public interface IUpdateConflictClause<T> : IStatement, IUpdatable<T>
        where T : IModel
    {
    }
}
