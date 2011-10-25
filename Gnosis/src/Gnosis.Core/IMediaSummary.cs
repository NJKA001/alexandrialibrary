using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Image;

namespace Gnosis.Core
{
    public interface IMediaSummary
    {
        IEnumerable<ITag> Tags { get; }
        IImage Thumbnail { get; }
    }
}
