using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    [Table("Track")]
    [PrimaryKey("Track_Id", "Id")]
    [UniqueIndex("Track_Location", "Location")]
    [Index("Track_Title", "Title")]
    [Index("Track_TitleSort", "TitleSort")]
    [Index("Track_Sort", "Artists", "ReleaseDate", "Album", "TrackNumber")]
    public interface ITrack :
        IEntity
    {
        Uri Location { get; }
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

        [ChildTable("TrackPictures", "TrackId", "Sequence")]
        IOrderedSet<ITrackPicture> Pictures { get; }

        [ChildTable("TrackLyrics", "TrackId", "Sequence")]
        IOrderedSet<ITrackUnsynchronizedLyrics> Lyrics { get; }
        
        [ChildTable("TrackSynchronizedLyrics", "TrackId", "Sequence")]
        IOrderedSet<ITrackSynchronizedLyrics> SynchronizedLyrics { get; }
        
        [ChildTable("TrackIdentifiers", "TrackId", "Sequence")]
        IOrderedSet<ITrackIdentifier> Identifiers { get; }

        [ChildTable("TrackRatings", "TrackId", "Sequence")]
        IOrderedSet<ITrackRating> Ratings { get; }

        [ChildTable("TrackLinks", "TrackId", "Sequence")]
        IOrderedSet<ITrackLink> Links { get; }

        [ChildTable("TrackMetadata", "TrackId", "Sequence")]
        IOrderedSet<ITrackMetadata> Metadata { get; }


        [ChildTable("TrackTitleHashCodes", "TrackId")]
        ISet<IHashCode> TitleHashCodes { get; }

        [ChildTable("TrackAlbumHashCodes", "TrackId")]
        ISet<IHashCode> AlbumHashCodes { get; }

        [ChildTable("TrackArtistHashCodes", "TrackId")]
        ISet<IHashCode> ArtistHashCodes { get; }

        [ChildTable("TrackAlbumArtistHashCodes", "TrackId")]
        ISet<IHashCode> AlbumArtistHashCodes { get; }

        [ChildTable("TrackComposerHashCodes", "TrackId")]
        ISet<IHashCode> ComposerHashCodes { get; }

        [ChildTable("TrackConductorHashCodes", "TrackId")]
        ISet<IHashCode> ConductorHashCodes { get; }

        [ChildTable("TrackOriginalTitleHashCodes", "TrackId")]
        ISet<IHashCode> OriginalTitleHashCodes { get; }
    }
}
