using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITableConstraint : IStatement, ITableConstrained
    {
    }

    public interface ITableConstraint<T> : IStatement, ITableConstrained<T>
    {
    }
}
