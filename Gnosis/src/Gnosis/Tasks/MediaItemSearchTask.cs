using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    public class MediaItemSearchTask
        : TaskBase<IMediaItem>
    {
        public MediaItemSearchTask(ILogger logger, string pattern, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository)
            : base(logger)
        {
            this.pattern = pattern;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
        }

        private string pattern;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

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

        protected override void DoWork()
        {
            foreach (var artist in artistRepository.GetByName(pattern))
            {
                AddProgress("Artist: " + artist.Name);
                UpdateResults(artist);
                foreach (var album in albumRepository.GetByCreator(artist.Location))
                {
                    AddProgress("Album: " + album.Name);
                    UpdateResults(album);
                }
            }

            foreach (var album in albumRepository.GetByName(pattern))
            {
                AddProgress("Album: " + album.Name);
                UpdateResults(album);
                foreach (var track in trackRepository.GetByCatalog(album.Location))
                {
                    AddProgress("Track: " + track.Name);
                    UpdateResults(track);
                }
            }

            foreach (var track in trackRepository.GetByName(pattern))
            {
                AddProgress("Track: " + track.Name);
                UpdateResults(track);
            }

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
