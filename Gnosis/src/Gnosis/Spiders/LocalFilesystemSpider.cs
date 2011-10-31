using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Links;
using Gnosis.Tasks;

namespace Gnosis.Spiders
{
    public class LocalFilesystemSpider
        : ISpider
    {
        public LocalFilesystemSpider(ILogger logger, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (mediaRepository == null)
                throw new ArgumentNullException("mediaRepository");

            this.logger = logger;
            this.mediaFactory = mediaFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;

        public IMedia ReadMedia(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return mediaFactory.Create(target);
        }

        public void SaveMedia(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            mediaRepository.Save(new List<IMedia> { media });
        }

        public void SaveLinks(IEnumerable<ILink> links)
        {
            if (links == null)
                throw new ArgumentNullException("links");

            linkRepository.Save(links);
        }

        public void SaveTags(IEnumerable<ITag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException("tags");

            tagRepository.Save(tags);
        }

        public ITask<IEnumerable<IMedia>> Crawl(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (!target.IsFile)
                throw new ArgumentException("target must be a local file path");

            return new MediaCrawlTask(logger, this, target);
        }
    }
}
