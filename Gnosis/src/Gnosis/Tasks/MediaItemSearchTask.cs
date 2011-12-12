using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;

namespace Gnosis.Tasks
{
    public class MediaItemSearchTask
        : TaskBase<IMediaItem>
    {
        public MediaItemSearchTask(ILogger logger, string pattern, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IClip> clipRepository)
            : base(logger)
        {
            this.pattern = pattern;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.clipRepository = clipRepository;
        }

        private string pattern;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;

        private const int maxProgress = 100;

        private int progressCount = 0;
        //private int errorCount = 0;

        private void AddProgress(string description)
        {
            progressCount = progressCount < maxProgress ?
                progressCount + 1
                : 0;

            UpdateProgress(progressCount, maxProgress, description);
        }

        private void AddArtistResults(IEnumerable<IArtist> artists)
        {
            foreach (var artist in artists)
            {
                AddProgress("Artist: " + artist.Name);
                UpdateResults(artist);
                foreach (var album in albumRepository.GetByCreator(artist.Location))
                {
                    AddProgress("Album: " + album.Name);
                    UpdateResults(album);
                }
            }
        }

        private void AddAlbumResults(IEnumerable<IAlbum> albums)
        {
            foreach (var album in albums)
            {
                AddProgress("Album: " + album.Name);
                UpdateResults(album);
                foreach (var track in trackRepository.GetByCatalog(album.Location))
                {
                    AddProgress("Track: " + track.Name);
                    UpdateResults(track);
                }
                foreach (var clip in clipRepository.GetByCatalog(album.Location))
                {
                    AddProgress("Clip: " + clip.Name);
                    UpdateResults(clip);
                }
            }
        }

        private void AddTrackResults(IEnumerable<ITrack> tracks)
        {
            foreach (var track in tracks)
            {
                AddProgress("Track: " + track.Name);
                UpdateResults(track);
            }
        }

        private void AddClipResults(IEnumerable<IClip> clips)
        {
            foreach (var clip in clips)
            {
                AddProgress("Clip: " + clip.Name);
                UpdateResults(clip);
            }
        }

        protected override void DoWork()
        {
            var americanized = pattern.ToAmericanizedString();

            AddArtistResults(artistRepository.GetByName(pattern));
            AddArtistResults(artistRepository.GetByTag(TagDomain.String, pattern));

            if (!string.IsNullOrEmpty(americanized))
                AddArtistResults(artistRepository.GetByTag(TagDomain.String, americanized, Algorithm.Americanized));

            AddAlbumResults(albumRepository.GetByName(pattern));
            AddAlbumResults(albumRepository.GetByTag(TagDomain.String, pattern));
            
            if (!string.IsNullOrEmpty(americanized))
                AddAlbumResults(albumRepository.GetByTag(TagDomain.String, americanized, Algorithm.Americanized));

            AddTrackResults(trackRepository.GetByName(pattern));
            AddTrackResults(trackRepository.GetByTag(TagDomain.String, pattern));

            if (!string.IsNullOrEmpty(americanized))
                AddTrackResults(trackRepository.GetByTag(TagDomain.String, americanized, Algorithm.Americanized));

            AddClipResults(clipRepository.GetByName(pattern));
            AddClipResults(clipRepository.GetByTag(TagDomain.String, pattern));

            if (!string.IsNullOrEmpty(americanized))
                AddClipResults(clipRepository.GetByTag(TagDomain.String, americanized, Algorithm.Americanized));

            UpdateProgress(maxProgress, maxProgress, "Completed");
        }

        public void SetPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            this.pattern = pattern;
        }
    }
}
