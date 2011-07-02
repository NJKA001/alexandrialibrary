using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssItem
    {
        string Title { get; set; }
        Uri Link { get; set; }
        string Description { get; set; }
        string Author { get; set; }
        Uri Comments { get; set; }
        IRssEnclosure Enclosure { get; set; }
        IRssGuid Guid { get; set; }
        DateTime PubDate { get; set; }
        IRssSource Source { get; set; }

        IEnumerable<IRssCategory> Categories { get; }
    }
}
