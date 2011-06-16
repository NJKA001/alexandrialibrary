using System;
using System.Collections.Generic;

using Gnosis.Core;

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
        
        string Artists { get; set; }
        string ArtistsSort { get; set; }
        string AlbumArtists { get; set; }

        string Composers { get; set; }
        string Conductors { get; set; }
        string Genres { get; set; }
        string Moods { get; set; }
        string Languages { get; set; }
        
        DateTime RecordingDate { get; set; }
        DateTime ReleaseDate { get; set; }

        string OriginalTitle { get; set; }
        string OriginalArtists { get; set; }
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

        IEnumerable<IHashCode> TitleHashCodes { get; }
        IEnumerable<IHashCode> AlbumHashCodes { get; }
        IEnumerable<IHashCode> ArtistHashCodes { get; }
        IEnumerable<IHashCode> AlbumArtistHashCodes { get; }
        IEnumerable<IHashCode> ComposerHashCodes { get; }
        IEnumerable<IHashCode> ConductorHashCodes { get; }
        IEnumerable<IHashCode> OriginalTitleHashCodes { get; }
        IEnumerable<IHashCode> OriginalArtistHashCodes { get; }

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
    }
}
