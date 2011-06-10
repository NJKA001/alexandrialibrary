using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILookup
    {
        string Name { get; }
        string SourceName { get; }
        IEnumerable<string> Columns { get; }
        IFilter GetFilter(IDictionary<string, object> parameters);
    }
}
