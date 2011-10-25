using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ITag
    {
        long Id { get; }
        Uri Target { get; }
        ITagType Type { get; }
        object Value { get; }
        ITagTuple Tuple { get; }
    }
}
