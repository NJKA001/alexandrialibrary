using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public class WebScrapeSource : SourceBase
    {
        public WebScrapeSource()
            : this(Guid.NewGuid())
        {
        }

        public WebScrapeSource(Guid id)
            : base(id)
        {
            ImagePath = "pack://application:,,,/Images/feed.png";
            AddChild(new ProxySource(Guid.Empty) { Parent = this });
        }
    }
}