using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IArtist
        : IApplication
    {
        string Name { get; }
        DateTime ActiveFrom { get; }
        DateTime ActiveTo { get; }

        Uri Target { get; }
        IMediaType TargetType { get; }
        Uri Thumbnail { get; }
    }
}
