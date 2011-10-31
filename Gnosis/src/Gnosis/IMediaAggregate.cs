using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMediaAggregate
    {
        IMedia Media { get; }
        IEnumerable<ILink> Links { get; }
        IEnumerable<ITag> Tags { get; }
    }
}
