using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum WorkType : byte
    {
        None = 0,
        Album = 1,
        Track = 2,
        Playlist = 3,
        PlaylistItem = 4,
        Feed = 5,
        FeedItem = 6,
        Show = 7,
        Season = 8,
        Episode = 9,
        Movie = 10,
        Document = 11,
        Bookmark = 12
    }
}
