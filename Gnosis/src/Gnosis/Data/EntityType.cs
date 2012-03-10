using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum EntityType : byte
    {
        None = 0,        
        Artist,
        Album,
        Track,
        Playlist,
        PlaylistItem,
        Feed,
        FeedItem,
        Show,
        Season,
        Episode,
        Movie,
        Document,
        Bookmark,
        Link,
        Tag
    }
}
