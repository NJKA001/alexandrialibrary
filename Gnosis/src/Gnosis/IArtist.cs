using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IArtist
    {
        Guid Id { get; }
        string Name { get; }
        DateTime ActiveFrom { get; }
        DateTime ActiveTo { get; }
        IImage Thumbnail { get; }
    }
}
