using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Audio;
using Gnosis.Spiders;

namespace Gnosis.Alexandria.Controllers
{
    public class SpiderFactory
    {
        public SpiderFactory(ILogger logger, ISecurityContext securityContext, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IMetadataRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (mediaRepository == null)
                throw new ArgumentNullException("mediaRepository");
            if (mediaItemRepository == null)
                throw new ArgumentNullException("mediaItemRepository");

            this.logger = logger;
            this.securityContext = securityContext;
            this.securityContext = securityContext;
            this.mediaFactory = mediaFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
            this.mediaItemRepository = mediaItemRepository;
            this.audioStreamFactory = audioStreamFactory;
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IMetadataRepository mediaItemRepository;
        private readonly IAudioStreamFactory audioStreamFactory;
        
        public ISpider CreateCatalogSpider()
        {
            return new CatalogSpider(logger, securityContext, mediaFactory, linkRepository, tagRepository, mediaRepository, mediaItemRepository, audioStreamFactory);
        }
    }
}
