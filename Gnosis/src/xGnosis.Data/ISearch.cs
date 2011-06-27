using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface ISearch
    {
        string Name { get; }
        string SourceName { get; }
        bool IsDefault { get; }
        IEnumerable<string> Columns { get; }
        IFilter GetFilter(IDictionary<string, object> parameters);
    }
}
