using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public interface IRssEnclosure
        : IRssElement
    {
        Uri Url { get; }
        IMediaType Type { get; }
        int Length { get; }
    }
}
