using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Spiders
{
    public class LocalFilesystemSpider
        : ISpider
    {
        public LocalFilesystemSpider(IMediaFactory mediaFactory)
        {
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");

            this.mediaFactory = mediaFactory;
        }

        private readonly IMediaFactory mediaFactory;

        public ITask<IEnumerable<IMedia>> Crawl(Uri target, int depth)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (!target.IsFile)
                throw new ArgumentException("target must be a local file path");
            if (!Directory.Exists(target.LocalPath))
                throw new ArgumentException("target does not exist as a local directory");

            //var task = new Task<IEnumerable<IMedia>>
            return null;
        }
    }
}
