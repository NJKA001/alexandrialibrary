using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ISpider
    {
        void Crawl(IRepresentationGraph graph);
        void Crawl(IRepresentationGraph graph, Uri location);
    }
}
