using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using TagLib;

using Gnosis.Core;
using Gnosis.Core.Iso;
using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Models.Tracks
{
    public class Track
        : EntityBase<ITrack>, ITrack
    {
        public Track()
        {
            AddInitializer(value => this.location = value.ToUri(), track => track.Location);
            AddInitializer(x => this.mediaType = x.ToString(), track => track.MediaType);
            AddInitializer(x => this.title = x.ToString(), track => track.Title);
            AddInitializer(x => this.titleSort = x.ToString(), track => track.TitleSort);
            AddInitializer(x => this.subtitle = x.ToString(), track => track.Subtitle);
            AddInitializer(x => this.grouping = x.ToString(), track => track.Grouping);
            AddInitializer(x => this.comment = x.ToString(), track => track.Comment);
            AddInitializer(x => this.album = x.ToString(), track => track.Album);
            AddInitializer(x => this.albumSort = x.ToString(), track => track.AlbumSort);
            AddInitializer(x => this.albumSubtitle = x.ToString(), track => track.AlbumSubtitle);
            AddInitializer(x => this.artists = x.ToNames(), track => track.Artists);
            AddInitializer(x => this.artistsSort = x.ToString(), track => track.ArtistsSort);
            AddInitializer(x => this.albumArtists = x.ToNames(), track => track.AlbumArtists);
            AddInitializer(x => this.composers = x.ToString(), track => track.Composers);
            AddInitializer(x => this.conductor = x.ToString(), track => track.Conductor);
            AddInitializer(x => this.genres = x.ToString(), track => track.Genres);
            AddInitializer(x => this.moods = x.ToString(), track => track.Moods);
            AddInitializer(value => this.languages = value.ToIso639Languages(), track => track.Languages);
            AddInitializer(x => this.recordingDate = x.ToDateTime(), track => track.RecordingDate);
            AddInitializer(x => this.releaseDate = x.ToDateTime(), track => track.ReleaseDate);
            AddInitializer(x => this.originalTitle = x.ToString(), track => track.OriginalTitle);
            AddInitializer(x => this.originalReleaseDate = x.ToDateTime(), track => track.OriginalReleaseDate);
            AddInitializer(x => this.trackNumber = x.ToUInt32(), track => track.TrackNumber);
            AddInitializer(x => this.trackCount = x.ToUInt32(), track => track.TrackCount);
            AddInitializer(x => this.discNumber = x.ToUInt32(), track => track.DiscNumber);
            AddInitializer(x => this.discCount = x.ToUInt32(), track => track.DiscCount);
            AddInitializer(x => this.duration = x.ToTimeSpan(), track => track.Duration);
            AddInitializer(x => this.beatsPerMinute = x.ToUInt32(), track => track.BeatsPerMinute);
            AddInitializer(x => this.playCount = x.ToUInt64(), track => track.PlayCount);
            AddInitializer(x => this.playlistDelay = x.ToTimeSpan(), track => track.PlaylistDelay);
            AddInitializer(x => this.originalFileName = x.ToString(), track => track.OriginalFileName);
            AddInitializer(x => this.encodingDate = x.ToDateTime(), track => track.EncodingDate);
            AddInitializer(x => this.taggingDate = x.ToDateTime(), track => track.TaggingDate);
            AddInitializer(x => this.copyright = x.ToString(), track => track.Copyright);
            AddInitializer(x => this.publisher = x.ToString(), track => track.Publisher);
            AddInitializer(x => this.internationalStandardRecordingCode = x.ToString(), track => track.InternationalStandardRecordingCode);
            
            AddChildInitializer<ITrackPicture>(child => AddPicture(child as ITrackPicture));
            AddChildInitializer<ITrackUnsynchronizedLyrics>(child => AddLyrics(child as ITrackUnsynchronizedLyrics));
            AddChildInitializer<ITrackSynchronizedLyrics>(child => AddSynchronizedLyrics(child as ITrackSynchronizedLyrics));
            AddChildInitializer<ITrackRating>(child => AddRating(child as ITrackRating));
            
            AddValueInitializer(value => AddIdentifier(value as ITrackIdentifier), x => x.Identifiers);
            AddValueInitializer(value => AddLink(value as ITrackLink), x => x.Links);
            AddValueInitializer(value => AddMetadatum(value as ITrackMetadatum), x => x.Metadata);
            AddValueInitializer(value => AddTitleHashCode(value as IHashCode), x => x.TitleHashCodes);
            AddValueInitializer(value => AddAlbumHashCode(value as IHashCode), x => x.AlbumHashCodes);
            AddValueInitializer(value => AddArtistHashCode(value as IHashCode), x => x.ArtistHashCodes);
            AddValueInitializer(value => AddAlbumArtistHashCode(value as IHashCode), x => x.AlbumArtistHashCodes);
            AddValueInitializer(value => AddComposerHashCode(value as IHashCode), x => x.ComposerHashCodes);
            AddValueInitializer(value => AddConductorHashCode(value as IHashCode), x => x.ConductorHashCodes);
            AddValueInitializer(value => AddOriginalTitleHashCode(value as IHashCode), x => x.OriginalTitleHashCodes);
            AddValueInitializer(value => AddOriginalArtistHashCode(value as IHashCode), x => x.OriginalArtistHashCodes);

            AddHashFunction(HashCode.SchemeDoubleMetaphone, token => HashCode.CreateDoubleMetaphoneHash(this.Id, token));
            AddHashFunction(HashCode.SchemeNameHash, token => HashCode.CreateNameHash(this.Id, token));

            AddHashInitializer(hashCode => AddTitleHashCode(hashCode), hashCode => RemoveTitleHashCode(hashCode), track => track.TitleHashCodes);
            AddHashInitializer(hashCode => AddArtistHashCode(hashCode), hashCode => RemoveArtistHashCode(hashCode), track => track.ArtistHashCodes);
            AddHashInitializer(hashCode => AddAlbumHashCode(hashCode), hashCode => RemoveAlbumHashCode(hashCode), track => track.AlbumHashCodes);
            AddHashInitializer(hashCode => AddAlbumArtistHashCode(hashCode), hashCode => RemoveAlbumArtistHashCode(hashCode), track => track.AlbumArtistHashCodes);
            AddHashInitializer(hashCode => AddComposerHashCode(hashCode), hashCode => RemoveComposerHashCode(hashCode), track => track.ComposerHashCodes);
            AddHashInitializer(hashCode => AddConductorHashCode(hashCode), hashCode => RemoveConductorHashCode(hashCode), track => track.ConductorHashCodes);
            AddHashInitializer(hashCode => AddOriginalTitleHashCode(hashCode), hashCode => RemoveOriginalTitleHashCode(hashCode), track => track.OriginalTitleHashCodes);
            AddHashInitializer(hashCode => AddOriginalArtistHashCode(hashCode), hashCode => RemoveOriginalArtistHashCode(hashCode), track => track.OriginalArtistHashCodes);
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
        private IEnumerable<string> artists = new List<string>{ "Unknown Artist" };
        private string artistsSort = "Unknown Artist";
        private IEnumerable<string> albumArtists = new List<string>();
        private string composers = string.Empty;
        private string conductor = string.Empty;
        private string genres = string.Empty;
        private string moods = string.Empty;
        private IEnumerable<IIso639Language> languages = new List<IIso639Language>();
        private DateTime recordingDate = DateTime.MinValue;
        private DateTime releaseDate = DateTime.MinValue;
        private string originalTitle = string.Empty;
        private string originalArtists = string.Empty;
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

        private TagLib.File file;
        private TagLib.Id3v2.Tag tag;

        private readonly IList<ITrackPicture> pictures = new ObservableCollection<ITrackPicture>();
        private readonly IList<ITrackUnsynchronizedLyrics> lyrics = new ObservableCollection<ITrackUnsynchronizedLyrics>();
        private readonly IList<ITrackSynchronizedLyrics> synchronizedLyrics = new ObservableCollection<ITrackSynchronizedLyrics>();
        private readonly IList<ITrackIdentifier> identifiers = new ObservableCollection<ITrackIdentifier>();
        private readonly IList<ITrackRating> ratings = new ObservableCollection<ITrackRating>();
        private readonly IList<ITrackLink> links = new ObservableCollection<ITrackLink>();
        private readonly IList<ITrackMetadatum> metadata = new ObservableCollection<ITrackMetadatum>();

        private readonly IList<IHashCode> titleHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> albumHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> artistHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> albumArtistHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> composerHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> conductorHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> originalTitleHashCodes = new ObservableCollection<IHashCode>();
        private readonly IList<IHashCode> originalArtistHashCodes = new ObservableCollection<IHashCode>();

        #region Private Methods

        private void AddPicture(ITrackPicture picture)
        {
            AddChild<ITrackPicture>(() => pictures.Add(picture), picture, x => x.Pictures);
        }

        private void AddLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            AddChild<ITrackUnsynchronizedLyrics>(() => this.lyrics.Add(lyrics), lyrics, x => x.Lyrics);
        }

        private void AddRating(ITrackRating rating)
        {
            AddChild<ITrackRating>(() => this.ratings.Add(rating), rating, x => x.Ratings);
        }

        private void AddIdentifier(ITrackIdentifier identifier)
        {
            AddValue<ITrackIdentifier>(() => this.identifiers.Add(identifier), identifier, x => x.Identifiers);
        }

        private void AddLink(ITrackLink link)
        {
            AddValue<ITrackLink>(() => this.links.Add(link), link, x => x.Links);
        }

        private void AddMetadatum(ITrackMetadatum metadatum)
        {
            AddValue<ITrackMetadatum>(() => this.metadata.Add(metadatum), metadatum, x => x.Metadata);
        }

        private void AddTitleHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.titleHashCodes.Add(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void RemoveTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.titleHashCodes.Remove(hashCode), hashCode, x => x.TitleHashCodes);
        }

        private void AddAlbumHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.albumHashCodes.Add(hashCode), hashCode, x => x.AlbumHashCodes);
        }

        private void RemoveAlbumHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.albumHashCodes.Remove(hashCode), hashCode, x => x.AlbumHashCodes);
        }

        private void AddArtistHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.artistHashCodes.Add(hashCode), hashCode, x => x.ArtistHashCodes);
        }

        private void RemoveArtistHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.albumHashCodes.Remove(hashCode), hashCode, x => x.ArtistHashCodes);
        }

        private void AddAlbumArtistHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.albumArtistHashCodes.Add(hashCode), hashCode, x => x.AlbumArtistHashCodes);
        }

        private void RemoveAlbumArtistHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.albumArtistHashCodes.Remove(hashCode), hashCode, x => x.AlbumArtistHashCodes);
        }

        private void AddComposerHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.composerHashCodes.Add(hashCode), hashCode, x => x.ComposerHashCodes);
        }

        private void RemoveComposerHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.composerHashCodes.Remove(hashCode), hashCode, x => x.ComposerHashCodes);
        }

        private void AddConductorHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.conductorHashCodes.Add(hashCode), hashCode, x => x.ConductorHashCodes);
        }

        private void RemoveConductorHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.conductorHashCodes.Remove(hashCode), hashCode, x => x.ConductorHashCodes);
        }

        private void AddOriginalTitleHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.originalTitleHashCodes.Add(hashCode), hashCode, x => x.OriginalTitleHashCodes);
        }

        private void RemoveOriginalTitleHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.originalTitleHashCodes.Remove(hashCode), hashCode, x => x.OriginalTitleHashCodes);
        }

        private void AddOriginalArtistHashCode(IHashCode hashCode)
        {
            AddValue<IHashCode>(() => this.originalArtistHashCodes.Add(hashCode), hashCode, x => x.OriginalArtistHashCodes);
        }

        private void RemoveOriginalArtistHashCode(IHashCode hashCode)
        {
            RemoveValue<IHashCode>(() => this.originalArtistHashCodes.Remove(hashCode), hashCode, x => x.OriginalArtistHashCodes);
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
                    Change(() => location = value, x => x.Location);
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
                    Change(() => mediaType = value, x => x.MediaType);
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
                    Change(() => title = value, x => x.Title);
                    RefreshHashCodes(value, x => x.TitleHashCodes);
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
                    Change(() => titleSort = value, x => x.TitleSort);
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
                    Change(() => subtitle = value, x => x.Subtitle);
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
                    Change(() => grouping = value, x => x.Grouping);
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
                    Change(() => comment = value, x => x.Comment);
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
                    Change(() => album = value, x => x.Album);
                    RefreshHashCodes(value, x => x.AlbumHashCodes);
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
                    Change(() => albumSort = value, x => x.AlbumSort);
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
                    Change(() => albumSubtitle = value, x => x.AlbumSubtitle);
                }
            }
        }

        public IEnumerable<string> Artists
        {
            get { return artists; }
            set
            {
                if (value != null && value != artists)
                {
                    Change(() => artists = value, x => x.Artists);
                    RefreshHashCodes(value, x => x.ArtistHashCodes);
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
                    Change(() => artistsSort = value, x => x.ArtistsSort);
                }
            }
        }

        public IEnumerable<string> AlbumArtists
        {
            get { return albumArtists; }
            set
            {
                if (value != null && value != albumArtists)
                {
                    Change(() => albumArtists = value, x => x.AlbumArtists);
                    RefreshHashCodes(value, x => x.AlbumArtistHashCodes);
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
                    Change(() => composers = value, x => x.Composers);
                    RefreshHashCodes(value, x => x.ComposerHashCodes);
                }
            }
        }

        public string Conductor
        {
            get { return conductor; }
            set
            {
                if (value != null && value != conductor)
                {
                    Change(() => conductor = value, x => x.Conductor);
                    RefreshHashCodes(value, x => x.ConductorHashCodes);
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
                    Change(() => genres = value, x => x.Genres);
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
                    Change(() => moods = value, x => x.Moods);
                }
            }
        }

        public IEnumerable<IIso639Language> Languages
        {
            get { return languages; }
            set
            {
                if (value != null && value != languages)
                {
                    Change(() => languages = value, track => track.Languages);
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
                    Change(() => recordingDate = value, x => x.RecordingDate);
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
                    Change(() => releaseDate = value, x => x.ReleaseDate);
                }
            }
        }

        public string OriginalTitle
        {
            get { return originalTitle; }
            set
            {
                if (value != null && value != originalTitle)
                {
                    Change(() => originalTitle = value, x => x.OriginalTitle);
                    RefreshHashCodes(value, x => x.OriginalTitleHashCodes);
                }
            }
        }

        public string OriginalArtists
        {
            get { return originalArtists; }
            set
            {
                if (value != null && value != originalArtists)
                {
                    Change(() => originalArtists = value, track => track.OriginalArtists);
                    RefreshHashCodes(value, track => track.OriginalArtistHashCodes);
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
                    Change(() => originalReleaseDate = value, x => x.OriginalReleaseDate);
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
                    Change(() => trackNumber = value, x => x.TrackNumber);
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
                    Change(() => trackCount = value, x => x.TrackCount);
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
                    Change(() => discNumber = value, x => x.DiscNumber);
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
                    Change(() => discCount = value, x => x.DiscCount);
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
                    Change(() => duration = value, x => x.Duration);
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
                    Change(() => beatsPerMinute = value, x => x.BeatsPerMinute);
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
                    Change(() => playCount = value, x => x.PlayCount);
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
                    Change(() => playlistDelay = value, x => x.PlaylistDelay);
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
                    Change(() => originalFileName = value, x => x.OriginalFileName);
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
                    Change(() => encodingDate = value, x => x.EncodingDate);
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
                    Change(() => taggingDate = value, x => x.TaggingDate);
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
                    Change(() => copyright = value, x => x.Copyright);
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
                    Change(() => publisher = value, x => x.Publisher);
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
                    Change(() => internationalStandardRecordingCode = value, x => x.InternationalStandardRecordingCode);
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

        public IEnumerable<IHashCode> OriginalArtistHashCodes
        {
            get { return originalArtistHashCodes; }
        }


        public void AddPicture(string mediaType, TrackPictureType pictureType, string description, byte[] data)
        {
            var picture = new TrackPicture();
            picture.Initialize(new EntityInitialState(Context, Logger, this.Id));
            picture.TextEncoding = TextEncoding.UTF8;
            picture.MediaType = mediaType;
            picture.PictureType = pictureType;
            picture.Description = description;
            picture.Data = data;
            AddPicture(picture);
        }

        public void RemovePicture(ITrackPicture picture)
        {
            RemoveChild<ITrackPicture>(() => pictures.Remove(picture), picture, x => x.Pictures);
        }


        public void AddLyrics(string language, string description, string text)
        {
            var lyrics = new TrackUnsynchronizedLyrics();
            lyrics.Initialize(new EntityInitialState(Context, Logger, this.Id));
            lyrics.TextEncoding = TextEncoding.UTF8;
            lyrics.Language = language;
            lyrics.Description = description;
            lyrics.Text = text;
            AddLyrics(lyrics);
        }

        public void RemoveLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            RemoveChild<ITrackUnsynchronizedLyrics>(() => this.lyrics.Remove(lyrics), lyrics, x => x.Lyrics);
        }

        public void AddSynchronizedLyrics(ITrackSynchronizedLyrics lyrics)
        {
            AddChild<ITrackSynchronizedLyrics>(() => this.synchronizedLyrics.Add(lyrics), lyrics, x => x.Lyrics);
        }

        public void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics lyrics)
        {
            RemoveChild<ITrackSynchronizedLyrics>(() => this.synchronizedLyrics.Remove(lyrics), lyrics, x => x.SynchronizedLyrics);
        }


        public void AddRating(byte score, Uri user, ulong playCount)
        {
            var rating = new TrackRating();
            rating.Initialize(new EntityInitialState(Context, Logger, this.Id));
            rating.Score = score;
            rating.User = user;
            rating.PlayCount = playCount;
            AddRating(rating);
        }

        public void RemoveRating(ITrackRating rating)
        {
            RemoveChild<ITrackRating>(() => this.ratings.Remove(rating), rating, x => x.Ratings);
        }


        public void AddIdentifier(Uri scheme, string identifier)
        {
            AddIdentifier(new TrackIdentifier(this.Id, scheme, identifier));
        }

        public void RemoveIdentifier(ITrackIdentifier identifier)
        {
            RemoveValue<ITrackIdentifier>(() => this.identifiers.Remove(identifier), identifier, x => x.Identifiers);
        }


        public void AddLink(string relationship, Uri location)
        {
            AddLink(new TrackLink(this.Id, TextEncoding.UTF8, relationship, location));
        }

        public void RemoveLink(ITrackLink link)
        {
            RemoveValue<ITrackLink>(() => this.links.Remove(link), link, x => x.Links);
        }


        public void AddMetadatum(string description, string content)
        {
            AddMetadatum(new TrackMetadatum(this.Id, TextEncoding.UTF8, description, content));
        }

        public void RemoveMetadatum(ITrackMetadatum metadatum)
        {
            RemoveValue<ITrackMetadatum>(() => this.metadata.Remove(metadatum), metadatum, x => x.Metadata);
        }

        private void InitializeTag()
        {
            if (Location == null || !Location.IsFile || MediaType != "audio/mpeg")
                throw new InvalidOperationException("Only local MP3 files can be tagged - tracks with an invalid or remote location cannot be tagged");

            if (file == null)
                file = TagLib.File.Create(Location.LocalPath);
            
            if (tag == null)
                tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;
        }

        public void LoadTag()
        {
            InitializeTag();

            Title = tag.Title;
            TitleSort = tag.TitleSort;
            Subtitle = tag.Subtitle;
            Grouping = tag.Grouping;
            Comment = tag.Comment;
            Album = tag.Album;
            AlbumSort = tag.AlbumSort;
            AlbumSubtitle = tag.AlbumSubtitle;
            Artists = tag.Performers.AsEnumerable();
            ArtistsSort = tag.ArtistsSort;
            AlbumArtists = tag.AlbumArtists.AsEnumerable();
            Composers = tag.JoinedComposers;
            Conductor = tag.Conductor;
            Genres = tag.JoinedGenres;
            Moods = tag.Moods;
            Languages = !string.IsNullOrEmpty(tag.Languages) ? tag.Languages.Split('/').Select(code => Iso639Language.GetLanguageByCode(code)) : new List<IIso639Language> { Iso639Language.Undetermined };
            RecordingDate = tag.RecordingDate;
            ReleaseDate = tag.ReleaseDate;
            OriginalTitle = tag.OriginalTitle;
            OriginalArtists = tag.JoinedOriginalArtists;
            OriginalReleaseDate = tag.OriginalReleaseDate;
            TrackNumber = tag.Track;
            TrackCount = tag.TrackCount;
            DiscNumber = tag.Disc;
            DiscCount = tag.DiscCount;
            Duration = tag.Duration;
            BeatsPerMinute = tag.BeatsPerMinute;
            PlayCount = tag.PlayCount;
            PlaylistDelay = tag.PlaylistDelay;
            OriginalFileName = tag.OriginalFilename;
            EncodingDate = tag.EncodingDate;
            TaggingDate = tag.TaggingDate;
            Copyright = tag.Copyright;
            Publisher = tag.Publisher;
            InternationalStandardRecordingCode = tag.InternationalStandardRecordingCode;

            //TODO: Synch lyrics and ad-hoc metadata
        }

        public void SaveTag()
        {
            InitializeTag();

            tag.Title = Title;
            tag.TitleSort = TitleSort;
            tag.Subtitle = Subtitle;
            tag.Grouping = Grouping;
            tag.Comment = Comment;
            tag.Album = Album;
            tag.AlbumSort = AlbumSort;
            tag.AlbumSubtitle = AlbumSubtitle;
            tag.Performers = Artists.ToArray();
            tag.ArtistsSort = ArtistsSort;
            tag.AlbumArtists = AlbumArtists.ToArray();
            tag.Composers = Composers.ToNames().ToArray();
            tag.Conductor = Conductor;
            tag.Genres = Genres.Split('/');
            tag.Moods = Moods;
            tag.Languages = string.Join("/", Languages.Select(lang => lang.Alpha3Code));
            tag.RecordingDate = RecordingDate;
            tag.ReleaseDate = ReleaseDate;
            tag.OriginalTitle = OriginalTitle;
            tag.OriginalArtists = Artists.ToNames().ToArray();
            tag.OriginalReleaseDate = OriginalReleaseDate;
            tag.Track = TrackNumber;
            tag.TrackCount = TrackCount;
            tag.Disc = DiscNumber;
            tag.DiscCount = DiscCount;
            tag.Duration = Duration;
            tag.BeatsPerMinute = BeatsPerMinute;
            tag.PlayCount = PlayCount;
            tag.PlaylistDelay = PlaylistDelay;
            tag.OriginalFilename = OriginalFileName;
            tag.EncodingDate = EncodingDate;
            tag.TaggingDate = TaggingDate;
            tag.Copyright = Copyright;
            tag.Publisher = Publisher;
            tag.InternationalStandardRecordingCode = InternationalStandardRecordingCode;

            //TODO: Synch lyrics and ad-hoc metadata

            file.Save();
        }

        #endregion
    }
}
