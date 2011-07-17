using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Rss
{
    public interface IRssItem
        : IXmlElement
    {
        string Title { get; }
        Uri Link { get; }
        string Description { get; }
        string Author { get; }
        Uri Comments { get; }
        IRssEnclosure Enclosure { get; }
        IRssGuid Guid { get; }
        DateTime PubDate { get; }
        IRssSource Source { get; }

        IEnumerable<IRssCategory> Categories { get; }
    }
}
