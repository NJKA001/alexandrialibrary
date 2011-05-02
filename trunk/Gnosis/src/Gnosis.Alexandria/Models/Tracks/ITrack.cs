﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrack :
        IChangeableModel
    {
        Uri Location { get; }
        string MediaType { get; }

        string Title { get; set; }
        string TitleSort { get; set; }
        string Subtitle { get; set; }
        string Grouping { get; set; }
        string Comment { get; set; }

        string Album { get; set; }
        string AlbumSort { get; set; }
        string AlbumSubtitle { get; set; }

        string Artists { get; set; }
        string ArtistSort { get; set; }
        string AlbumArtists { get; }

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
        IEnumerable<ITrackUnsynchronizedLyrics> Lyrics { get; }
        IEnumerable<ITrackSynchronizedLyrics> SynchronizedLyrics { get; }
        IEnumerable<ITrackIdentifier> Identifiers { get; }
        IEnumerable<ITrackRating> Ratings { get; }
        IEnumerable<ITrackLink> Links { get; }
        IEnumerable<ITrackMetadata> Metadata { get; }

        void AddPicture(ITrackPicture picture);
        void RemovePicture(ITrackPicture picture);

        void AddLyrics(ITrackUnsynchronizedLyrics lyrics);
        void RemoveLyrics(ITrackUnsynchronizedLyrics lyrics);

        void AddSyncrhonizedLyrics(ITrackSynchronizedLyrics lyrics);
        void RemoveSynchronizedLyrics(ITrackSynchronizedLyrics lyrics);

        void AddIdentifier(ITrackIdentifier identifier);
        void RemoveIdentifier(ITrackIdentifier identifier);

        void AddRating(ITrackRating rating);
        void RemoveRating(ITrackRating rating);

        void AddLink(ITrackLink link);
        void RemoveLink(ITrackLink link);

        void AddMetadata(ITrackMetadata metadata);
        void RemoveMetadata(ITrackMetadata metadata);
    }
}