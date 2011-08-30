using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IResourceIdentifier
    {
        IResourceScheme Scheme { get; }
        string HierarchicalPart { get; }
        IResourceQuery Query { get; }
        string Fragment { get; }

        bool IsValid { get; }
    }
}
