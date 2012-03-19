using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Alexandria.Controllers
{
    public class MediaItemController
        : IMetadataController
    {
        public MediaItemController(ILogger logger, ISecurityContext securityContext, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMetadataRepository mediaItemRepository)
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
            if (mediaItemRepository == null)
                throw new ArgumentNullException("mediaItemRepository");            

            this.logger = logger;
            this.securityContext = securityContext;
            this.mediaFactory = mediaFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaItemRepository = mediaItemRepository;
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMetadataRepository mediaItemRepository;

        public void UpdateThumbnail<T>(Uri id, Uri thumbnail, byte[] thumbnailData)
            where T : class, IMetadata
        {
            if (id == null)
                throw new ArgumentNullException("id");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");

            try
            {
                var item = mediaItemRepository.GetByLocation<T>(id);
                if (item == null)
                    return;

                var builder = new MediaItemBuilder<T>(securityContext, mediaFactory, item)
                    .Thumbnail(thumbnail, thumbnailData);

                mediaItemRepository.Save<T>(new List<T> { builder.ToMediaItem() });
                

                //if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(Artist))
                //{
                //    var item = artistRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                //    var updated = new Artist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    artistRepository.Save(new List<IArtist> { updated });
                //}
                //else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(Album))
                //{
                //    var item = albumRepository.GetByLocation(id);
                //    if (item == null)
                //        return;
                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                //    var updated = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    albumRepository.Save(new List<IAlbum> { updated });
                //}
                //else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(Track))
                //{
                //    var item = trackRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                //    var updated = new Track(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    trackRepository.Save(new List<ITrack> { updated });
                //}
                //else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(Clip))
                //{
                //    var item = clipRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
                //    var updated = new Clip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    clipRepository.Save(new List<IClip> { updated });
                //}
            }
            catch (Exception ex)
            {
                logger.Error("  UpdateTrackThumbnail", ex);
                throw;
            }
        }

        public void UpdateSummary<T>(Uri id, string summary)
            where T : class, IMetadata
        {
            if (id == null)
                throw new ArgumentNullException("id");
            if (summary == null)
                throw new ArgumentNullException("summary");

            try
            {
                var item = mediaItemRepository.GetByLocation<T>(id);
                if (item == null)
                    return;

                var builder = new MediaItemBuilder<T>(securityContext, mediaFactory, item)
                    .Identity(item.Name, summary, item.FromDate, item.ToDate, item.Number, item.Location);

                mediaItemRepository.Save<T>(new List<T> { builder.ToMediaItem() });

                //if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(Artist))
                //{
                //    var item = artistRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                //    var updated = new Artist(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    artistRepository.Save(new List<IArtist> { updated });
                //}
                //else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(Album))
                //{
                //    var item = albumRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                //    var updated = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    albumRepository.Save(new List<IAlbum> { updated });
                //}
                //else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(Track))
                //{
                //    var item = trackRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                //    var updated = new Track(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    trackRepository.Save(new List<ITrack> { updated });
                //}
                //else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(Clip))
                //{
                //    var item = clipRepository.GetByLocation(id);
                //    if (item == null)
                //        return;

                //    var identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, summary, item.FromDate, item.ToDate, item.Number);
                //    var sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                //    var creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                //    var catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                //    var targetInfo = new TargetInfo(item.Target, item.TargetType);
                //    var userInfo = new UserInfo(item.User, item.UserName);
                //    var thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
                //    var updated = new Clip(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                //    clipRepository.Save(new List<IClip> { updated });
                //}
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
            return mediaItemRepository.GetByLocation<IAlbum>(album);
        }

        public IEnumerable<ITrack> GetTracks(Uri album)
        {
            return mediaItemRepository.GetByCatalog<ITrack>(album);
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
