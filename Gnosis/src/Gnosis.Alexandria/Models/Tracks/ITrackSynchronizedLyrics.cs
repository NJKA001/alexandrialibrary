using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;

namespace Gnosis.Alexandria.Models.Tracks
{
    [Table("TrackSynchronizedLyrics")]
    [UniqueIndex("TrackSynchronizedLyrics_Track_Language_Description", "Track", "Language", "Description")]
    public interface ITrackSynchronizedLyrics : IEntity
    {
        ITrack Track { get; }
        string TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Lyrics { get; set; }
        TrackSynchronizedTextType ContentType { get; set; }
    }
}
