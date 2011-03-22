using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist.Updating
{
    public interface IUpdate : IStatement, IUpdatable, IConflicting<IUpdateConflictClause>
    {
    }

    public interface IUpdate<T> : IStatement, IUpdatable<T>, IConflicting<IUpdateConflictClause<T>>
        where T : IModel
    {
    }
}
