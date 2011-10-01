using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISchema
    {
        Uri Identifier { get; }
        string Name { get; }

        ISchema Parent { get; }
        IEnumerable<ISchema> Children { get; }

        ISchema GetChild(string name);
    }
}
