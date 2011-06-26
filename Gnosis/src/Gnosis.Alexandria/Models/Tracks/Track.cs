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
            AddInitializer(value => this.mediaType = value.ToString(), track => track.MediaType);
            AddInitializer(value => this.title = value.ToString(), track => track.Title);
            AddInitializer(value => this.titleSort = value.ToString(), track => track.TitleSort);
            AddInitializer(value => this.subtitle = value.ToString(), track => track.Subtitle);
            AddInitializer(value => this.grouping = value.ToString(), track => track.Grouping);
            AddInitializer(value => this.comment = value.ToString(), track => track.Comment);
            AddInitializer(value => this.album = value.ToString(), track => track.Album);
            AddInitializer(value => this.albumSort = value.ToString(), track => track.AlbumSort);
            AddInitializer(value => this.albumSubtitle = value.ToString(), track => track.AlbumSubtitle);
            AddInitializer(value => this.artists = value.ToNames(), track => track.Artists);
            AddInitializer(value => this.artistsSort = value.ToString(), track => track.ArtistsSort);
            AddInitializer(value => this.albumArtists = value.ToNames(), track => track.AlbumArtists);
            AddInitializer(value => this.composers = value.ToNames(), track => track.Composers);
            AddInitializer(value => this.genres = value.ToNames(), track => track.Genres);
            AddInitializer(value => this.moods = value.ToNames(), track => track.Moods);
            AddInitializer(value => this.languages = value.ToLanguages(), track => track.Languages);
            AddInitializer(value => this.conductor = value.ToString(), track => track.Conductor);
            AddInitializer(value => this.recordingDate = value.ToDateTime(), track => track.RecordingDate);
            AddInitializer(value => this.releaseDate = value.ToDateTime(), track => track.ReleaseDate);
            AddInitializer(value => this.originalTitle = value.ToString(), track => track.OriginalTitle);
            AddInitializer(value => this.originalArtists = value.ToNames(), track => track.OriginalArtists);
            AddInitializer(value => this.originalReleaseDate = value.ToDateTime(), track => track.OriginalReleaseDate);
            AddInitializer(value => this.trackNumber = value.ToUInt32(), track => track.TrackNumber);
            AddInitializer(value => this.trackCount = value.ToUInt32(), track => track.TrackCount);
            AddInitializer(value => this.discNumber = value.ToUInt32(), track => track.DiscNumber);
            AddInitializer(value => this.discCount = value.ToUInt32(), track => track.DiscCount);
            AddInitializer(value => this.duration = value.ToTimeSpan(), track => track.Duration);
            AddInitializer(value => this.beatsPerMinute = value.ToUInt32(), track => track.BeatsPerMinute);
            AddInitializer(value => this.playCount = value.ToUInt64(), track => track.PlayCount);
            AddInitializer(value => this.playlistDelay = value.ToTimeSpan(), track => track.PlaylistDelay);
            AddInitializer(value => this.originalFileName = value.ToString(), track => track.OriginalFileName);
            AddInitializer(value => this.encodingDate = value.ToDateTime(), track => track.EncodingDate);
            AddInitializer(value => this.taggingDate = value.ToDateTime(), track => track.TaggingDate);
            AddInitializer(value => this.copyright = value.ToString(), track => track.Copyright);
            AddInitializer(value => this.publisher = value.ToString(), track => track.Publisher);
            AddInitializer(value => this.internationalStandardRecordingCode = value.ToString(), track => track.InternationalStandardRecordingCode);
            
            AddChildInitializer<ITrackPicture>(child => AddPicture(child as ITrackPicture));
            AddChildInitializer<ITrackUnsynchronizedLyrics>(child => AddLyrics(child as ITrackUnsynchronizedLyrics));
            AddChildInitializer<ITrackSynchronizedLyrics>(child => AddSynchronizedLyrics(child as ITrackSynchronizedLyrics));
            AddChildInitializer<ITrackRating>(child => AddRating(child as ITrackRating));
            
            AddValueInitializer(value => AddIdentifier(value as ITrackIdentifier), track => track.Identifiers);
            AddValueInitializer(value => AddLink(value as ITrackLink), track => track.Links);
            AddValueInitializer(value => AddMetadatum(value as ITrackMetadatum), track => track.Metadata);
            AddValueInitializer(value => AddTitleTag(value as ITag), track => track.TitleTags);
            AddValueInitializer(value => AddAlbumTag(value as ITag), track => track.AlbumTags);
            AddValueInitializer(value => AddArtistTag(value as ITag), track => track.ArtistTags);
            AddValueInitializer(value => AddAlbumArtistTag(value as ITag), track => track.AlbumArtistTags);
            AddValueInitializer(value => AddComposerTag(value as ITag), track => track.ComposerTags);
            AddValueInitializer(value => AddConductorTag(value as ITag), track => track.ConductorTags);
            AddValueInitializer(value => AddOriginalTitleTag(value as ITag), track => track.OriginalTitleTags);
            AddValueInitializer(value => AddOriginalArtistTag(value as ITag), track => track.OriginalArtistTags);

            AddHashFunction(Tag.SchemeDoubleMetaphone, token => Tag.CreateDoubleMetaphoneHash(this.Id, token));
            AddHashFunction(Tag.SchemeAmericanizedGraph, token => Tag.CreateAmericanizedGraph(this.Id, token));

            AddHashInitializer(tag => AddTitleTag(tag), tag => RemoveTitleTag(tag), track => track.TitleTags);
            AddHashInitializer(tag => AddArtistTag(tag), tag => RemoveArtistTag(tag), track => track.ArtistTags);
            AddHashInitializer(tag => AddAlbumTag(tag), tag => RemoveAlbumTag(tag), track => track.AlbumTags);
            AddHashInitializer(tag => AddAlbumArtistTag(tag), tag => RemoveAlbumArtistTag(tag), track => track.AlbumArtistTags);
            AddHashInitializer(tag => AddComposerTag(tag), tag => RemoveComposerTag(tag), track => track.ComposerTags);
            AddHashInitializer(tag => AddConductorTag(tag), tag => RemoveConductorTag(tag), track => track.ConductorTags);
            AddHashInitializer(tag => AddOriginalTitleTag(tag), tag => RemoveOriginalTitleTag(tag), track => track.OriginalTitleTags);
            AddHashInitializer(tag => AddOriginalArtistTag(tag), tag => RemoveOriginalArtistTag(tag), track => track.OriginalArtistTags);
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
        private IEnumerable<string> composers = new List<string>();
        private IEnumerable<string> genres = new List<string>();
        private IEnumerable<string> moods = new List<string>();
        private IEnumerable<ILanguage> languages = new List<ILanguage>();
        private string conductor = string.Empty;
        private DateTime recordingDate = DateTime.MinValue;
        private DateTime releaseDate = DateTime.MinValue;
        private string originalTitle = string.Empty;
        private IEnumerable<string> originalArtists = new List<string>();
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

        private readonly IList<ITag> titleTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> albumTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> artistTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> albumArtistTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> composerTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> conductorTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> originalTitleTags = new ObservableCollection<ITag>();
        private readonly IList<ITag> originalArtistTags = new ObservableCollection<ITag>();

        #region Private Methods

        private void AddPicture(ITrackPicture picture)
        {
            AddChild<ITrackPicture>(() => pictures.Add(picture), picture, track => track.Pictures);
        }

        private void AddLyrics(ITrackUnsynchronizedLyrics lyrics)
        {
            AddChild<ITrackUnsynchronizedLyrics>(() => this.lyrics.Add(lyrics), lyrics, track => track.Lyrics);
        }

        private void AddRating(ITrackRating rating)
        {
            AddChild<ITrackRating>(() => this.ratings.Add(rating), rating, track => track.Ratings);
        }

        private void AddIdentifier(ITrackIdentifier identifier)
        {
            AddValue<ITrackIdentifier>(() => this.identifiers.Add(identifier), identifier, track => track.Identifiers);
        }

        private void AddLink(ITrackLink link)
        {
            AddValue<ITrackLink>(() => this.links.Add(link), link, track => track.Links);
        }

        private void AddMetadatum(ITrackMetadatum metadatum)
        {
            AddValue<ITrackMetadatum>(() => this.metadata.Add(metadatum), metadatum, track => track.Metadata);
        }

        private void AddTitleTag(ITag tag)
        {
            AddValue<ITag>(() => this.titleTags.Add(tag), tag, track => track.TitleTags);
        }

        private void RemoveTitleTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.titleTags.Remove(tag), tag, track => track.TitleTags);
        }

        private void AddAlbumTag(ITag tag)
        {
            AddValue<ITag>(() => this.albumTags.Add(tag), tag, track => track.AlbumTags);
        }

        private void RemoveAlbumTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.albumTags.Remove(tag), tag, track => track.AlbumTags);
        }

        private void AddArtistTag(ITag tag)
        {
            AddValue<ITag>(() => this.artistTags.Add(tag), tag, track => track.ArtistTags);
        }

        private void RemoveArtistTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.albumTags.Remove(tag), tag, track => track.ArtistTags);
        }

        private void AddAlbumArtistTag(ITag tag)
        {
            AddValue<ITag>(() => this.albumArtistTags.Add(tag), tag, track => track.AlbumArtistTags);
        }

        private void RemoveAlbumArtistTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.albumArtistTags.Remove(tag), tag, track => track.AlbumArtistTags);
        }

        private void AddComposerTag(ITag tag)
        {
            AddValue<ITag>(() => this.composerTags.Add(tag), tag, track => track.ComposerTags);
        }

        private void RemoveComposerTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.composerTags.Remove(tag), tag, track => track.ComposerTags);
        }

        private void AddConductorTag(ITag tag)
        {
            AddValue<ITag>(() => this.conductorTags.Add(tag), tag, track => track.ConductorTags);
        }

        private void RemoveConductorTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.conductorTags.Remove(tag), tag, track => track.ConductorTags);
        }

        private void AddOriginalTitleTag(ITag tag)
        {
            AddValue<ITag>(() => this.originalTitleTags.Add(tag), tag, track => track.OriginalTitleTags);
        }

        private void RemoveOriginalTitleTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.originalTitleTags.Remove(tag), tag, track => track.OriginalTitleTags);
        }

        private void AddOriginalArtistTag(ITag tag)
        {
            AddValue<ITag>(() => this.originalArtistTags.Add(tag), tag, track => track.OriginalArtistTags);
        }

        private void RemoveOriginalArtistTag(ITag tag)
        {
            RemoveValue<ITag>(() => this.originalArtistTags.Remove(tag), tag, track => track.OriginalArtistTags);
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
                    Change(() => location = value, track => track.Location);
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
                    Change(() => mediaType = value, track => track.MediaType);
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
                    Change(() => title = value, track => track.Title);
                    RefreshTags(value, track => track.TitleTags);
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
                    Change(() => titleSort = value, track => track.TitleSort);
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
                    Change(() => subtitle = value, track => track.Subtitle);
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
                    Change(() => grouping = value, track => track.Grouping);
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
                    Change(() => comment = value, track => track.Comment);
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
                    Change(() => album = value, track => track.Album);
                    RefreshTags(value, track => track.AlbumTags);
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
                    Change(() => albumSort = value, track => track.AlbumSort);
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
                    Change(() => albumSubtitle = value, track => track.AlbumSubtitle);
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
                    Change(() => artists = value, track => track.Artists);
                    RefreshTags(value, track => track.ArtistTags);
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
                    Change(() => artistsSort = value, track => track.ArtistsSort);
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
                    Change(() => albumArtists = value, track => track.AlbumArtists);
                    RefreshTags(value, track => track.AlbumArtistTags);
                }
            }
        }

        public IEnumerable<string> Composers
        {
            get { return composers; }
            set
            {
                if (value != null && value != composers)
                {
                    Change(() => composers = value, track => track.Composers);
                    RefreshTags(value, track => track.ComposerTags);
                }
            }
        }

        public IEnumerable<string> Genres
        {
            get { return genres; }
            set
            {
                if (value != null && value != genres)
                {
                    Change(() => genres = value, track => track.Genres);
                }
            }
        }

        public IEnumerable<string> Moods
        {
            get { return moods; }
            set
            {
                if (value != null && value != moods)
                {
                    Change(() => moods = value, track => track.Moods);
                }
            }
        }

        public IEnumerable<ILanguage> Languages
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

        public string Conductor
        {
            get { return conductor; }
            set
            {
                if (value != null && value != conductor)
                {
                    Change(() => conductor = value, track => track.Conductor);
                    RefreshTags(value, track => track.ConductorTags);
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
                    Change(() => recordingDate = value, track => track.RecordingDate);
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
                    Change(() => releaseDate = value, track => track.ReleaseDate);
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
                    Change(() => originalTitle = value, track => track.OriginalTitle);
                    RefreshTags(value, track => track.OriginalTitleTags);
                }
            }
        }

        public IEnumerable<string> OriginalArtists
        {
            get { return originalArtists; }
            set
            {
                if (value != null && value != originalArtists)
                {
                    Change(() => originalArtists = value, track => track.OriginalArtists);
                    RefreshTags(value, track => track.OriginalArtistTags);
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
                    Change(() => originalReleaseDate = value, track => track.OriginalReleaseDate);
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
                    Change(() => trackNumber = value, track => track.TrackNumber);
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
                    Change(() => trackCount = value, track => track.TrackCount);
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
                    Change(() => discNumber = value, track => track.DiscNumber);
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
                    Change(() => discCount = value, track => track.DiscCount);
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
                    Change(() => duration = value, track => track.Duration);
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
                    Change(() => beatsPerMinute = value, track => track.BeatsPerMinute);
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
                    Change(() => playCount = value, track => track.PlayCount);
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
                    Change(() => playlistDelay = value, track => track.PlaylistDelay);
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
                    Change(() => originalFileName = value, track => track.OriginalFileName);
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
                    Change(() => encodingDate = value, track => track.EncodingDate);
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
                    Change(() => taggingDate = value, track => track.TaggingDate);
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
                    Change(() => copyright = value, track => track.Copyright);
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
                    Change(() => publisher = value, track => track.Publisher);
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
                    Change(() => internationalStandardRecordingCode = value, track => track.InternationalStandardRecordingCode);
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

        public IEnumerable<ITag> TitleTags
        {
            get { return titleTags; }
        }

        public IEnumerable<ITag> AlbumTags
        {
            get { return albumTags; }
        }

        public IEnumerable<ITag> ArtistTags
        {
            get { return artistTags; }
        }

        public IEnumerable<ITag> AlbumArtistTags
        {
            get { return albumArtistTags; }
        }

        public IEnumerable<ITag> ComposerTags
        {
            get { return composerTags; }
        }

        public IEnumerable<ITag> ConductorTags
        {
            get { return conductorTags; }
        }

        public IEnumerable<ITag> OriginalTitleTags
        {
            get { return originalTitleTags; }
        }

        public IEnumerable<ITag> OriginalArtistTags
        {
            get { return originalArtistTags; }
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
            RemoveChild<ITrackPicture>(() => pictures.Remove(picture), picture, track => track.Pictures);
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
            RemoveChild<ITrackUnsynchronizedLyrics>(() => this.lyrics.Remove(lyrics), lyrics, track => track.Lyrics);
        }

        public void AddSynchronizedLyrics(ITrackSynchronizedLyrics lyrics)
        {
            AddChild<ITrackSynchronizedLyrics>(() => this.synchronizedLyrics.Add(lyrics), lyrics, track => track.Lyrics);
        }

        public void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics lyrics)
        {
            RemoveChild<ITrackSynchronizedLyrics>(() => this.synchronizedLyrics.Remove(lyrics), lyrics, track => track.SynchronizedLyrics);
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
            RemoveChild<ITrackRating>(() => this.ratings.Remove(rating), rating, track => track.Ratings);
        }


        public void AddIdentifier(Uri scheme, string identifier)
        {
            AddIdentifier(new TrackIdentifier(this.Id, scheme, identifier));
        }

        public void RemoveIdentifier(ITrackIdentifier identifier)
        {
            RemoveValue<ITrackIdentifier>(() => this.identifiers.Remove(identifier), identifier, track => track.Identifiers);
        }


        public void AddLink(string relationship, Uri location)
        {
            AddLink(new TrackLink(this.Id, TextEncoding.UTF8, relationship, location));
        }

        public void RemoveLink(ITrackLink link)
        {
            RemoveValue<ITrackLink>(() => this.links.Remove(link), link, track => track.Links);
        }


        public void AddMetadatum(string description, string content)
        {
            AddMetadatum(new TrackMetadatum(this.Id, TextEncoding.UTF8, description, content));
        }

        public void RemoveMetadatum(ITrackMetadatum metadatum)
        {
            RemoveValue<ITrackMetadatum>(() => this.metadata.Remove(metadatum), metadatum, track => track.Metadata);
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
            Artists = tag.ArtistsList;
            ArtistsSort = tag.ArtistsSort;
            AlbumArtists = tag.AlbumArtistsList;
            Composers = tag.ComposersList;
            Genres = tag.GenresList;
            Moods = tag.Moods;
            Languages = tag.Languages.Select(code => Language.GetLanguageByCode(code));
            Conductor = tag.Conductor;
            RecordingDate = tag.RecordingDate;
            ReleaseDate = tag.ReleaseDate;
            OriginalTitle = tag.OriginalTitle;
            OriginalArtists = tag.OriginalArtists;
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
            tag.ArtistsList = Artists;
            tag.ArtistsSort = ArtistsSort;
            tag.AlbumArtistsList = AlbumArtists;
            tag.ComposersList = Composers;
            tag.GenresList = Genres;
            tag.Moods = Moods;
            tag.Languages = Languages.Select(lang => lang.ToString());
            tag.Conductor = Conductor;
            tag.RecordingDate = RecordingDate;
            tag.ReleaseDate = ReleaseDate;
            tag.OriginalTitle = OriginalTitle;
            tag.OriginalArtists = Artists;
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
