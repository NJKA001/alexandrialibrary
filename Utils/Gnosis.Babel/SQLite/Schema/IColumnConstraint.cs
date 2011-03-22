using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IColumnConstraint : IStatement, IColumnar, IColumnConstrained, ITableConstrained
    {
    }

    public interface IColumnConstraint<T> : IStatement, IColumnar<T>, IColumnConstrained<T>, ITableConstrained<T>
    {
    }
}
