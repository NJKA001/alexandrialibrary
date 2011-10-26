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
        public LocalFilesystemSpider(ILinkReader linkReader, IMediaFactory mediaFactory)
        {
            if (linkReader == null)
                throw new ArgumentNullException("linkReader");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");

            this.linkReader = linkReader;
            this.mediaFactory = mediaFactory;
        }

        private readonly ILinkReader linkReader;
        private readonly IMediaFactory mediaFactory;

        public ITask<IEnumerable<IMedia>> Crawl(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (!target.IsFile)
                throw new ArgumentException("target must be a local file path");
            if (!Directory.Exists(target.LocalPath))
                throw new ArgumentException("target does not exist as a local directory");

            var links = linkReader.Read(target);
            //var task = new Task<IEnumerable<IMedia>>
            return null;
        }
    }
}
