using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssFeedItem
    {
        string Title { get; set; }
        Uri Link { get; set; }
        string Description { get; set; }
        string AuthorName { get; set; }
        Uri AuthorEmail { get; set; }
        Uri Comments { get; set; }
        IRssEnclosure Enclosure { get; set; }
        string Guid { get; set; }
        DateTime PublishedDate { get; set; }
        IRssSource Source { get; set; }

        IEnumerable<IRssCategory> Categories { get; }
    }
}
