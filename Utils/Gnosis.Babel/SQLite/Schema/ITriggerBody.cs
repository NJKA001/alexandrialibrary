using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITriggerBody : IStatement
    {
        ITriggerBody Do(string expression);
        IStatement End { get; }
    }

    public interface ITriggerBody<T> : IStatement
    {
        ITriggerBody<T> Do(string expression);
        IStatement End { get; }
    }
}
