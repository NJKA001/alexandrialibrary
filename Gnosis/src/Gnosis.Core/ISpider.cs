using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISpider
    {
        void Crawl(ILinkGraph graph, Uri location);
    }
}
