using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IIndexedField
    {
        string Name { get; }
        bool Ascending { get; }
    }
}
