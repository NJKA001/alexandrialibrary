using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;

namespace Gnosis.Tasks
{
    public class SearchTask
        : TaskBase<IMediaItem>, ISearchTask
    {
        public SearchTask(ILogger logger, string pattern, IMediaItemRepository mediaItemRepository)
            : base(logger)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");
            if (mediaItemRepository == null)
                throw new ArgumentNullException("mediaItemRepository");

            this.pattern = pattern;
            this.americanized = pattern.ToAmericanizedString();
            this.mediaItemRepository = mediaItemRepository;
        }

        private const int maxProgress = 100;
        private const SearchFilters allFilters = SearchFilters.Albums | SearchFilters.Artists | SearchFilters.Clips | SearchFilters.Docs | SearchFilters.Feeds | SearchFilters.Pics | SearchFilters.Playlists | SearchFilters.Programs | SearchFilters.Tracks;
        
        private SearchFilters filters = allFilters;
        private string pattern;
        private string americanized;

        private readonly IMediaItemRepository mediaItemRepository;

        private bool IncludeAlbums { get { return (filters & SearchFilters.Albums) == SearchFilters.Albums; } }
        private bool IncludeArtists { get { return (filters & SearchFilters.Artists) == SearchFilters.Artists; } }
        private bool IncludeClips { get { return (filters & SearchFilters.Clips) == SearchFilters.Clips; } }
        private bool IncludeDocs { get { return (filters & SearchFilters.Docs) == SearchFilters.Docs; } }
        private bool IncludeFeeds { get { return (filters & SearchFilters.Feeds) == SearchFilters.Feeds; } }
        private bool IncludePics { get { return (filters & SearchFilters.Pics) == SearchFilters.Pics; } }
        private bool IncludePlaylists { get { return (filters & SearchFilters.Playlists) == SearchFilters.Playlists; } }
        private bool IncludePrograms { get { return (filters & SearchFilters.Programs) == SearchFilters.Programs; } }
        private bool IncludeTracks { get { return (filters & SearchFilters.Tracks) == SearchFilters.Tracks; } }

        private int progressCount = 0;

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
                foreach (var album in mediaItemRepository.GetByCreator<IAlbum>(artist.Location))
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
                foreach (var track in mediaItemRepository.GetByCatalog<ITrack>(album.Location))
                {
                    AddProgress("Track: " + track.Name);
                    UpdateResults(track);
                }
                foreach (var clip in mediaItemRepository.GetByCatalog<IClip>(album.Location))
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
            if (IncludeArtists)
            {
                AddArtistResults(mediaItemRepository.GetByName<IArtist>(pattern));
                AddArtistResults(mediaItemRepository.GetByTag<IArtist>(TagDomain.String, pattern));

                if (!string.IsNullOrEmpty(americanized))
                    AddArtistResults(mediaItemRepository.GetByTag<IArtist>(TagDomain.String, americanized, Algorithm.Americanized));
            }

            if (IncludeAlbums)
            {
                AddAlbumResults(mediaItemRepository.GetByName<IAlbum>(pattern));
                AddAlbumResults(mediaItemRepository.GetByTag<IAlbum>(TagDomain.String, pattern));

                if (!string.IsNullOrEmpty(americanized))
                    AddAlbumResults(mediaItemRepository.GetByTag<IAlbum>(TagDomain.String, americanized, Algorithm.Americanized));
            }

            if (IncludeTracks)
            {
                AddTrackResults(mediaItemRepository.GetByName<ITrack>(pattern));
                AddTrackResults(mediaItemRepository.GetByTag<ITrack>(TagDomain.String, pattern));

                if (!string.IsNullOrEmpty(americanized))
                    AddTrackResults(mediaItemRepository.GetByTag<ITrack>(TagDomain.String, americanized, Algorithm.Americanized));
            }

            if (IncludeClips)
            {
                AddClipResults(mediaItemRepository.GetByName<IClip>(pattern));
                AddClipResults(mediaItemRepository.GetByTag<IClip>(TagDomain.String, pattern));

                if (!string.IsNullOrEmpty(americanized))
                    AddClipResults(mediaItemRepository.GetByTag<IClip>(TagDomain.String, americanized, Algorithm.Americanized));
            }

            UpdateProgress(maxProgress, maxProgress, "Completed");
        }

        public SearchFilters Filters
        {
            get { return filters; }
            set { filters = value; }
        }

        public void SetPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            this.pattern = pattern;
            this.americanized = pattern.ToAmericanizedString();
        }
    }
}
