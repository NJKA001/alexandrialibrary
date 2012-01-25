using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Audio;
using Gnosis.Links;
using Gnosis.Metadata;
using Gnosis.Tasks;

namespace Gnosis.Spiders
{
    public class CatalogSpider
        : ISpider
    {
        public CatalogSpider(ILogger logger, ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaTypeFactory mediaTypeFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (contentTypeFactory == null)
                throw new ArgumentNullException("contentTypeFactory");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (mediaRepository == null)
                throw new ArgumentNullException("mediaRepository");
            if (mediaItemRepository == null)
                throw new ArgumentNullException("mediaItemRepository");
            if (audioStreamFactory == null)
                throw new ArgumentNullException("audioStreamFactory");

            this.logger = logger;
            this.securityContext = securityContext;
            this.contentTypeFactory = contentTypeFactory;
            this.mediaTypeFactory = mediaTypeFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
            this.mediaItemRepository = mediaItemRepository;
            this.audioStreamFactory = audioStreamFactory;

            Delay = TimeSpan.Zero;
            MaxErrors = 0;
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IContentTypeFactory contentTypeFactory;
        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IMediaItemRepository mediaItemRepository;
        private readonly IAudioStreamFactory audioStreamFactory;

        public TimeSpan Delay
        {
            get;
            set;
        }

        public int MaxErrors
        {
            get;
            set;
        }

        public IMedia GetMedia(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            var type = mediaTypeFactory.GetByLocation(location, contentTypeFactory);

            if (type == null)
                System.Diagnostics.Debug.WriteLine("  CatalogSpider.GetMedia: type is null for location=" + location.LocalPath);
            else
                System.Diagnostics.Debug.WriteLine("  CatalogSpider.GetMedia: location=" + location.LocalPath + " type=" + type.ToString());

            return type != null ?
                type.CreateMedia(location)
                : null;
        }

        private bool HasDefaultThumbnail(IMediaItem item)
        {
            return (item.Thumbnail.IsEmptyUrn() || item.Thumbnail.ToString().EndsWith(".jpg.to/")) && item.ThumbnailData.Length == 0;
        }

        private string GetArtistThumbnailPath(FileInfo fileInfo)
        {
            //TODO: Go back a directory with fileInfo and look for folder.jpg for the artist
            if (fileInfo.Directory.Parent != null)
            {
                var artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "folder.jpg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;

                artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "folder.jpeg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;

                artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "Folder.jpg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;

                artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "Folder.jpeg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;
            }

            return null;
        }

        private void SaveMediaItems(IAudio audio)
        {
            try
            {
                var artist = audio.GetArtist(securityContext, mediaTypeFactory, mediaItemRepository);
                mediaItemRepository.Save(new List<IArtist> { artist });
                tagRepository.Save(artist.GetTags());

                var album = audio.GetAlbum(securityContext, mediaTypeFactory, mediaItemRepository, artist);
                mediaItemRepository.Save(new List<IAlbum> { album });
                tagRepository.Save(album.GetTags());

                var track = audio.GetTrack(securityContext, mediaTypeFactory, mediaItemRepository, audioStreamFactory, artist, album);
                mediaItemRepository.Save(new List<ITrack> { track });
                tagRepository.Save(track.GetTags());

                var trackDate = track.FromDate > DateTime.MinValue ? track.FromDate : track.ToDate;
                if (album.FromDate == DateTime.MinValue && trackDate != DateTime.MinValue)
                {
                    var identityInfo = new IdentityInfo(album.Location, album.Type, album.Name, album.Summary, trackDate, trackDate, album.Number);
                    var sizeInfo = new SizeInfo(album.Duration, album.Height, album.Width);
                    var creatorInfo = new CreatorInfo(album.Creator, album.CreatorName);
                    var catalogInfo = new CatalogInfo(album.Catalog, album.CatalogName);
                    var targetInfo = new TargetInfo(album.Target, album.TargetType);
                    var userInfo = new UserInfo(album.User, album.UserName);
                    var thumbnailInfo = new ThumbnailInfo(album.Thumbnail, album.ThumbnailData);
                    album = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    mediaItemRepository.Save(new List<IAlbum> { album });
                }

                //if (album.ToDate == DateTime.MinValue && track.ToDate != DateTime.

                if (!HasDefaultThumbnail(track))
                {
                    if (HasDefaultThumbnail(album))
                    {
                        //var fromDate = album.FromDate;
                        //if (album.FromDate == DateTime.MinValue || album.FromDate == DateTime.MaxValue)
                        //{
                        //    fromDate = track.ToDate != DateTime.MinValue && track.ToDate != DateTime.MaxValue ?
                        //        track.ToDate
                        //        : track.FromDate;
                        //}

                        var number = album.Number;
                        if (album.Name.Contains("#") && !album.Name.EndsWith("#"))
                        {
                            var suffix = album.Name.Substring(album.Name.LastIndexOf('#') + 1).Trim();
                            uint.TryParse(suffix, out number);
                        }
                        else if (album.Name.Contains("(") && !album.Name.EndsWith("("))
                        {
                            var suffix = album.Name.Substring(album.Name.LastIndexOf("(") + 1);
                            var cleaned = System.Text.RegularExpressions.Regex.Replace(suffix, "[^0-9]", string.Empty);
                            uint.TryParse(cleaned, out number);
                        }

                        var identityInfo = new IdentityInfo(album.Location, album.Type, album.Name, album.Summary, album.FromDate, album.ToDate, number);
                        var sizeInfo = new SizeInfo(album.Duration, album.Height, album.Width);
                        var creatorInfo = new CreatorInfo(album.Creator, album.CreatorName);
                        var catalogInfo = new CatalogInfo(album.Catalog, album.CatalogName);
                        var targetInfo = new TargetInfo(album.Target, album.TargetType);
                        var userInfo = new UserInfo(album.User, album.UserName);
                        var thumbnailInfo = new ThumbnailInfo(track.Thumbnail, track.ThumbnailData);
                        var updated = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                        mediaItemRepository.Save(new List<IAlbum> { updated });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("  SaveMediaItems", ex);
            }
        }

        private void SaveMediaItems(IVideo video)
        {
            try
            {
                var artist = video.GetArtist(securityContext, mediaTypeFactory, mediaItemRepository);
                mediaItemRepository.Save(new List<IArtist> { artist });
                tagRepository.Save(artist.GetTags());

                var album = video.GetAlbum(securityContext, mediaTypeFactory, mediaItemRepository, artist);
                mediaItemRepository.Save(new List<IAlbum> { album });
                tagRepository.Save(album.GetTags());

                var clip = video.GetClip(securityContext, mediaTypeFactory, mediaItemRepository, artist, album);
                mediaItemRepository.Save(new List<IClip> { clip });
                tagRepository.Save(clip.GetTags());

                var clipDate = clip.FromDate > DateTime.MinValue ? clip.FromDate : clip.ToDate;
                if (album.FromDate == DateTime.MinValue && clipDate != DateTime.MinValue)
                {
                    var identityInfo = new IdentityInfo(album.Location, album.Type, album.Name, album.Summary, clipDate, clipDate, album.Number);
                    var sizeInfo = new SizeInfo(album.Duration, album.Height, album.Width);
                    var creatorInfo = new CreatorInfo(album.Creator, album.CreatorName);
                    var catalogInfo = new CatalogInfo(album.Catalog, album.CatalogName);
                    var targetInfo = new TargetInfo(album.Target, album.TargetType);
                    var userInfo = new UserInfo(album.User, album.UserName);
                    var thumbnailInfo = new ThumbnailInfo(album.Thumbnail, album.ThumbnailData);
                    album = new Album(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
                    mediaItemRepository.Save(new List<IAlbum> { album });
                }
            }
            catch (Exception ex)
            {
                logger.Error("  CatalogSpider.SaveMediaItems", ex);
            }
        }

        public void HandleMedia(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            mediaRepository.Save(new List<IMedia> { media });

            if (media is IAudio)
            {
                SaveMediaItems(media as IAudio);
            }
            else if (media is IVideo)
            {
                SaveMediaItems(media as IVideo);
            }

            var location = media.Location;
            if (location.IsFile && (location.ToString().ToLower().EndsWith("folder.jpg") || location.ToString().EndsWith("folder.jpeg")))
            {
                var fileInfo = new System.IO.FileInfo(location.LocalPath);
                var pattern = new Uri(fileInfo.DirectoryName).ToString() + "%";

                var artistThumbnailPath = GetArtistThumbnailPath(fileInfo);

                var thumbnails = new List<ILink>();
                foreach (var related in mediaRepository.ByLocation(pattern))
                {
                    thumbnails.Add(new Link(related.Location, media.Location, "album-thumbnail", fileInfo.Name));

                    if (artistThumbnailPath != null)
                    {
                        thumbnails.Add(new Link(related.Location, new Uri(artistThumbnailPath), "artist-thumbnail", fileInfo.Name));
                    }
                }

                System.Diagnostics.Debug.WriteLine("Saving thumbnail links: " + thumbnails.Count());
                linkRepository.Save(thumbnails);
            }
        }

        public void HandleLinks(IEnumerable<ILink> links)
        {
            if (links == null)
                throw new ArgumentNullException("links");

            linkRepository.Save(links);
        }

        public void HandleTags(IEnumerable<ITag> tags)
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

            return new CatalogMediaTask(logger, mediaTypeFactory, this, target, Delay, MaxErrors);
        }
    }
}
