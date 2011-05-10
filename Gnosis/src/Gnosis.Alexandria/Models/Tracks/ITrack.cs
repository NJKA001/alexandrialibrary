using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Tracks
{
    [Table("Track")]
    [UniqueIndex("Track_Location", "Location")]
    
    [Index("Track_Title", "Title")]
    [Index("Track_TitleSort", "TitleSort")]
    [Index("Track_Album", "Album")]
    [Index("Track_AlbumSort", "AlbumSort")]
    [Index("Track_Artists", "Artists")]
    [Index("Track_ArtistsSort", "ArtistsSort")]
    [Index("Track_Composers", "Composers")]
    [Index("Track_Conductors", "Conductors")]
    [Index("Track_Genres", "Genres")]
    [Index("Track_Moods", "Moods")]
    [Index("Track_RecordingDate", "RecordingDate")]
    [Index("Track_ReleaseDate", "ReleaseDate")]
    [Index("Track_OriginalTitle", "OriginalTitle")]
    [Index("Track_OriginalReleaseDate", "OriginalReleaseDate")]

    [Index("Track_Sort", "Artists", "ReleaseDate", "Album", "DiscNumber", "TrackNumber")]
    [DefaultSort("Artists ASC, ReleaseDate ASC, Album ASC, DiscNumber ASC, TrackNumber ASC")]
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

        [OneToMany("TrackPicture")]
        [ForeignUniqueIndex("TrackPicture_Parent_Description", "Parent", "Description")]
        Gnosis.Core.Collections.ISet<ITrackPicture> Pictures { get; }

        [OneToMany("TrackLyrics")]
        [ForeignUniqueIndex("TrackLyrics_Parent_Language_Description", "Parent", "Language", "Description")]
        Gnosis.Core.Collections.ISet<ITrackUnsynchronizedLyrics> Lyrics { get; }

        [OneToMany("TrackSynchronizedLyrics")]
        [ForeignUniqueIndex("TrackSynchronizedLyrics_Parent_Language_Description", "Parent", "Language", "Description")]
        Gnosis.Core.Collections.ISet<ITrackSynchronizedLyrics> SynchronizedLyrics { get; }

        [OneToMany("TrackIdentifier")]
        [ForeignUniqueIndex("TrackIdentifier_Parent_Scheme_Identifier", "Parent", "Scheme", "Identifier")]
        [ForeignIndex("TrackIdentifier_Identifier", "Identifier")]
        IOrderedSet<ITrackIdentifier> Identifiers { get; }

        [OneToMany("TrackRating")]
        [ForeignUniqueIndex("TrackRating_Parent_User", "Parent", "User")]
        IOrderedSet<ITrackRating> Ratings { get; }

        [OneToMany("TrackLink")]
        [ForeignUniqueIndex("TrackLink_Parent_Relationship", "Parent", "Relationship")]
        [ForeignIndex("TrackLink_Location", "Location")]
        IOrderedSet<ITrackLink> Links { get; }

        [OneToMany("TrackMetadata")]
        [ForeignUniqueIndex("TrackMetadata_Parent_Description", "Parent", "Description")]
        [ForeignIndex("TrackMetadata_Description", "Description")]
        [ForeignIndex("TrackMetadata_Content", "Content")]
        IOrderedSet<ITrackMetadata> Metadata { get; }

        [OneToMany("TrackTitleHash")]
        [ForeignUniqueIndex("TrackTitleHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackTitleHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> TitleHashCodes { get; }

        [OneToMany("TrackAlbumHash")]
        [ForeignUniqueIndex("TrackAlbumHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackAlbumHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> AlbumHashCodes { get; }

        [OneToMany("TrackArtistHash")]
        [ForeignUniqueIndex("TrackTitleHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackArtistHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> ArtistHashCodes { get; }

        [OneToMany("TrackAlbumArtistHash")]
        [ForeignUniqueIndex("TrackAlbumArtistHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackAlbumArtistHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> AlbumArtistHashCodes { get; }

        [OneToMany("TrackComposerHash")]
        [ForeignUniqueIndex("TrackComposerHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackComposerHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> ComposerHashCodes { get; }

        [OneToMany("TrackConductorHash")]
        [ForeignUniqueIndex("TrackConductorHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackConductorHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> ConductorHashCodes { get; }

        [OneToMany("TrackOriginalTitleHash")]
        [ForeignUniqueIndex("TrackOriginalTitleHash_Parent_Scheme", "Parent", "Scheme")]
        [ForeignIndex("TrackOriginalTitleHash_Value", "Value")]
        Gnosis.Core.Collections.ISet<IHashCode> OriginalTitleHashCodes { get; }
    }
}
