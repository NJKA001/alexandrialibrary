using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Spiders;

namespace Gnosis.Alexandria.Controllers
{
    public class SpiderFactory
    {
        public SpiderFactory(ILogger logger, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository)
        {
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

        public ISpider CreateCatalogSpider()
        {
            return new CatalogSpider(logger, mediaFactory, linkRepository, tagRepository, mediaRepository);
        }
    }
}
