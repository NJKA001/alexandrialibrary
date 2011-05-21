using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Collections
{
    public enum CollectionItemState
    {
        None = 0,
        Added,
        Existing,
        Moved,
        Removed
    }
}
