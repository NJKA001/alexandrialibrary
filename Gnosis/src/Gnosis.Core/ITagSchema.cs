using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITagSchema
    {
        Uri Identifier { get; }
        string Name { get; }

        ITagSchema Parent { get; }
        IEnumerable<ITagSchema> Children { get; }

        ITagSchema GetChild(string name);
    }
}
