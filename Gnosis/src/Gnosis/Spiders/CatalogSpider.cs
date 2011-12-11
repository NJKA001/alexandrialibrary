using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Audio;
using Gnosis.Links;
using Gnosis.Tasks;

namespace Gnosis.Spiders
{
    public class CatalogSpider
        : ISpider
    {
        public CatalogSpider(ILogger logger, ISecurityContext securityContext, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IClip> clipRepository, IAudioStreamFactory audioStreamFactory)
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
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");
            if (clipRepository == null)
                throw new ArgumentNullException("clipRepository");
            if (audioStreamFactory == null)
                throw new ArgumentNullException("audioStreamFactory");

            this.logger = logger;
            this.securityContext = securityContext;
            this.mediaFactory = mediaFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.clipRepository = clipRepository;
            this.audioStreamFactory = audioStreamFactory;

            Delay = TimeSpan.Zero;
            MaxErrors = 0;
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;
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

            return mediaFactory.Create(location);
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
                var artist = audio.GetArtist(securityContext, trackRepository, artistRepository);
                artistRepository.Save(new List<IArtist> { artist });

                var album = audio.GetAlbum(securityContext, trackRepository, albumRepository, artist);
                albumRepository.Save(new List<IAlbum> { album });

                var track = audio.GetTrack(securityContext, trackRepository, audioStreamFactory, artist, album);
                trackRepository.Save(new List<ITrack> { track });

                var trackDate = track.FromDate > DateTime.MinValue ? track.FromDate : track.ToDate;
                if (album.FromDate == DateTime.MinValue && trackDate != DateTime.MinValue)
                {
                    album = new GnosisAlbum(album.Name, album.Summary, trackDate, album.Number, album.Creator, album.CreatorName, album.Catalog, album.CatalogName, album.Target, album.TargetType, album.User, album.UserName, album.Thumbnail, album.ThumbnailData, album.Location);
                    albumRepository.Save(new List<IAlbum> { album });
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

                        var updated = new GnosisAlbum(album.Name, album.Summary, album.FromDate, number, album.Creator, album.CreatorName, album.Catalog, album.CatalogName, album.Target, album.TargetType, album.User, album.UserName, track.Thumbnail, track.ThumbnailData, album.Location);
                        albumRepository.Save(new List<IAlbum> { updated });
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
                var artist = video.GetArtist(securityContext, clipRepository, artistRepository);
                artistRepository.Save(new List<IArtist> { artist });

                var album = video.GetAlbum(securityContext, clipRepository, albumRepository, artist);
                albumRepository.Save(new List<IAlbum> { album });

                var clip = video.GetClip(securityContext, clipRepository, artist, album);
                clipRepository.Save(new List<IClip> { clip });

                var clipDate = clip.FromDate > DateTime.MinValue ? clip.FromDate : clip.ToDate;
                if (album.FromDate == DateTime.MinValue && clipDate != DateTime.MinValue)
                {
                    album = new GnosisAlbum(album.Name, string.Empty, clipDate, album.Number, album.Creator, album.CreatorName, album.Catalog, album.CatalogName, album.Target, album.TargetType, album.User, album.UserName, album.Thumbnail, album.ThumbnailData, album.Location);
                    albumRepository.Save(new List<IAlbum> { album });
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
                    thumbnails.Add(new Link(related.Location, media.Location, MediaType.ApplicationGnosisAlbumThumbnail.ToString(), fileInfo.Name));

                    if (artistThumbnailPath != null)
                    {
                        thumbnails.Add(new Link(related.Location, new Uri(artistThumbnailPath), MediaType.ApplicationGnosisArtistThumbnail.ToString(), fileInfo.Name));
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

            return new CatalogMediaTask(logger, this, target, Delay, MaxErrors);
        }
    }
}
