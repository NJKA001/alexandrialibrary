using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IResourceQuery
    {
        IEnumerable<KeyValuePair<string, string>> Parameters { get; }
    }
}
