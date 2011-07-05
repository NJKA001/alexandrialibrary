using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public interface IRssFeedFactory
    {
        IRssFeed Create(Uri location);
    }
}
