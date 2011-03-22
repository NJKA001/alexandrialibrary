using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IView : IStatement
    {
        ISelect AsSelectAll { get; }
        ISelect AsSelectDistinct { get; }
    }

    public interface IView<T> : IStatement
    {
        ISelect AsSelectAll { get; }
        ISelect AsSelectDistinct { get; }
    }
}
