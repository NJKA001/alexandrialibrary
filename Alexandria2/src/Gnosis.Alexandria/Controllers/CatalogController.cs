﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Audio;
using Gnosis.Spiders;

namespace Gnosis.Alexandria.Controllers
{
    public class CatalogController
        : ICatalogController
    {
        public CatalogController(ILogger logger, ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaRepository mediaRepository, ILinkRepository linkRepository, ITagRepository tagRepository, IMetadataRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory)
        {
            this.logger = logger;
            spider = new CatalogSpider(logger, securityContext, mediaFactory, linkRepository, tagRepository, mediaRepository, mediaItemRepository, audioStreamFactory);
        }

        private readonly ILogger logger;
        private readonly CatalogSpider spider;

        public ITask<IEnumerable<IMedia>> BuildCatalog(Uri location)
        {
            return spider.Crawl(location);
        }

        public ITask<IEnumerable<IMedia>> BuildCatalog(string path)
        {
            return BuildCatalog(new Uri(path));
        }
    }
}
