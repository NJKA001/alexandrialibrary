using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public interface IRssEnclosure
    {
        Uri Path { get; }
        IMediaType MediaType { get; }
        int Length { get; }
    }
}
