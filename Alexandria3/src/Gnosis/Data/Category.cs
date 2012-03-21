using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum Category : ushort
    {
        None = 0,
        Album = 1,
        AlbumArtist = 2,
        Artist = 3,
        BeatsPerMinute = 4,
        Comment = 5,
        Composer = 6,
        Conductor = 7,
        Copyright = 8,
        Disc = 9,
        DiscCount = 10,
        Genre = 11,
        Grouping = 12,
        Lyrics = 13,
        Title = 14,
        Track = 15,
        TrackCount = 16,
        Year = 17
    }
}
