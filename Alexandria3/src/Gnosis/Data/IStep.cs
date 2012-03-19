using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public interface IStep
    {
        string Text { get; }
        IEnumerable<KeyValuePair<string, object>> Items { get; }

        void AddItem(string name, object value);
    }
}
