using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ISpider
    {
        TimeSpan Delay { get; set; }
        uint MaxErrors { get; set; }

        IMedia GetMedia(Uri location);

        void HandleMedia(IMedia media);
        void HandleLinks(IEnumerable<ILink> links);
        void HandleTags(IEnumerable<ITag> tags);
        
        ITask<IEnumerable<IMedia>> Crawl(Uri target);
    }
}
