using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class ColumnType : ColumnConstrained, IColumnType
    {
    }

    public class ColumnType<T> : ColumnConstrained<T>, IColumnType<T>
    {
    }
}
