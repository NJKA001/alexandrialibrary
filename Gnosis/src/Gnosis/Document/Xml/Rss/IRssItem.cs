using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Rss
{
    public interface IRssItem
        : IRssElement
    {
        string Title { get; }
        Uri Link { get; }
        string Description { get; }
        string Author { get; }
        Uri CommentsLink { get; }
        IRssEnclosure Enclosure { get; }
        IRssGuid Guid { get; }
        DateTime PubDate { get; }
        IRssSource Source { get; }

        IEnumerable<IRssCategory> Categories { get; }
    }
}
