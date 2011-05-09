using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class Track
        : EntityBase, ITrack
    {
        public Track(IContext context, Uri location)
            : base(context)
        {
            this.location = location;

            this.pictures = new OrderedSet<ITrackPicture>(context);
            this.lyrics = new OrderedSet<ITrackUnsynchronizedLyrics>(context);
            this.synchronizedLyrics = new OrderedSet<ITrackSynchronizedLyrics>(context);
            this.identifiers = new OrderedSet<ITrackIdentifier>(context);
            this.ratings = new OrderedSet<ITrackRating>(context);
            this.links = new OrderedSet<ITrackLink>(context);
            this.metadata = new OrderedSet<ITrackMetadata>(context);
        }

        public Track(IContext context, Guid id, ITimeStamp timeStamp, 
            Uri location, string mediaType, string title, string titleSort, string subtitle, string grouping, string comment, 
            string album, string albumSort, string albumSubtitle, string artists, string artistsSort, string albumArtists, 
            string composers, string conductors, string genres, string moods, string languages, DateTime recordingDate, DateTime releaseDate,
            string originalTitle, DateTime originalReleaseDate, uint trackNumber, uint trackCount, uint discNumber, uint discCount,
            TimeSpan duration, uint beatsPerMinute, ulong playCount, TimeSpan playlistDelay, string originalFileName,
            DateTime encodingDate, DateTime taggingDate, string copyright, string publisher, string internationalStandardRecordingCode,
            IEnumerable<ITrackPicture> pictures, IEnumerable<ITrackUnsynchronizedLyrics> lyrics, 
            IEnumerable<ITrackSynchronizedLyrics> synchronizedLyrics, IEnumerable<ITrackIdentifier> identifiers,
            IEnumerable<ITrackRating> ratings, IEnumerable<ITrackLink> links, IEnumerable<ITrackMetadata> metadata,
            IEnumerable<IHashCode> titleHashCodes, IEnumerable<IHashCode> albumHashCodes,
            IEnumerable<IHashCode> artistHashCodes, IEnumerable<IHashCode> albumArtistHashCodes,
            IEnumerable<IHashCode> composerHashCodes, IEnumerable<IHashCode> conductorHashCodes,
            IEnumerable<IHashCode> originalTitleHashCodes)
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

            this.pictures = new OrderedSet<ITrackPicture>(context, pictures);
            this.lyrics = new OrderedSet<ITrackUnsynchronizedLyrics>(context, lyrics);
            this.synchronizedLyrics = new OrderedSet<ITrackSynchronizedLyrics>(context, synchronizedLyrics);
            this.identifiers = new OrderedSet<ITrackIdentifier>(context, identifiers);
            this.ratings = new OrderedSet<ITrackRating>(context, ratings);
            this.links = new OrderedSet<ITrackLink>(context, links);
            this.metadata = new OrderedSet<ITrackMetadata>(context, metadata);

            this.titleHashCodes = new Set<IHashCode>(context, titleHashCodes);
            this.albumHashCodes = new Set<IHashCode>(context, albumHashCodes);
            this.artistHashCodes = new Set<IHashCode>(context, artistHashCodes);
            this.albumArtistHashCodes = new Set<IHashCode>(context, albumArtistHashCodes);
            this.composerHashCodes = new Set<IHashCode>(context, composerHashCodes);
            this.conductorHashCodes = new Set<IHashCode>(context, conductorHashCodes);
            this.originalTitleHashCodes = new Set<IHashCode>(context, originalTitleHashCodes);
        }

        private readonly Uri location;
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
        
        private readonly IOrderedSet<ITrackPicture> pictures;
        private readonly IOrderedSet<ITrackUnsynchronizedLyrics> lyrics;
        private readonly IOrderedSet<ITrackSynchronizedLyrics> synchronizedLyrics;
        private readonly IOrderedSet<ITrackIdentifier> identifiers;
        private readonly IOrderedSet<ITrackRating> ratings;
        private readonly IOrderedSet<ITrackLink> links;
        private readonly IOrderedSet<ITrackMetadata> metadata;

        private readonly Gnosis.Core.Collections.ISet<IHashCode> titleHashCodes;
        private readonly Gnosis.Core.Collections.ISet<IHashCode> albumHashCodes;
        private readonly Gnosis.Core.Collections.ISet<IHashCode> artistHashCodes;
        private readonly Gnosis.Core.Collections.ISet<IHashCode> albumArtistHashCodes;
        private readonly Gnosis.Core.Collections.ISet<IHashCode> composerHashCodes;
        private readonly Gnosis.Core.Collections.ISet<IHashCode> conductorHashCodes;
        private readonly Gnosis.Core.Collections.ISet<IHashCode> originalTitleHashCodes;

        #region ITrack Members

        public Uri Location
        {
            get { return location; }
        }

        public string MediaType
        {
            get { return mediaType; }
            set
            {
                if (value != null && value != mediaType)
                {
                    OnEntityChanged(() => mediaType = value, "MediaType");
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
                    OnEntityChanged(() => title = value, "Title");
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
                    OnEntityChanged(() => titleSort = value, "TitleSort");
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
                    OnEntityChanged(() => subtitle = value, "Subtitle");
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
                    OnEntityChanged(() => grouping = value, "Grouping");
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
                    OnEntityChanged(() => comment = value, "Comment");
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
                    OnEntityChanged(() => album = value, "Album");
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
                    OnEntityChanged(() => albumSort = value, "AlbumSort");
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
                    OnEntityChanged(() => albumSubtitle = value, "AlbumSubtitle");
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
                    OnEntityChanged(() => artists = value, "Artists");
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
                    OnEntityChanged(() => artistsSort = value, "ArtistsSort");
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
                    OnEntityChanged(() => albumArtists = value, "AlbumArtists");
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
                    OnEntityChanged(() => composers = value, "Composers");
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
                    OnEntityChanged(() => conductors = value, "Conductors");
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
                    OnEntityChanged(() => genres = value, "Genres");
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
                    OnEntityChanged(() => moods = value, "Moods");
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
                    OnEntityChanged(() => languages = value, "Languages");
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
                    OnEntityChanged(() => recordingDate = value, "RecordingDate");
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
                    OnEntityChanged(() => releaseDate = value, "ReleaseDate");
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
                    OnEntityChanged(() => originalTitle = value, "OriginalTitle");
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
                    OnEntityChanged(() => originalReleaseDate = value, "OriginalReleaseDate");
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
                    OnEntityChanged(() => trackNumber = value, "TrackNumber");
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
                    OnEntityChanged(() => trackCount = value, "TrackCount");
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
                    OnEntityChanged(() => discNumber = value, "DiscNumber");
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
                    OnEntityChanged(() => discCount = value, "DiscCount");
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
                    OnEntityChanged(() => duration = value, "Duration");
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
                    OnEntityChanged(() => beatsPerMinute = value, "BeatsPerMinute");
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
                    OnEntityChanged(() => playCount = value, "PlayCount");
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
                    OnEntityChanged(() => playlistDelay = value, "PlaylistDelay");
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
                    OnEntityChanged(() => originalFileName = value, "OriginalFileName");
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
                    OnEntityChanged(() => encodingDate = value, "EncodingDate");
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
                    OnEntityChanged(() => taggingDate = value, "TaggingDate");
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
                    OnEntityChanged(() => copyright = value, "Copyright");
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
                    OnEntityChanged(() => publisher = value, "Publisher");
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
                    OnEntityChanged(() => internationalStandardRecordingCode = value, "InternationalStandardRecordingCode");
                }
            }
        }

        public IOrderedSet<ITrackPicture> Pictures
        {
            get { return pictures; }
        }

        public IOrderedSet<ITrackUnsynchronizedLyrics> Lyrics
        {
            get { return lyrics; }
        }

        public IOrderedSet<ITrackSynchronizedLyrics> SynchronizedLyrics
        {
            get { return synchronizedLyrics; }
        }

        public IOrderedSet<ITrackIdentifier> Identifiers
        {
            get { return identifiers; }
        }

        public IOrderedSet<ITrackRating> Ratings
        {
            get { return ratings; }
        }

        public IOrderedSet<ITrackLink> Links
        {
            get { return links; }
        }

        public IOrderedSet<ITrackMetadata> Metadata
        {
            get { return metadata; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> TitleHashCodes
        {
            get { return titleHashCodes; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> AlbumHashCodes
        {
            get { return albumHashCodes; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> ArtistHashCodes
        {
            get { return artistHashCodes; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> AlbumArtistHashCodes
        {
            get { return albumArtistHashCodes; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> ComposerHashCodes
        {
            get { return composerHashCodes; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> ConductorHashCodes
        {
            get { return conductorHashCodes; }
        }

        public Gnosis.Core.Collections.ISet<IHashCode> OriginalTitleHashCodes
        {
            get { return originalTitleHashCodes; }
        }

        #endregion
    }
}
