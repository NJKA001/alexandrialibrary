using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Alexandria.Controllers
{
    public class MediaItemController
        : IMediaItemController
    {
        public MediaItemController(ILogger logger, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IDoc> docRepository, IMediaItemRepository<IFeed> feedRepository, IMediaItemRepository<IFeedItem> feedItemRepository, IMediaItemRepository<IPic> picRepository, IMediaItemRepository<IPlaylist> playlistRepository, IMediaItemRepository<IPlaylistItem> playlistItemRepository, IMediaItemRepository<IProgram> programRepository, IMediaItemRepository<ITrack> trackRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (clipRepository == null)
                throw new ArgumentNullException("clipRepository");
            if (docRepository == null)
                throw new ArgumentNullException("docRepository");
            if (feedRepository == null)
                throw new ArgumentNullException("feedRepository");
            if (feedItemRepository == null)
                throw new ArgumentNullException("feedItemRepository");
            if (picRepository == null)
                throw new ArgumentNullException("picRepository");
            if (playlistRepository == null)
                throw new ArgumentNullException("playlistRepository");
            if (playlistItemRepository == null)
                throw new ArgumentNullException("playlistItemRepository");
            if (programRepository == null)
                throw new ArgumentNullException("programRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");
            

            this.logger = logger;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.clipRepository = clipRepository;
            this.docRepository = docRepository;
            this.feedRepository = feedRepository;
            this.feedItemRepository = feedItemRepository;
            this.picRepository = picRepository;
            this.playlistRepository = playlistRepository;
            this.playlistItemRepository = playlistItemRepository;
            this.programRepository = programRepository;
            this.trackRepository = trackRepository;
        }

        private readonly ILogger logger;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;
        private readonly IMediaItemRepository<IDoc> docRepository;
        private readonly IMediaItemRepository<IFeed> feedRepository;
        private readonly IMediaItemRepository<IFeedItem> feedItemRepository;
        private readonly IMediaItemRepository<IPic> picRepository;
        private readonly IMediaItemRepository<IPlaylist> playlistRepository;
        private readonly IMediaItemRepository<IPlaylistItem> playlistItemRepository;
        private readonly IMediaItemRepository<IProgram> programRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

        public void UpdateThumbnail<T>(Uri id, Uri thumbnail, byte[] thumbnailData)
            where T : IMediaItem
        {
            if (id == null)
                throw new ArgumentNullException("id");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");

            try
            {
                if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(Artist))
                {
                    var item = artistRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                    var updated = new Artist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    artistRepository.Save(new List<IArtist> { updated });
                }
                else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(Album))
                {
                    var item = albumRepository.GetByLocation(id);
                    if (item == null)
                        return;
                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                    var updated = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    albumRepository.Save(new List<IAlbum> { updated });
                }
                else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(Track))
                {
                    var item = trackRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                    var updated = new Track(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    trackRepository.Save(new List<ITrack> { updated });
                }
                else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(Clip))
                {
                    var item = clipRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                    var updated = new Clip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    clipRepository.Save(new List<IClip> { updated });
                }
            }
            catch (Exception ex)
            {
                logger.Error("  UpdateTrackThumbnail", ex);
                throw;
            }
        }

        public void UpdateSummary<T>(Uri id, string summary)
            where T : IMediaItem
        {
            if (id == null)
                throw new ArgumentNullException("id");
            if (summary == null)
                throw new ArgumentNullException("summary");

            try
            {
                if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(Artist))
                {
                    var item = artistRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                    var updated = new Artist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    artistRepository.Save(new List<IArtist> { updated });
                }
                else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(Album))
                {
                    var item = albumRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                    var updated = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    albumRepository.Save(new List<IAlbum> { updated });
                }
                else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(Track))
                {
                    var item = trackRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                    var updated = new Track(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    trackRepository.Save(new List<ITrack> { updated });
                }
                else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(Clip))
                {
                    var item = clipRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                    var userInfo = new UserInfo(item.User, item.UserName);
                    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                    var updated = new Clip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    clipRepository.Save(new List<IClip> { updated });
                }
            }
            catch (Exception ex)
            {
                logger.Error("  MediaItemController.UpdateSummary", ex);
                throw;
            }
        }

        public IEnumerable<ILink> GetLinksBySource(Uri source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return linkRepository.GetBySource(source);
        }

        public IEnumerable<ILink> GetLinksByTarget(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return linkRepository.GetByTarget(target);
        }
        
        public IEnumerable<ITag> GetTags(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            return tagRepository.GetByTarget(target);
        }

        public IAlbum GetAlbum(Uri album)
        {
            return albumRepository.GetByLocation(album);
        }

        public IEnumerable<ITrack> GetTracks(Uri album)
        {
            return trackRepository.GetByCatalog(album);
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
    }
}
