using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class MediaItemBuilder<T>
        : IMediaItemBuilder<T>
        where T : class, IMediaItem
    {
        public MediaItemBuilder(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory)
            : this(securityContext, mediaTypeFactory, null)
        {
        }

        public MediaItemBuilder(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, T item)
        {
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");

            this.securityContext = securityContext;
            this.mediaTypeFactory = mediaTypeFactory;

            if (item != null)
            {
                this.identityInfo = new IdentityInfo(item.Location, item.Type, item.Name, item.Summary, item.FromDate, item.ToDate, item.Number);
                this.sizeInfo = new SizeInfo(item.Duration, item.Height, item.Width);
                this.creatorInfo = new CreatorInfo(item.Creator, item.CreatorName);
                this.catalogInfo = new CatalogInfo(item.Catalog, item.CatalogName);
                this.targetInfo = new TargetInfo(item.Target, item.TargetType);
                this.userInfo = new UserInfo(item.User, item.UserName);
                this.thumbnailInfo = new ThumbnailInfo(item.Thumbnail, item.ThumbnailData);
            }
            else
            {
                this.identityInfo = IdentityInfo.GetDefault(GetMediaType());
                this.targetInfo = TargetInfo.GetDefault(mediaTypeFactory);
                this.userInfo = securityContext.CurrentUserInfo;
            }

            InitializeCreateFunction();
        }

        private readonly ISecurityContext securityContext;
        private readonly IMediaTypeFactory mediaTypeFactory;

        private IdentityInfo identityInfo;
        private SizeInfo sizeInfo = SizeInfo.Default;
        private CreatorInfo creatorInfo = CreatorInfo.Default;
        private CatalogInfo catalogInfo = CatalogInfo.Default;
        private TargetInfo targetInfo;
        private UserInfo userInfo;
        private ThumbnailInfo thumbnailInfo = ThumbnailInfo.Default;
        private Func<IdentityInfo, SizeInfo, CreatorInfo, CatalogInfo, TargetInfo, UserInfo, ThumbnailInfo, IMediaItem> createFunction;

        private IMediaType GetMediaType()
        {
            if (typeof(T) == typeof(IAlbum))
                return mediaTypeFactory.GetByCode("vnd.gnosis.album");
            else if (typeof(T) == typeof(IArtist))
                return mediaTypeFactory.GetByCode("vnd.gnosis.artist");
            else if (typeof(T) == typeof(IClip))
                return mediaTypeFactory.GetByCode("vnd.gnosis.clip");
            else if (typeof(T) == typeof(IDoc))
                return mediaTypeFactory.GetByCode("vnd.gnosis.doc");
            else if (typeof(T) == typeof(IFeed))
                return mediaTypeFactory.GetByCode("vnd.gnosis.feed");
            else if (typeof(T) == typeof(IFeedItem))
                return mediaTypeFactory.GetByCode("vnd.gnosis.feed.item");
            else if (typeof(T) == typeof(IPic))
                return mediaTypeFactory.GetByCode("vnd.gnosis.pic");
            else if (typeof(T) == typeof(IPlaylist))
                return mediaTypeFactory.GetByCode("vnd.gnosis.playlist");
            else if (typeof(T) == typeof(IPlaylistItem))
                return mediaTypeFactory.GetByCode("vnd.gnosis.playlist.item");
            else if (typeof(T) == typeof(IProgram))
                return mediaTypeFactory.GetByCode("vnd.gnosis.program");
            else if (typeof(T) == typeof(ITrack))
                return mediaTypeFactory.GetByCode("vnd.gnosis.track");
            else
                return mediaTypeFactory.Default;
        }

        private void InitializeCreateFunction()
        {
            if (typeof(T) == typeof(IAlbum))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Album(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IArtist))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Artist(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IClip))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Clip(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IDoc))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Doc(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IFeed))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Feed(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IFeedItem))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new FeedItem(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IPic))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Pic(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IPlaylist))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Playlist(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IPlaylistItem))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new PlaylistItem(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(IProgram))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Program(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
            else if (typeof(T) == typeof(ITrack))
                createFunction = (identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX) => new Track(identityInfoX, sizeInfoX, creatorInfoX, catalogInfoX, targetInfoX, userInfoX, thumbnailInfoX);
        }

        public IMediaItemBuilder<T> Identity(string name, string summary)
        {
            identityInfo = new IdentityInfo(Guid.NewGuid().ToUrn(), GetMediaType(), name, summary, DateTime.MinValue, DateTime.MaxValue, 0);
            return this;
        }

        public IMediaItemBuilder<T> Identity(string name, string summary, DateTime fromDate, DateTime toDate, uint number)
        {
            identityInfo = new IdentityInfo(Guid.NewGuid().ToUrn(), GetMediaType(), name, summary, fromDate, toDate, number);
            return this;
        }

        public IMediaItemBuilder<T> Identity(string name, string summary, DateTime fromDate, DateTime toDate, uint number, Uri location)
        {
            identityInfo = new IdentityInfo(location, GetMediaType(), name, summary, fromDate, toDate, number);
            return this;
        }

        public IMediaItemBuilder<T> Size(TimeSpan duration)
        {
            sizeInfo = new SizeInfo(duration, 0, 0);
            return this;
        }

        public IMediaItemBuilder<T> Size(TimeSpan duration, uint height, uint width)
        {
            sizeInfo = new SizeInfo(duration, height, width);
            return this;
        }

        public IMediaItemBuilder<T> Size(uint height, uint width)
        {
            sizeInfo = new SizeInfo(TimeSpan.Zero, height, width);
            return this;
        }

        public IMediaItemBuilder<T> Creator(Uri creator, string creatorName)
        {
            creatorInfo = new CreatorInfo(creator, creatorName);
            return this;
        }

        public IMediaItemBuilder<T> Catalog(Uri catalog, string catalogName)
        {
            catalogInfo = new CatalogInfo(catalog, catalogName);
            return this;
        }

        public IMediaItemBuilder<T> Target(Uri target, IMediaType targetType)
        {
            targetInfo = new TargetInfo(target, targetType);
            return this;
        }

        public IMediaItemBuilder<T> User(Uri user, string userName)
        {
            userInfo = new UserInfo(user, userName);
            return this;
        }

        public IMediaItemBuilder<T> Thumbnail(Uri thumbnail, byte[] thumbnailData)
        {
            thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);
            return this;
        }

        public T GetDefault()
        {
            return (T)createFunction(IdentityInfo.GetDefault(GetMediaType()), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.GetDefault(mediaTypeFactory), UserInfo.Default, ThumbnailInfo.Default);
        }

        public T ToMediaItem()
        {
            return (T)createFunction(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }
    }
}
