using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface IOrderable
    {
        IOrderBy OrderBy { get; }
    }
}
