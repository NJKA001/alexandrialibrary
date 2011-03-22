using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface ICompoundable
    {
        ICompound Union { get; }
        ICompound UnionAll { get; }
        ICompound Intersect { get; }
        ICompound Except { get; }
    }
}
