using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ILinkSchema
    {
        Uri Identifier { get; }
        string Name { get; }

        ILinkSchema Parent { get; }
        IEnumerable<ILinkSchema> Children { get; }

        ILinkSchema GetChild(string name);
    }
}
