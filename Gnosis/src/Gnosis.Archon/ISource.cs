using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Archon
{
    public interface ISource
    {
        Guid Id { get; }
        ISource Parent { get; set; }
        string Name { get; set; }
        IEnumerable<ISource> Children { get; }
        void AddChild(ISource child);

        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
    }
}
