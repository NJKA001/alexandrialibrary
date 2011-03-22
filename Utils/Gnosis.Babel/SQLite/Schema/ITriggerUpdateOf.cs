using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITriggerUpdateOf : IStatement
    {
        ITriggerUpdateOf Or(string column);
        ITriggerType On(string table);
    }

    public interface ITriggerUpdateOf<T> : IStatement
    {
        ITriggerUpdateOf<T> Or(Expression<Func<T, object>> column);
        ITriggerType<T> On(string table);
    }
}
