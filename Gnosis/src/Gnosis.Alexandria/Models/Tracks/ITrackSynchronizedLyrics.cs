using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ITrackSynchronizedLyrics : IEntity
    {
        [ColumnIgnore]
        ITrack Track { get; }
        string TextEncoding { get; set; }
        string Language { get; set; }
        string Description { get; set; }
        string Lyrics { get; set; }
        TrackSynchronizedTextType ContentType { get; set; }
    }
}
