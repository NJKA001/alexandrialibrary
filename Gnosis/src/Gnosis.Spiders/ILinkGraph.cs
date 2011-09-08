using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Spiders
{
    public interface ILinkGraph
    {
        Uri Source { get; }
        string Name { get; }
        string Rel { get; }
        string Rev { get; }
        IEnumerable<ILinkGraph> Children { get; }

        void AddChild(ILinkGraph child);
    }
}
