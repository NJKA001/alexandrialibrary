using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Links;
using Gnosis.Tags.Id3.Id3v2;
using Gnosis.Tasks;

namespace Gnosis.Spiders
{
    public class CatalogSpider
        : ISpider
    {
        public CatalogSpider(ILogger logger, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IArtistRepository artistRepository, IAlbumRepository albumRepository, ITrackRepository trackRepository)
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

            Delay = TimeSpan.Zero;
            MaxErrors = 0;
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IArtistRepository artistRepository;
        private readonly IAlbumRepository albumRepository;
        private readonly ITrackRepository trackRepository;

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

        private void SaveTrack(IAudio audio)
        {
            var track = trackRepository.GetByAudioLocation(audio.Location);
            if (track == null)
            {
                IArtist artist = null;
                IAlbum album = null;

                var tags = audio.GetTags();
                var titleTag = tags.Where(x => x.Type == Id3v2TagType.Title).FirstOrDefault();
                var artistTag = tags.Where(x => x.Type == Id3v2TagType.Artist).FirstOrDefault();
                var albumTag = tags.Where(x => x.Type == Id3v2TagType.Album).FirstOrDefault();
                
                if (artistTag != null)
                {
                    var artistName = artistTag.Tuple.ToString();
                    artist = artistRepository.GetByName(artistName).FirstOrDefault();
                    if (artist == null)
                    {
                        artist = new GnosisArtist(artistName, DateTime.MinValue, DateTime.MaxValue, null);
                        artistRepository.Save(new List<IArtist> { artist });
                    }
                }

                if (albumTag != null)
                {
                    var albumTitle = albumTag.Tuple.ToString();
                    //album = albumRepository.GetByTitle(albumTitle
                }
            }
        }

        public void HandleMedia(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            mediaRepository.Save(new List<IMedia> { media });

            if (media is IAudio)
            {
                SaveTrack(media as IAudio);
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
                    thumbnails.Add(new Link(related.Location, media.Location, LinkType.AlbumThumbnail, fileInfo.Name));

                    if (artistThumbnailPath != null)
                    {
                        thumbnails.Add(new Link(related.Location, new Uri(artistThumbnailPath), LinkType.ArtistThumbnail, fileInfo.Name));
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
