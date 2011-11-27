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

        private readonly string pattern;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

        protected override void DoWork()
        {
            foreach (var artist in artistRepository.GetByName(pattern))
            {
                UpdateResults(artist);
                foreach (var album in albumRepository.GetByCreator(artist.Location))
                    UpdateResults(album);
            }

            foreach (var album in albumRepository.GetByName(pattern))
            {
                UpdateResults(album);
            }

            foreach (var track in trackRepository.GetByName(pattern))
            {
                UpdateResults(track);
            }
        }
    }
}
