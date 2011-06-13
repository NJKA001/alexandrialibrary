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
        void AddPicture(ITrackPicture picture);
        void RemovePicture(ITrackPicture picture);

        IEnumerable<ITrackUnsynchronizedLyrics> Lyrics { get; }
        void AddLyrics(ITrackUnsynchronizedLyrics lyrics);
        void RemoveLyrics(ITrackUnsynchronizedLyrics lyrics);

        IEnumerable<ITrackSynchronizedLyrics> SynchronizedLyrics { get; }
        void AddSynchronizedLyrics(ITrackSynchronizedLyrics synchronizedLyrics);
        void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics synchronizedLyrics);

        IEnumerable<ITrackIdentifier> Identifiers { get; }
        //void AddIdentifier(ITrackIdentifier identifier);
        //void RemoveIdentifier(ITrackIdentifier identifier);

        IEnumerable<ITrackRating> Ratings { get; }
        //void AddRating(ITrackRating rating);
        //void RemoveRating(ITrackRating rating);

        IEnumerable<ITrackLink> Links { get; }
        //void AddLink(ITrackLink link);
        //void RemoveLink(ITrackLink link);

        IEnumerable<ITrackMetadatum> Metadata { get; }
        //void AddMetadatum(ITrackMetadatum metadatum);
        //void RemoveMetadatum(ITrackMetadatum metadatum);

        IEnumerable<IHashCode> TitleHashCodes { get; }
        IEnumerable<IHashCode> AlbumHashCodes { get; }
        IEnumerable<IHashCode> ArtistHashCodes { get; }
        IEnumerable<IHashCode> AlbumArtistHashCodes { get; }
        IEnumerable<IHashCode> ComposerHashCodes { get; }
        IEnumerable<IHashCode> ConductorHashCodes { get; }
        IEnumerable<IHashCode> OriginalTitleHashCodes { get; }
    }
}
