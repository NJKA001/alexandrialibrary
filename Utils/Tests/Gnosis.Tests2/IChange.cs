using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public interface IChange
    {
        string Text { get; }
        IEnumerable<KeyValuePair<string, object>> Data { get; }
    }
}
