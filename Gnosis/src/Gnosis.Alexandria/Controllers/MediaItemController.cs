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
        public MediaItemController(ILogger logger, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IClip> clipRepository)
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
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");
            if (clipRepository == null)
                throw new ArgumentNullException("clipRepository");

            this.logger = logger;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.clipRepository = clipRepository;
        }

        private readonly ILogger logger;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;

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
                if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(GnosisArtist))
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
                    var updated = new GnosisArtist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    artistRepository.Save(new List<IArtist> { updated });
                }
                else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(GnosisAlbum))
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
                    var updated = new GnosisAlbum(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    albumRepository.Save(new List<IAlbum> { updated });
                }
                else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(GnosisTrack))
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
                    var updated = new GnosisTrack(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    trackRepository.Save(new List<ITrack> { updated });
                }
                else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(GnosisClip))
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
                    var updated = new GnosisClip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
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
                if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(GnosisArtist))
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
                    var updated = new GnosisArtist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    artistRepository.Save(new List<IArtist> { updated });
                }
                else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(GnosisAlbum))
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
                    var updated = new GnosisAlbum(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    albumRepository.Save(new List<IAlbum> { updated });
                }
                else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(GnosisTrack))
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
                    var updated = new GnosisTrack(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    trackRepository.Save(new List<ITrack> { updated });
                }
                else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(GnosisClip))
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
                    var updated = new GnosisClip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
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
