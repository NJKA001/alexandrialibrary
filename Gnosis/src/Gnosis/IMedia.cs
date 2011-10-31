using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IMedia
    {
        Uri Location { get; }
        IMediaType Type { get; }

        IEnumerable<ILink> GetLinks();
        IEnumerable<ITag> GetTags();
    }
}
