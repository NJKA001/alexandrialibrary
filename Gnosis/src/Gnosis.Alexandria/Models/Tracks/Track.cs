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
            AddInitializer("AlbumArtists", x => this.albumArtists = x.ToString());
            AddInitializer("Composers", x => this.composers = x.ToString());
            AddInitializer("Conductors", x => this.conductors = x.ToString());
            AddInitializer("Genres", x => this.genres = x.ToString());
            AddInitializer("Moods", x => this.moods = x.ToString());
            AddInitializer("Languages", x => this.languages = x.ToString());
            AddInitializer("RecordingDate", x => this.recordingDate = x.ToDateTime());
            AddInitializer("ReleaseDate", x => this.releaseDate = x.ToDateTime());
            AddInitializer("OriginalTitle", x => this.originalTitle = x.ToString());
            AddInitializer("OriginalReleaseDate", x => this.originalReleaseDate = x.ToDateTime());
            AddInitializer("TrackNumber", x => this.trackNumber = x.ToUInt32());
            AddInitializer("TrackCount", x => this.trackCount = x.ToUInt32());
            AddInitializer("DiscNumber", x => this.discNumber = x.ToUInt32());
            AddInitializer("DiscCount", x => this.discCount = x.ToUInt32());
            AddInitializer("Duration", x => this.duration = x.ToTimeSpan());
            AddInitializer("BeatsPerMinute", x => this.beatsPerMinute = x.ToUInt32());
            AddInitializer("PlayCount", x => this.playCount = x.ToUInt64());
            AddInitializer("PlaylistDelay", x => this.playlistDelay = x.ToTimeSpan());
            AddInitializer("OriginalFileName", x => this.originalFileName = x.ToString());
            AddInitializer("EncodingDate", x => this.encodingDate = x.ToDateTime());
            AddInitializer("TaggingDate", x => this.taggingDate = x.ToDateTime());
            AddInitializer("Copyright", x => this.copyright = x.ToString());
            AddInitializer("Publisher", x => this.publisher = x.ToString());
            AddInitializer("InternationalStandardRecordingCode", x => this.internationalStandardRecordingCode = x.ToString());
        }

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
        private readonly ObservableCollection<ITrackMetadatum> metadata = new ObservableCollection<ITrackMetadatum>();

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

        public void AddPicture(ITrackPicture picture)
        {
            AddChild(() => pictures.Add(picture), picture, "Pictures");
        }

        public void RemovePicture(ITrackPicture picture)
        {
            RemoveChild(() => pictures.Remove(picture), picture.Id, "Pictures");
        }


        public IEnumerable<ITrackUnsynchronizedLyrics> Lyrics
        {
            get { return lyrics; }
        }

        public void AddLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            AddChild(() => this.lyrics.Add(lyrics), lyrics, "Lyrics");
        }

        public void RemoveLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            RemoveChild(() => this.lyrics.Remove(lyrics), lyrics.Id, "Lyrics");
        }


        public IEnumerable<ITrackSynchronizedLyrics> SynchronizedLyrics
        {
            get { return synchronizedLyrics; }
        }

        public void AddSynchronizedLyrics(ITrackSynchronizedLyrics synchronizedLyrics)
        {
            AddChild(() => this.synchronizedLyrics.Add(synchronizedLyrics), synchronizedLyrics, "SynchronizedLyrics");
        }

        public void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics synchronizedLyrics)
        {
            RemoveChild(() => this.synchronizedLyrics.Remove(synchronizedLyrics), synchronizedLyrics.Id, "SynchronizedLyrics");
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

        public IEnumerable<ITrackMetadatum> Metadata
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
