using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class Track
        : EntityBase, ITrack
    {
        public Track()
        {
            AddInitializer("Location", x => this.location = x.ToUri());
            AddInitializer("MediaType", x => this.mediaType = x.ToString());
            AddInitializer("Title", x => this.title = x.ToString());
            AddInitializer("TitleSort", x => this.titleSort = x.ToString());
            AddInitializer("Subtitle", x => this.subtitle = x.ToString());
            AddInitializer("Grouping", x => this.grouping = x.ToString());
            AddInitializer("Comment", x => this.comment = x.ToString());
            AddInitializer("Album", x => this.album = x.ToString());
            AddInitializer("AlbumSort", x => this.albumSort = x.ToString());
            AddInitializer("AlbumSubtitle", x => this.albumSubtitle = x.ToString());
            AddInitializer("Artists", x => this.artists = x.ToString());
            AddInitializer("ArtistsSort", x => this.artistsSort = x.ToString());
        }

        /*
        public Track(IContext context, Guid id, DateTime timeStamp, 
            Uri location, string mediaType, string title, string titleSort, string subtitle, string grouping, string comment, 
            string album, string albumSort, string albumSubtitle, string artists, string artistsSort, string albumArtists, 
            string composers, string conductors, string genres, string moods, string languages, DateTime recordingDate, DateTime releaseDate,
            string originalTitle, DateTime originalReleaseDate, uint trackNumber, uint trackCount, uint discNumber, uint discCount,
            TimeSpan duration, uint beatsPerMinute, ulong playCount, TimeSpan playlistDelay, string originalFileName,
            DateTime encodingDate, DateTime taggingDate, string copyright, string publisher, string internationalStandardRecordingCode)
            : base(context, id, timeStamp)
        {
            this.location = location;
            this.mediaType = mediaType;
            this.title = title;
            this.titleSort = titleSort;
            this.subtitle = subtitle;
            this.grouping = grouping;
            this.comment = comment;
            this.album = album;
            this.albumSort = albumSort;
            this.albumSubtitle = albumSubtitle;
            this.artists = artists;
            this.artistsSort = artistsSort;
            this.albumArtists = albumArtists;
            this.composers = composers;
            this.conductors = conductors;
            this.genres = genres;
            this.moods = moods;
            this.languages = languages;
            this.recordingDate = recordingDate;
            this.releaseDate = releaseDate;
            this.originalTitle = originalTitle;
            this.originalReleaseDate = originalReleaseDate;
            this.trackNumber = trackNumber;
            this.trackCount = trackCount;
            this.discNumber = discNumber;
            this.discCount = discCount;
            this.duration = duration;
            this.beatsPerMinute = beatsPerMinute;
            this.playCount = playCount;
            this.playlistDelay = playlistDelay;
            this.originalFileName = originalFileName;
            this.encodingDate = encodingDate;
            this.taggingDate = taggingDate;
            this.copyright = copyright;
            this.publisher = publisher;
            this.internationalStandardRecordingCode = internationalStandardRecordingCode;

            this.pictures = new ObservableCollection<ITrackPicture>();
            this.lyrics = new ObservableCollection<ITrackUnsynchronizedLyrics>();
            this.synchronizedLyrics = new ObservableCollection<ITrackSynchronizedLyrics>();
            this.identifiers = new ObservableCollection<ITrackIdentifier>();
            this.ratings = new ObservableCollection<ITrackRating>();
            this.links = new ObservableCollection<ITrackLink>();
            this.metadata = new ObservableCollection<ITrackMetadata>();

            this.titleHashCodes = new ObservableCollection<IHashCode>();
            this.albumHashCodes = new ObservableCollection<IHashCode>();
            this.artistHashCodes = new ObservableCollection<IHashCode>();
            this.albumArtistHashCodes = new ObservableCollection<IHashCode>();
            this.composerHashCodes = new ObservableCollection<IHashCode>();
            this.conductorHashCodes = new ObservableCollection<IHashCode>();
            this.originalTitleHashCodes = new ObservableCollection<IHashCode>();
        }
        */

        private Uri location;
        private string mediaType = "audio/unknown";
        private string title = "Untitled";
        private string titleSort = "Untitled";
        private string subtitle = string.Empty;
        private string grouping = string.Empty;
        private string comment = string.Empty;
        private string album = "Unknown Album";
        private string albumSort = "Unknown Album";
        private string albumSubtitle = string.Empty;
        private string artists = "Unknown Artist";
        private string artistsSort = "Unknown Artist";
        private string albumArtists = string.Empty;
        private string composers = string.Empty;
        private string conductors = string.Empty;
        private string genres = string.Empty;
        private string moods = string.Empty;
        private string languages = string.Empty;
        private DateTime recordingDate = DateTime.MinValue;
        private DateTime releaseDate = DateTime.MinValue;
        private string originalTitle = string.Empty;
        private DateTime originalReleaseDate = DateTime.MinValue;
        private uint trackNumber = 0;
        private uint trackCount = 0;
        private uint discNumber = 0;
        private uint discCount = 0;
        private TimeSpan duration = TimeSpan.Zero;
        private uint beatsPerMinute = 0;
        private ulong playCount = 0;
        private TimeSpan playlistDelay = TimeSpan.Zero;
        private string originalFileName = string.Empty;
        private DateTime encodingDate = DateTime.MinValue;
        private DateTime taggingDate = DateTime.MinValue;
        private string copyright = string.Empty;
        private string publisher = string.Empty;
        private string internationalStandardRecordingCode = string.Empty;

        private readonly ObservableCollection<ITrackPicture> pictures = new ObservableCollection<ITrackPicture>();
        private readonly ObservableCollection<ITrackUnsynchronizedLyrics> lyrics = new ObservableCollection<ITrackUnsynchronizedLyrics>();
        private readonly ObservableCollection<ITrackSynchronizedLyrics> synchronizedLyrics = new ObservableCollection<ITrackSynchronizedLyrics>();
        private readonly ObservableCollection<ITrackIdentifier> identifiers = new ObservableCollection<ITrackIdentifier>();
        private readonly ObservableCollection<ITrackRating> ratings = new ObservableCollection<ITrackRating>();
        private readonly ObservableCollection<ITrackLink> links = new ObservableCollection<ITrackLink>();
        private readonly ObservableCollection<ITrackMetadata> metadata = new ObservableCollection<ITrackMetadata>();

        private readonly ObservableCollection<IHashCode> titleHashCodes = new ObservableCollection<IHashCode>();
        private readonly ObservableCollection<IHashCode> albumHashCodes = new ObservableCollection<IHashCode>();
        private readonly ObservableCollection<IHashCode> artistHashCodes = new ObservableCollection<IHashCode>();
        private readonly ObservableCollection<IHashCode> albumArtistHashCodes = new ObservableCollection<IHashCode>();
        private readonly ObservableCollection<IHashCode> composerHashCodes = new ObservableCollection<IHashCode>();
        private readonly ObservableCollection<IHashCode> conductorHashCodes = new ObservableCollection<IHashCode>();
        private readonly ObservableCollection<IHashCode> originalTitleHashCodes = new ObservableCollection<IHashCode>();

        #region ITrack Members

        public Uri Location
        {
            get { return location; }
            set
            {
                if (value != null && value != location)
                {
                    Change(() => location = value, "Location");
                }
            }
        }

        public string MediaType
        {
            get { return mediaType; }
            set
            {
                if (value != null && value != mediaType)
                {
                    Change(() => mediaType = value, "MediaType");
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value != null && value != title)
                {
                    Change(() => title = value, "Title");
                }
            }
        }

        public string TitleSort
        {
            get { return titleSort; }
            set
            {
                if (value != null && value != titleSort)
                {
                    Change(() => titleSort = value, "TitleSort");
                }
            }
        }

        public string Subtitle
        {
            get { return subtitle; }
            set
            {
                if (value != null && value != subtitle)
                {
                    Change(() => subtitle = value, "Subtitle");
                }
            }
        }

        public string Grouping
        {
            get { return grouping; }
            set
            {
                if (value != null && value != grouping)
                {
                    Change(() => grouping = value, "Grouping");
                }
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                if (value != null && value != comment)
                {
                    Change(() => comment = value, "Comment");
                }
            }
        }

        public string Album
        {
            get { return album; }
            set
            {
                if (value != null && value != album)
                {
                    Change(() => album = value, "Album");
                }
            }
        }

        public string AlbumSort
        {
            get { return albumSort; }
            set
            {
                if (value != null && value != albumSort)
                {
                    Change(() => albumSort = value, "AlbumSort");
                }
            }
        }

        public string AlbumSubtitle
        {
            get { return albumSubtitle; }
            set
            {
                if (value != null && value != albumSubtitle)
                {
                    Change(() => albumSubtitle = value, "AlbumSubtitle");
                }
            }
        }

        public string Artists
        {
            get { return artists; }
            set
            {
                if (value != null && value != artists)
                {
                    Change(() => artists = value, "Artists");
                }
            }
        }

        public string ArtistsSort
        {
            get { return artistsSort; }
            set
            {
                if (value != null && value != artistsSort)
                {
                    Change(() => artistsSort = value, "ArtistsSort");
                }
            }
        }

        public string AlbumArtists
        {
            get { return albumArtists; }
            set
            {
                if (value != null && value != albumArtists)
                {
                    Change(() => albumArtists = value, "AlbumArtists");
                }
            }
        }

        public string Composers
        {
            get { return composers; }
            set
            {
                if (value != null && value != composers)
                {
                    Change(() => composers = value, "Composers");
                }
            }
        }

        public string Conductors
        {
            get { return conductors; }
            set
            {
                if (value != null && value != conductors)
                {
                    Change(() => conductors = value, "Conductors");
                }
            }
        }

        public string Genres
        {
            get { return genres; }
            set
            {
                if (value != null && value != genres)
                {
                    Change(() => genres = value, "Genres");
                }
            }
        }

        public string Moods
        {
            get { return moods; }
            set
            {
                if (value != null && value != moods)
                {
                    Change(() => moods = value, "Moods");
                }
            }
        }

        public string Languages
        {
            get { return languages; }
            set
            {
                if (value != null && value != languages)
                {
                    Change(() => languages = value, "Languages");
                }
            }
        }

        public DateTime RecordingDate
        {
            get { return recordingDate; }
            set
            {
                if (value != recordingDate)
                {
                    Change(() => recordingDate = value, "RecordingDate");
                }
            }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                if (value != releaseDate)
                {
                    Change(() => releaseDate = value, "ReleaseDate");
                }
            }
        }

        public string OriginalTitle
        {
            get { return originalFileName; }
            set
            {
                if (value != null && value != originalTitle)
                {
                    Change(() => originalTitle = value, "OriginalTitle");
                }
            }
        }

        public DateTime OriginalReleaseDate
        {
            get { return originalReleaseDate; }
            set
            {
                if (value != originalReleaseDate)
                {
                    Change(() => originalReleaseDate = value, "OriginalReleaseDate");
                }
            }
        }

        public uint TrackNumber
        {
            get { return trackNumber; }
            set
            {
                if (value != trackNumber)
                {
                    Change(() => trackNumber = value, "TrackNumber");
                }
            }
        }

        public uint TrackCount
        {
            get { return trackCount; }
            set
            {
                if (value != trackCount)
                {
                    Change(() => trackCount = value, "TrackCount");
                }
            }
        }

        public uint DiscNumber
        {
            get { return discNumber; }
            set
            {
                if (value != discNumber)
                {
                    Change(() => discNumber = value, "DiscNumber");
                }
            }
        }

        public uint DiscCount
        {
            get { return discCount; }
            set
            {
                if (value != discCount)
                {
                    Change(() => discCount = value, "DiscCount");
                }
            }
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                if (value != duration)
                {
                    Change(() => duration = value, "Duration");
                }
            }
        }

        public uint BeatsPerMinute
        {
            get { return beatsPerMinute; }
            set
            {
                if (value != beatsPerMinute)
                {
                    Change(() => beatsPerMinute = value, "BeatsPerMinute");
                }
            }
        }

        public ulong PlayCount
        {
            get { return playCount; }
            set
            {
                if (value != playCount)
                {
                    Change(() => playCount = value, "PlayCount");
                }
            }
        }

        public TimeSpan PlaylistDelay
        {
            get { return playlistDelay; }
            set
            {
                if (value != playlistDelay)
                {
                    Change(() => playlistDelay = value, "PlaylistDelay");
                }
            }
        }

        public string OriginalFileName
        {
            get { return originalFileName; }
            set
            {
                if (value != null && value != originalFileName)
                {
                    Change(() => originalFileName = value, "OriginalFileName");
                }
            }
        }

        public DateTime EncodingDate
        {
            get { return encodingDate; }
            set
            {
                if (value != encodingDate)
                {
                    Change(() => encodingDate = value, "EncodingDate");
                }
            }
        }

        public DateTime TaggingDate
        {
            get { return taggingDate; }
            set
            {
                if (value != taggingDate)
                {
                    Change(() => taggingDate = value, "TaggingDate");
                }
            }
        }

        public string Copyright
        {
            get { return copyright; }
            set
            {
                if (value != null && value != copyright)
                {
                    Change(() => copyright = value, "Copyright");
                }
            }
        }

        public string Publisher
        {
            get { return publisher; }
            set
            {
                if (value != null && value != publisher)
                {
                    Change(() => publisher = value, "Publisher");
                }
            }
        }

        public string InternationalStandardRecordingCode
        {
            get { return internationalStandardRecordingCode; }
            set
            {
                if (value != null && value != internationalStandardRecordingCode)
                {
                    Change(() => internationalStandardRecordingCode = value, "InternationalStandardRecordingCode");
                }
            }
        }

        public IEnumerable<ITrackPicture> Pictures
        {
            get { return pictures; }
        }

        public IEnumerable<ITrackUnsynchronizedLyrics> Lyrics
        {
            get { return lyrics; }
        }

        public IEnumerable<ITrackSynchronizedLyrics> SynchronizedLyrics
        {
            get { return synchronizedLyrics; }
        }

        public IEnumerable<ITrackIdentifier> Identifiers
        {
            get { return identifiers; }
        }

        public IEnumerable<ITrackRating> Ratings
        {
            get { return ratings; }
        }

        public IEnumerable<ITrackLink> Links
        {
            get { return links; }
        }

        public IEnumerable<ITrackMetadata> Metadata
        {
            get { return metadata; }
        }

        public IEnumerable<IHashCode> TitleHashCodes
        {
            get { return titleHashCodes; }
        }

        public IEnumerable<IHashCode> AlbumHashCodes
        {
            get { return albumHashCodes; }
        }

        public IEnumerable<IHashCode> ArtistHashCodes
        {
            get { return artistHashCodes; }
        }

        public IEnumerable<IHashCode> AlbumArtistHashCodes
        {
            get { return albumArtistHashCodes; }
        }

        public IEnumerable<IHashCode> ComposerHashCodes
        {
            get { return composerHashCodes; }
        }

        public IEnumerable<IHashCode> ConductorHashCodes
        {
            get { return conductorHashCodes; }
        }

        public IEnumerable<IHashCode> OriginalTitleHashCodes
        {
            get { return originalTitleHashCodes; }
        }

        #endregion
    }
}
