using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Models.Tracks
{
    [Table("TrackUnsynchronizedLyrics")]
    [UniqueIndex("TrackUnsynchronizedLyrics_Track_Language_Description", "Track", "Language", "Description")]
    public interface ITrackUnsynchronizedLyrics : IEntity
    {
        ITrack Track { get; }
        string TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Lyrics { get; set; }
    }
}
