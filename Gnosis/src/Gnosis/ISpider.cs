using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface ISpider
    {
        IMedia ReadMedia(Uri target);

        void SaveMedia(IMedia media);
        void SaveLinks(IEnumerable<ILink> links);
        void SaveTags(IEnumerable<ITag> tags);
        
        ITask<IEnumerable<IMedia>> Crawl(Uri target);
    }
}
