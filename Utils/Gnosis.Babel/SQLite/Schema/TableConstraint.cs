using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class TableConstraint : TableConstrained, ITableConstraint
    {
    }

    public class TableConstraint<T> : TableConstrained<T>, ITableConstraint<T>
    {
    }
}
