using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    public class MediaCrawlTask
        : TaskBase<IEnumerable<IMedia>>
    {
        public MediaCrawlTask(ILogger logger, ISpider spider)
            : base(logger)
        {
        }

        protected override void DoWork()
        {
        }
    }
}
