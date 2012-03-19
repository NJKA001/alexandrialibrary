using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tasks
{
    [Flags]
    public enum SearchFilters
    {
        None = 0,
        Albums = 1,
        Artists = 2,
        Clips = 4,
        Docs = 8,
        Feeds = 16,
        Pics = 32,
        Playlists = 64,
        Programs = 128,
        Tracks = 256
    }
}
