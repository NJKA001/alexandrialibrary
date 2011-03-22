using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public class IndexTable : IndexColumnar, IIndexTable
    {
    }

    public class IndexTable<T> : IndexColumnar<T>, IIndexTable<T>
    {
    }
}
