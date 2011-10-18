using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMediaSummary
    {
        Uri Location { get; }
        IMediaType Type { get; }
        IEnumerable<ILink> Links { get; }
        IEnumerable<IEnumerable<ITag>> Tags { get; }
    }
}
