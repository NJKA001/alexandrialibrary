using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IIndexTable : IStatement, IIndexColumnar
    {
    }

    public interface IIndexTable<T> : IStatement, IIndexColumnar<T>
    {
    }
}
