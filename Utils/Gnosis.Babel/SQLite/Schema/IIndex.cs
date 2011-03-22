using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IIndex : IStatement
    {
        IIndexTable On(string table);
    }

    public interface IIndex<T> : IStatement
    {
        IIndexTable<T> On(string table);
    }
}
