using System;
using System.Collections.Generic;

using Gnosis.Culture;
using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrack : IEntity
    {
        Uri Location { get; set; }
        string MediaType { get; set; }

        string Title { get; set; }
        string TitleSort { get; set; }
        string Subtitle { get; set; }
        string Grouping { get; set; }
        string Comment { get; set; }

        string Album { get; set; }
        string AlbumSort { get; set; }
        string AlbumSubtitle { get; set; }
        
        IEnumerable<string> Artists { get; set; }
        string ArtistsSort { get; set; }
        IEnumerable<string> AlbumArtists { get; set; }

        IEnumerable<string> Composers { get; set; }
        IEnumerable<string> Genres { get; set; }
        IEnumerable<string> Moods { get; set; }
        IEnumerable<ILanguage> Languages { get; set; }
        string Conductor { get; set; }

        DateTime RecordingDate { get; set; }
        DateTime ReleaseDate { get; set; }

        string OriginalTitle { get; set; }
        IEnumerable<string> OriginalArtists { get; set; }
        DateTime OriginalReleaseDate { get; set; }

        uint TrackNumber { get; set; }
        uint TrackCount { get; set; }
        uint DiscNumber { get; set; }
        uint DiscCount { get; set; }
        
        TimeSpan Duration { get; set; }
        uint BeatsPerMinute { get; set; }
        
        ulong PlayCount { get; set; }
        TimeSpan PlaylistDelay { get; set; }

        string OriginalFileName { get; set; }
        DateTime EncodingDate { get; set; }
        DateTime TaggingDate { get; set; }

        string Copyright { get; set; }
        string Publisher { get; set; }
        string InternationalStandardRecordingCode { get; set; }

        IEnumerable<ITrackPicture> Pictures { get; }
        IEnumerable<ITrackUnsynchronizedLyrics> Lyrics { get; }
        IEnumerable<ITrackSynchronizedLyrics> SynchronizedLyrics { get; }
        IEnumerable<ITrackRating> Ratings { get; }
        IEnumerable<ITrackIdentifier> Identifiers { get; }
        IEnumerable<ITrackLink> Links { get; }
        IEnumerable<ITrackMetadatum> Metadata { get; }

        IEnumerable<ITag> TitleTags { get; }
        IEnumerable<ITag> AlbumTags { get; }
        IEnumerable<ITag> ArtistTags { get; }
        IEnumerable<ITag> AlbumArtistTags { get; }
        IEnumerable<ITag> ComposerTags { get; }
        IEnumerable<ITag> ConductorTags { get; }
        IEnumerable<ITag> OriginalTitleTags { get; }
        IEnumerable<ITag> OriginalArtistTags { get; }

        void AddPicture(string mediaType, TrackPictureType pictureType, string description, byte[] data);
        void RemovePicture(ITrackPicture picture);

        void AddLyrics(string language, string description, string lyrics);
        void RemoveLyrics(ITrackUnsynchronizedLyrics lyrics);

        void AddSynchronizedLyrics(ITrackSynchronizedLyrics lyrics);
        void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics lyrics);

        void AddRating(byte rating, Uri user, ulong playCount);
        void RemoveRating(ITrackRating rating);

        void AddIdentifier(Uri scheme, string identifier);
        void RemoveIdentifier(ITrackIdentifier identifier);

        void AddLink(string relationship, Uri location);
        void RemoveLink(ITrackLink link);

        void AddMetadatum(string description, string content);
        void RemoveMetadatum(ITrackMetadatum metadatum);

        void LoadTag();
        void SaveTag();
    }
}
