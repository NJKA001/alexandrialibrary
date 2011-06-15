using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Gnosis.Core;
using Gnosis.Alexandria.Models;

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
            AddChildInitializer("TrackPicture", child => AddPicture(child as ITrackPicture));
            AddChildInitializer("TrackUnsynchronizedLyrics", child => AddLyrics(child as ITrackUnsynchronizedLyrics));
            AddChildInitializer("TrackSynchronizedLyrics", child => AddSynchronizedLyrics(child as ITrackSynchronizedLyrics));
            AddChildInitializer("TrackRating", child => AddRating(child as ITrackRating));
            AddValueInitializer("Track_Identifiers", value => AddIdentifier(value as ITrackIdentifier));
            AddValueInitializer("Track_Links", value => AddLink(value as ITrackLink));
            AddValueInitializer("Track_Metadata", value => AddMetadatum(value as ITrackMetadatum));
            AddValueInitializer("Track_TitleHashCodes", value => AddTitleHashCode(value as IHashCode));
            AddValueInitializer("Track_AlbumHashCodes", value => AddAlbumHashCode(value as IHashCode));
            AddValueInitializer("Track_ArtistHashCodes", value => AddArtistHashCode(value as IHashCode));
            AddValueInitializer("Track_AlbumArtistHashCodes", value => AddAlbumArtistHashCode(value as IHashCode));
            AddValueInitializer("Track_ComposerHashCodes", value => AddComposerHashCode(value as IHashCode));
            AddValueInitializer("Track_ConductorHashCodes", value => AddConductorHashCode(value as IHashCode));
            AddValueInitializer("Track_OriginalTitleHashCodes", value => AddOriginalTitleHashCode(value as IHashCode));
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

        #region Private Methods

        private void AddPicture(ITrackPicture picture)
        {
            AddChild<ITrack, ITrackPicture>(() => pictures.Add(picture), picture, x => x.Pictures);
        }

        private void AddLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            AddChild<ITrack, ITrackUnsynchronizedLyrics>(() => this.lyrics.Add(lyrics), lyrics, x => x.Lyrics);
        }

        private void AddSynchronizedLyrics(ITrackSynchronizedLyrics synchronizedLyrics)
        {
            AddChild<ITrack, ITrackSynchronizedLyrics>(() => this.synchronizedLyrics.Add(synchronizedLyrics), synchronizedLyrics, x => x.Lyrics);
        }

        private void AddRating(ITrackRating rating)
        {
            AddChild<ITrack, ITrackRating>(() => this.ratings.Add(rating), rating, x => x.Ratings);
        }

        private void AddIdentifier(ITrackIdentifier identifier)
        {
            AddValue<ITrack, ITrackIdentifier>(() => this.identifiers.Add(identifier), identifier, x => x.Identifiers);
        }

        private void AddLink(ITrackLink link)
        {
            AddValue<ITrack, ITrackLink>(() => this.links.Add(link), link, x => x.Links);
        }

        private void AddMetadatum(ITrackMetadatum metadatum)
        {
            AddValue<ITrack, ITrackMetadatum>(() => this.metadata.Add(metadatum), metadatum, x => x.Metadata);
        }

        private void AddTitleHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.titleHashCodes.Add(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void RemoveTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.titleHashCodes.Remove(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void AddAlbumHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.albumHashCodes.Add(hashCode), hashCode, x => x.AlbumHashCodes);
        }

        private void RemoveAlbumHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.albumHashCodes.Remove(hashCode), hashCode, x => x.AlbumHashCodes);
        }

        private void AddArtistHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.artistHashCodes.Add(hashCode), hashCode, x => x.ArtistHashCodes);
        }

        private void RemoveArtistHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.albumHashCodes.Remove(hashCode), hashCode, x => x.ArtistHashCodes);
        }

        private void AddAlbumArtistHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.albumArtistHashCodes.Add(hashCode), hashCode, x => x.AlbumArtistHashCodes);
        }

        private void RemoveAlbumArtistHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.albumArtistHashCodes.Remove(hashCode), hashCode, x => x.AlbumArtistHashCodes);
        }

        private void AddComposerHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.composerHashCodes.Add(hashCode), hashCode, x => x.ComposerHashCodes);
        }

        private void RemoveComposerHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.composerHashCodes.Remove(hashCode), hashCode, x => x.ComposerHashCodes);
        }

        private void AddConductorHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.conductorHashCodes.Add(hashCode), hashCode, x => x.ConductorHashCodes);
        }

        private void RemoveConductorHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.conductorHashCodes.Remove(hashCode), hashCode, x => x.ConductorHashCodes);
        }

        private void AddOriginalTitleHashCode(IHashCode hashCode)
        {
            AddValue<ITrack, IHashCode>(() => this.originalTitleHashCodes.Add(hashCode), hashCode, x => x.OriginalTitleHashCodes);
        }

        private void RemoveOriginalTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<ITrack, IHashCode>(() => this.originalTitleHashCodes.Remove(hashCode), hashCode, x => x.OriginalTitleHashCodes);
        }

        #endregion

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


        public void AddPicture(string textEncoding, string mediaType, TrackPictureType pictureType, string description, byte[] data)
        {
            var picture = new TrackPicture();
            picture.Initialize(new EntityInitialState(Context, Logger, this.Id));
            picture.TextEncoding = textEncoding;
            picture.MediaType = mediaType;
            picture.PictureType = pictureType;
            picture.Description = description;
            picture.Data = data;
            AddPicture(picture);
        }

        public void RemovePicture(ITrackPicture picture)
        {
            RemoveChild<ITrack, ITrackPicture>(() => pictures.Remove(picture), picture, x => x.Pictures);
        }


        public void AddLyrics(string textEncoding, string language, string description, string lyrics)
        {
            var lyricsItem = new TrackUnsynchronizedLyrics();
            lyricsItem.Initialize(new EntityInitialState(Context, Logger, this.Id));
            lyricsItem.TextEncoding = textEncoding;
            lyricsItem.Language = language;
            lyricsItem.Description = description;
            lyricsItem.Lyrics = lyrics;
            AddLyrics(lyricsItem);
        }

        public void RemoveLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            RemoveChild<ITrack, ITrackUnsynchronizedLyrics>(() => this.lyrics.Remove(lyrics), lyrics, x => x.Lyrics);
        }


        public void AddSynchronizedLyrics(string textEncoding, string language, string description, string lyrics, TrackSynchronizedTextType contentType)
        {
            var lyricsItem = new TrackSynchronizedLyrics();
            lyricsItem.Initialize(new EntityInitialState(Context, Logger, this.Id));
            lyricsItem.TextEncoding = textEncoding;
            lyricsItem.Language = language;
            lyricsItem.Description = description;
            lyricsItem.Lyrics = lyrics;
            lyricsItem.ContentType = contentType;
            AddSynchronizedLyrics(lyricsItem);
        }

        public void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics synchronizedLyrics)
        {
            RemoveChild<ITrack, ITrackSynchronizedLyrics>(() => this.synchronizedLyrics.Remove(synchronizedLyrics), synchronizedLyrics, x => x.SynchronizedLyrics);
        }


        public void AddRating(byte rating, Uri user, ulong playCount)
        {
            var ratingItem = new TrackRating();
            ratingItem.Initialize(new EntityInitialState(Context, Logger, this.Id));
            ratingItem.Rating = rating;
            ratingItem.User = user;
            ratingItem.PlayCount = playCount;
            AddRating(ratingItem);
        }

        public void RemoveRating(ITrackRating rating)
        {
            RemoveChild<ITrack, ITrackRating>(() => this.ratings.Remove(rating), rating, x => x.Ratings);
        }


        public void AddIdentifier(Uri scheme, string identifier)
        {
            AddIdentifier(new TrackIdentifier(this.Id, scheme, identifier));
        }

        public void RemoveIdentifier(ITrackIdentifier identifier)
        {
            RemoveValue<ITrack, ITrackIdentifier>(() => this.identifiers.Remove(identifier), identifier, x => x.Identifiers);
        }


        public void AddLink(string textEncoding, string relationship, Uri location)
        {
            AddLink(new TrackLink(this.Id, textEncoding, relationship, location));
        }

        public void RemoveLink(ITrackLink link)
        {
            RemoveValue<ITrack, ITrackLink>(() => this.links.Remove(link), link, x => x.Links);
        }


        public void AddMetadatum(string textEncoding, string description, string content)
        {
            AddMetadatum(new TrackMetadatum(this.Id, textEncoding, description, content));
        }

        public void RemoveMetadatum(ITrackMetadatum metadatum)
        {
            RemoveValue<ITrack, ITrackMetadatum>(() => this.metadata.Remove(metadatum), metadatum, x => x.Metadata);
        }

        #endregion
    }
}
