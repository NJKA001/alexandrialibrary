using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Spiders;

namespace Gnosis.Alexandria.Controllers
{
    public class SpiderFactory
    {
        public SpiderFactory(ILogger logger, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository)
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
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");

            this.logger = logger;
            this.mediaFactory = mediaFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

        public ISpider CreateCatalogSpider()
        {
            return new CatalogSpider(logger, mediaFactory, linkRepository, tagRepository, mediaRepository, artistRepository, albumRepository, trackRepository);
        }
    }
}
