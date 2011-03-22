using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Persist
{
    public interface IConflicting<S>
        where S : IStatement
    {
        S OrAbort { get; }
        S OrFail { get; }
        S OrIgnore { get; }
        S OrReplace { get; }
        S OrRollback { get; }
    }
}
